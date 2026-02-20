

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model.Entity;
public class LessonEntity
{
    [Key]
    public Guid Groupid { get; set; }
    public string Subject1 { get; set; }
    public string Subject2 { get; set; }
    public string Teacher1 { get; set; }
    public string Teacher2 { get; set; }
    public string Classroom1 {  get; set; }
    public string Classroom2 { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    [ForeignKey(nameof(Groupid))]
    public DayScheduleEntity daySchedule { get; set; }
}
