using Application.Abstraction.DataBase;
using Application.Features.TeacherSchedule.Common;
using Contracts.Common;
using Contracts.Schedules;
using Domain.Common.Enums;
using Domain.Model.Entity;
using Domain.Model.ReturnEntity;
using Domain.Specification;
using MediatR;

namespace Application.Features.TeacherSchedule.Queries;

public class GetTeacherDayScheduleHandler
    : IRequestHandler<GetTeacherDayScheduleQuery, TResult<GetTeacherDayScheduleDTO>>
{
    private readonly IBaseRepository<LessonEntity> _repository;

    public GetTeacherDayScheduleHandler(IBaseRepository<LessonEntity> repository)
    {
        _repository = repository;
    }

    public async Task<TResult<GetTeacherDayScheduleDTO>> Handle(
        GetTeacherDayScheduleQuery request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.FullName))
            return TResult<GetTeacherDayScheduleDTO>.FailedOperation(ErrorCode.BadRequest);

        var targetDate = DateOnly.FromDateTime(DateTime.Today);

        var spec = new TeacherDayScheduleSpecification(request.FullName, targetDate);
        var entities = await _repository.ListAsync(spec, cancellationToken);

        if (!entities.Any())
            return TResult<GetTeacherDayScheduleDTO>.FailedOperation(ErrorCode.NotFound);

        var dayDto = new DayScheduleDTO
        {
            Date = targetDate,
            Group = string.Join(", ", entities.Select(e => e.daySchedule.GroupName).Distinct()),
            Lessons = entities.Select(e => new Lesson
            {
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                Lesson1 = e.Subject1 ?? string.Empty,
                Lesson2 = e.Subject2 ?? string.Empty,
                Fio1 = e.Teacher1 ?? string.Empty,
                Fio2 = e.Teacher2 ?? string.Empty,
                ClassRoom1 = e.Classroom1 ?? string.Empty,
                ClassRoom2 = e.Classroom2 ?? string.Empty
            }).ToList()
        };

        return TResult<GetTeacherDayScheduleDTO>.CompletedOperation(new GetTeacherDayScheduleDTO { Schedule = dayDto });
    }
}
