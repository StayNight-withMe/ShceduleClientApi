using Contracts.Schedules;

namespace Application.Features.GroupWeekSchedule.Common;

public class GetGroupWeekScheduleDTO
{
    public List<DayScheduleDTO> Schedule { get; set; } = default!;
}
