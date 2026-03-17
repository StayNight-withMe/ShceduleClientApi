using Ardalis.Specification;
using Domain.Model.Entity;

namespace Domain.Specification;

public class GetOrderedTeacherSpec : Specification<TeacherEntity, string>
{
    public GetOrderedTeacherSpec()
    {
        Query
            .OrderBy(c => c.FullName)
            .Select(t => t.FullName);
    }
}