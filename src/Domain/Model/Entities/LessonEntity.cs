using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model.Entity;

public class LessonEntity
{
    [Key]
    public Guid Groupid { get; set; } = default!;
    public string Subject1 { get; set; } = default!;
    public string Subject2 { get; set; } = default!;
    public string Teacher1 { get; set; } = default!;
    public string Teacher2 { get; set; } = default!;
    public string Classroom1 { get; set; } = default!;
    public string Classroom2 { get; set; } = default!;
    public TimeOnly StartTime { get; set; } = default!;
    public TimeOnly EndTime { get; set; } = default!;
    public int LessonNumber { get; set; } = default!;

    [ForeignKey(nameof(Groupid))]
    public DayScheduleEntity daySchedule { get; set; } = default!;
}
