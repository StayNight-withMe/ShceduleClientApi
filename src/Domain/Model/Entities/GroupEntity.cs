using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model.Entities;

public class GroupEntity
{
    [Key]
    public Guid Id { get; set; } = default!;
    public Guid SpecialtyId { get; set; } = default!;
    public string Name { get; set; } = default!;

    [ForeignKey(nameof(SpecialtyId))]
    public SpecialtyEntity Specialty { get; set; } = default!;
}
