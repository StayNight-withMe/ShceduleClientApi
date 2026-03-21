using Application.Features.GroupWeekSchedule.Common;
using Domain.Model.ReturnEntity;
using MediatR;

namespace Application.Features.GroupWeekSchedule.Queries;

public record GetGroupWeekScheduleQuery(string GroupName, DateOnly Date) : IRequest<TResult<GetGroupWeekScheduleDTO>>;
