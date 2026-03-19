
using Application.Abstraction.DataBase;
using Application.Features.TeacherSchedule.Common;
using AutoMapper;
using Contracts.Common;
using Contracts.Schedules;
using Domain.Model.Entity;
using Domain.Model.ReturnEntity;
using Domain.Specification;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.TeacherSchedule.Queries;
public class GetGroupDayScheduleHandler : IRequestHandler<GetGroupDayScheduleQuery, TResult<GetGroupDayScheduleDTO>>
{
    public readonly ILogger<GetGroupDayScheduleHandler> _logger;

    public readonly IBaseRepository<LessonEntity> _lessonRepo;
    private readonly IMapper _mapper;

    public GetGroupDayScheduleHandler(
        ILogger<GetGroupDayScheduleHandler> logger,
        IBaseRepository<LessonEntity> lessonRepo,
        IMapper mapper
        )
    {
        _logger = logger;
        _lessonRepo = lessonRepo;
        _mapper = mapper;
    }


    public async Task<TResult<GetGroupDayScheduleDTO>> Handle(GetGroupDayScheduleQuery request, CancellationToken cancellationToken)
    {
        var lessons = await _lessonRepo.ListAsync(new LessonsByGroupAndDateSpec(request.GroupName, request.Day));

        var lessonsList = _mapper.Map<List<Lesson>>(lessons);

        var daySchedule = lessons.First().daySchedule;
        var mappedDaySchedule = _mapper.Map<DayScheduleDTO>(daySchedule);

        return TResult<GetGroupDayScheduleDTO>.CompletedOperation(new GetGroupDayScheduleDTO {
            Shcedule = mappedDaySchedule
        });
    }
}

