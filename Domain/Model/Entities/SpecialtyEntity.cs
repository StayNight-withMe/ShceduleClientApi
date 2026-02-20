using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Entities;
public class SpecialtyEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
}
