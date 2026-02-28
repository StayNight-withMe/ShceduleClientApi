
//using Domain.Model.TargetEntity.DaySchedule;

using Contracts.Schedules;
using Contracts.Common;


namespace Application.Features.TeacherSchedule.Common;
public class GetGroupWeekScheduleDTO
{
    public List<DayScheduleDTO> Shcedule { get; set; }
}
