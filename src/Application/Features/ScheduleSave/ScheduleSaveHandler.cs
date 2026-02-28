
using Contracts.Schedules;
using MediatR;

namespace Application.Features.ScheduleSave;
public class ScheduleSaveHandler : IRequestHandler<DayScheduleDTO>
{
    public Task Handle(DayScheduleDTO request, CancellationToken cancellationToken)
    {
        Console.WriteLine(request.Date);
        Console.WriteLine(request.Group);

        foreach (var i in request.Lessons)
        {
         Console.WriteLine($"{i.Lesson1}");
         Console.WriteLine($"{i.Lesson2}");
         Console.WriteLine($"{i.ClassRoom1}");
         Console.WriteLine($"{i.ClassRoom2}");
         Console.WriteLine($"{i.Fio1}");
         Console.WriteLine($"{i.Fio2}");
         Console.WriteLine($"{i.Lesson1}");
         Console.WriteLine($"{i.Lesson2}");
        }

        return Task.CompletedTask;


    }
}
