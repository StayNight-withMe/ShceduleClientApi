using Domain.Model.Common.Lesson;

namespace Domain.Model.TargetEntity.DaySchedule;

public class DayShceduleDTO
{
    public string GroupName { get; set; } 
    public DateOnly Date { get; set; }
    public List<Lesson> Lessons { get; set; }
}
