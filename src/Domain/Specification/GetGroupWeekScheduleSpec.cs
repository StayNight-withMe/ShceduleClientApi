using Ardalis.Specification;
using Domain.Model.Entity;

namespace Domain.Specification;

public class GetGroupWeekScheduleSpec : Specification<LessonEntity, LessonEntity>
{
    public GetGroupWeekScheduleSpec(string groupName, DateOnly date)
    {
        Query
            .Where(x => x.daySchedule.GroupName == groupName)
            .Where(x => x.daySchedule.Date <= date)
            .OrderBy(x => x.daySchedule.Date);

        Query
            .Include(x => x.daySchedule);
    }
}