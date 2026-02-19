using Application.Features.TeacherSchedule.Common;
using Domain.Model.ReturnEntity;
using MediatR;

namespace Application.Features.TeacherSchedule.Queries;

public record GetFullWeekScheduleQuery(DateOnly date) : IRequest<TResult<GetFullWeekScheduleDTO>>;
