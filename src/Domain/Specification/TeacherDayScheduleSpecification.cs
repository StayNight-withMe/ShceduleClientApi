using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Ardalis.Specification;
using Domain.Model.Entity;

namespace Domain.Specification;

public class TeacherDayScheduleSpecification : Specification<LessonEntity>
{
    public TeacherDayScheduleSpecification(string fullName, DateOnly targetDate)
    {
        var name = fullName.Trim().ToLower();

        Query.Where(l => l.daySchedule.Date == targetDate)
             .Where(l => (l.Teacher1 ?? "").ToLower() == name || (l.Teacher2 ?? "").ToLower() == name)
             .Include(l => l.daySchedule)
             .AsNoTracking()
             .OrderBy(l => l.StartTime);
    }
}
