using Domain.Common.Enums;

namespace Domain.Model.Common.Lesson;

public class Lesson
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string Lesson1 { get; set; }
    public string Lesson2 { get; set; }
    public string Fio1 { get; set; }
    public string Fio2 { get; set; }
    public string ClassRoom1 { get; set; }
    public string ClassRoom2 { get; set; }
    public durationState DurationState { get; set; }
}
