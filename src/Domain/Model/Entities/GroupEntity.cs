using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model.Entities;
public class GroupEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid SpecialtyId { get; set; }
    public string Name { get; set; }

    [ForeignKey(nameof(SpecialtyId))]
    public SpecialtyEntity Specialty { get; set; }
}
