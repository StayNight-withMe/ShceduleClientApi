using System.Collections;
using Ardalis.Specification;
using Domain.Model.Entity;

namespace Domain.Specification;

public class TeacherWeekScheduleSpecification : Specification<LessonEntity>
{
    public TeacherWeekScheduleSpecification(string fullName, DateOnly startDate, DateOnly endDate)
    {
        var name = fullName.Trim().ToLower();

        Query.Where(l => l.daySchedule.Date >= startDate && l.daySchedule.Date <= endDate)
             .Where(l => (l.Teacher1 ?? "").ToLower() == name || (l.Teacher2 ?? "").ToLower() == name)
             .Include(l => l.daySchedule)
             .AsNoTracking()
             .OrderBy(l => l.daySchedule.Date)
             .ThenBy(l => l.StartTime);
    }
}
