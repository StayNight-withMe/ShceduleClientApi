using Contracts.Schedules;

namespace Application.Features.TeacherSchedule.Common;

public class GetTeacherWeekScheduleDTO
{
    public List<DayScheduleDTO> Schedule { get; set; } = default!;
}
