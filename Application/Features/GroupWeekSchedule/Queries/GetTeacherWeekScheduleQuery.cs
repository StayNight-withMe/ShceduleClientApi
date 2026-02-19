using Application.Features.TeacherSchedule.Common;
using Domain.Model.ReturnEntity;
using MediatR;

namespace Application.Features.TeacherSchedule.Queries;

public record GetGroupWeekScheduleQuery(string GroupName, DateOnly date) : IRequest<TResult<GetGroupWeekScheduleDTO>>;
