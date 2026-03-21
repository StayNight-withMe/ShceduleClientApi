using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Entity;

public class DayScheduleEntity
{
    [Key]
    public Guid Id { get; set; } = default!;
    [MaxLength(2)]
    public string GroupName { get; set; } = default!;
    public DateOnly Date { get; set; } = default!;
}
