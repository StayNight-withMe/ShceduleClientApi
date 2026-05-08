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

public class GetTeacherWeekScheduleHandler
    : IRequestHandler<GetTeacherWeekScheduleQuery, TResult<GetTeacherWeekScheduleDTO>>
{
    private readonly IBaseRepository<LessonEntity> _repository;

    public GetTeacherWeekScheduleHandler(IBaseRepository<LessonEntity> repository)
    {
        _repository = repository;
    }

    public async Task<TResult<GetTeacherWeekScheduleDTO>> Handle(
        GetTeacherWeekScheduleQuery request,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.FullName))
            return TResult<GetTeacherWeekScheduleDTO>.FailedOperation(ErrorCode.BadRequest);

        var startDate = DateOnly.FromDateTime(DateTime.Today);
        var daysCount = request.DaysCount ?? 7;
        var endDate = startDate.AddDays(daysCount - 1);

        var spec = new TeacherWeekScheduleSpecification(request.FullName, startDate, endDate);
        var entities = await _repository.ListAsync(spec, ct);

        if (!entities.Any())
            return TResult<GetTeacherWeekScheduleDTO>.FailedOperation(ErrorCode.NotFound);

        var schedule = entities
            .GroupBy(e => e.daySchedule.Date)
            .Select(g => new DayScheduleDTO
            {
                Date = g.Key,
                Group = string.Join(", ", g.Select(x => x.daySchedule.GroupName).Distinct()),
                Lessons = g.OrderBy(x => x.StartTime).Select(x => new Lesson
                {
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    Lesson1 = x.Subject1 ?? string.Empty,
                    Lesson2 = x.Subject2 ?? string.Empty,
                    Fio1 = x.Teacher1 ?? string.Empty,
                    Fio2 = x.Teacher2 ?? string.Empty,
                    ClassRoom1 = x.Classroom1 ?? string.Empty,
                    ClassRoom2 = x.Classroom2 ?? string.Empty
                }).ToList()
            })
            .OrderBy(d => d.Date)
            .ToList();

        return TResult<GetTeacherWeekScheduleDTO>.CompletedOperation(new GetTeacherWeekScheduleDTO { Schedule = schedule });
    }
}
