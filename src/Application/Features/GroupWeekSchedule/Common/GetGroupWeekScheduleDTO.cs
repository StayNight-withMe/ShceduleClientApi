using Contracts.Schedules;

namespace Application.Features.TeacherSchedule.Common;

public class GetGroupWeekScheduleDTO
{
    public List<DayScheduleDTO> Schedule { get; set; } = default!;
}
