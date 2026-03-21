using Application.Abstraction.DataBase;
using Application.Features.GroupWeekSchedule.Common;
using Contracts.Common;
using Contracts.Schedules;
using Domain.Model.Entity;
using Domain.Model.ReturnEntity;
using Domain.Specification;
using MediatR;

namespace Application.Features.GroupWeekSchedule.Queries;

public class GetGroupWeekScheduleHandler : IRequestHandler<GetGroupWeekScheduleQuery, TResult<GetGroupWeekScheduleDTO>>
{
    private readonly IBaseRepository<LessonEntity> _lessonRepo;

    public GetGroupWeekScheduleHandler(
        IBaseRepository<LessonEntity> lessonRepo
    )
    {
        _lessonRepo = lessonRepo;
    }

    public async Task<TResult<GetGroupWeekScheduleDTO>> Handle(GetGroupWeekScheduleQuery request, CancellationToken ct)
    {
        var lessons = await _lessonRepo.ListAsync(new GetGroupWeekScheduleSpec(request.GroupName, request.Date));

        var dayScheduleList = lessons.GroupBy(x => x.daySchedule);

        var mappedDaySchedule = dayScheduleList.Select(g => new DayScheduleDTO
        {
            Group = g.Key.GroupName,
            Date = g.Key.Date,
            Lessons = g.Select(l => new Lesson
            {
                StartTime = l.StartTime,
                EndTime = l.EndTime,
                Lesson1 = l.Subject1,
                Lesson2 = l.Subject2,
                Fio1 = l.Teacher1,
                Fio2 = l.Teacher2,
                ClassRoom1 = l.Classroom1,
                ClassRoom2 = l.Classroom2
            }).ToList()
        }).ToList();

        return TResult<GetGroupWeekScheduleDTO>.CompletedOperation(new GetGroupWeekScheduleDTO
        {
            Schedule = mappedDaySchedule
        });
    }
}

