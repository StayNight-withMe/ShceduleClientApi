using Contracts.Schedules;

namespace Application.Features.GroupWeekSchedule.Common;

public class GetGroupWeekScheduleDTO
{
    public List<DayScheduleDTO> Shcedule { get; set; } = default!;
}