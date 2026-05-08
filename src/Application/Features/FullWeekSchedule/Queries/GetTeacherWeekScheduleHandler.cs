using Application.Features.TeacherSchedule.Common;
using Application.Abstraction.DataBase;
using Domain.Model.ReturnEntity;
using Domain.Model.Entity;
using Domain.Specification;
using Contracts.Schedules;
using Contracts.Common;
using MediatR;

namespace Application.Features.TeacherSchedule.Queries;

public class GetFullWeekScheduleHandler : IRequestHandler<GetFullWeekScheduleQuery, TResult<GetFullWeekScheduleDTO>>
{
    private readonly IBaseRepository<LessonEntity> _repository;

    public GetFullWeekScheduleHandler(IBaseRepository<LessonEntity> repository)
    {
        _repository = repository;
    }

    public async Task<TResult<GetFullWeekScheduleDTO>> Handle(GetFullWeekScheduleQuery request, CancellationToken cancellationToken)
    {
        var spec = new GetFullWeekScheduleSpec(request.date);
        var lessons = await _repository.ListAsync(spec, cancellationToken);

        var schedules = lessons
            .GroupBy(l => l.daySchedule.GroupName)
            .Select(g => new DayScheduleDTO
            {
                Group = g.Key,
                Date = request.date,
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
            })
            .ToList();

        var resultDto = new GetFullWeekScheduleDTO() { Shcedule = schedules };
        return TResult<GetFullWeekScheduleDTO>.CompletedOperation(resultDto);
    }
}
