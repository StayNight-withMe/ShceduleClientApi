
using Application.Abstraction.DataBase;
using Contracts.Schedules;
using Domain.Model.Entity;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.ScheduleSave;
public class ScheduleSaveHandler : IRequestHandler<DayScheduleDTO>
{
    public readonly ILogger<ScheduleSaveHandler> _logger;

    public readonly IBaseRepository<DayScheduleEntity> _dayScheduleRepository;

    public readonly IBaseRepository<LessonEntity> _lessonEntityRepository;

    public ScheduleSaveHandler(
        ILogger<ScheduleSaveHandler> logger,
        IBaseRepository<DayScheduleEntity> dayScheduleRepository,
        IBaseRepository<LessonEntity> lessonEntityRepository)
    {
        _logger = logger;
        _dayScheduleRepository = dayScheduleRepository;
        _lessonEntityRepository = lessonEntityRepository;
    }

    public Task Handle(DayScheduleDTO request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("broker continue");
        return Task.CompletedTask;
    }
}
