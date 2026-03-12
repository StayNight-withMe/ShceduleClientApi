using Ardalis.Specification;
using Domain.Model.Entities;
using Domain.ReadModels.AllGroup;


namespace Domain.Specification;
public class AllGroupSpec : Specification<GroupEntity, AllGroupDTO>
{
    public AllGroupSpec()
    {

        Query.AsNoTracking();

        Query.Include(c => c.Specialty)
                .OrderBy(c => c.Specialty.Name)
                .ThenBy(c => c.Name)
                .Select(c => new AllGroupDTO() { Name = c.Name, Speciality = new SpecialityDTO() { Name = c.Specialty.Name } } );
                

    }
}
