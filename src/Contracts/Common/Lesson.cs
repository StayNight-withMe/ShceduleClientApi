namespace Contracts.Common;

public class Lesson
{
    public TimeOnly StartTime { get; init; } = default!;
    public TimeOnly EndTime { get; init; } = default!;
    public string Lesson1 { get; init; } = default!;
    public string Lesson2 { get; init; } = default!;
    public string Fio1 { get; init; } = default!;
    public string Fio2 { get; init; } = default!;
    public string ClassRoom1 { get; init; } = default!;
    public string ClassRoom2 { get; init; } = default!;
}
