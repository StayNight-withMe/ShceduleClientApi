using Application.Abstraction.DataBase;
using AutoMapper;
using Contracts.Schedules;
using Domain.Common.CustomException;
using Domain.Model.Entity;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.ScheduleSave;
public class ScheduleSaveHandler : IRequestHandler<DayScheduleDTO>
{
    private readonly ILogger<ScheduleSaveHandler> _logger;

    private readonly IBaseRepository<DayScheduleEntity> _dayScheduleRepository;

    private readonly IBaseRepository<LessonEntity> _lessonEntityRepository;

    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;

    public ScheduleSaveHandler(
        ILogger<ScheduleSaveHandler> logger,
        IBaseRepository<DayScheduleEntity> dayScheduleRepository,
        IBaseRepository<LessonEntity> lessonEntityRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper
        )
    {
        _logger = logger;
        _dayScheduleRepository = dayScheduleRepository;
        _lessonEntityRepository = lessonEntityRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(DayScheduleDTO request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("broker continue");

        await _unitOfWork.CommitAsync(cancellationToken);
        var groupEntity = _mapper.Map<DayScheduleEntity>(request);
        var lessonsEntity = _mapper.Map<List<LessonEntity>>(request.Lessons);

        lessonsEntity.ForEach(c => c.Groupid = groupEntity.Id);
        await _dayScheduleRepository.AddAsync(groupEntity);

        foreach (var lessonEntity in lessonsEntity)
        {
            await _lessonEntityRepository.AddAsync(lessonEntity);
        }

        try
        {
          await _unitOfWork.CommitAsync(cancellationToken);
        }
        catch (CustomDbException ex) 
        {
            _logger.LogError(ex, "message to broker");
            throw;
        }

    }
}
