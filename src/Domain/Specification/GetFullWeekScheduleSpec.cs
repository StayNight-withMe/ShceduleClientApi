using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Ardalis.Specification;
using Domain.Model.Entity;

namespace Domain.Specification;

public class GetFullWeekScheduleSpec : Specification<LessonEntity>
{
    public GetFullWeekScheduleSpec(DateOnly date)
    {
        Query
            .AsNoTracking()
            .Where(x => x.daySchedule.Date == date)
            .Include(x => x.daySchedule)
            .OrderBy(x => x.daySchedule.GroupName) 
            .ThenBy(x => x.LessonNumber); 
    }
}
