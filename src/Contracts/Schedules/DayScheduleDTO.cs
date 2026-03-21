using Contracts.Common;
using MediatR;

namespace Contracts.Schedules;

public class DayScheduleDTO : IRequest
{
    public string Group { get; init; } = default!;
    public DateOnly Date { get; init; } = default!;
    public List<Lesson> Lessons { get; init; } = default!;
}
