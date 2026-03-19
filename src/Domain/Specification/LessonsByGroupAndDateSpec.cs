using Ardalis.Specification;
using Domain.Model.Entity;

namespace Domain.Specification;

public class LessonsByGroupAndDateSpec : Specification<LessonEntity>
{
    public LessonsByGroupAndDateSpec(string groupName, DateOnly date)
    {
        Query
            .AsNoTracking()
            .Where(x => x.daySchedule.GroupName == groupName)
            .Where(x => x.daySchedule.Date == date)
            .Include(x => x.daySchedule);
    }
}