

using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Entity;
public class DayScheduleEntity
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(2)]
    public string GroupName { get; set; }
    public DateOnly Date { get; set; }
}
