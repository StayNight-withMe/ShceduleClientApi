using Contracts.Schedules;

namespace Application.Features.TeacherSchedule.Common;

public class GetTeacherDayScheduleDTO
{
    public DayScheduleDTO Schedule { get; set; } = default!;
}
