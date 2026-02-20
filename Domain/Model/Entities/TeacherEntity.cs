
using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Entity;
public class TeacherEntity
{
    [Key]
    public Guid Id { get; set; }
    public string FullName { get; set; }
}
