using Application.Features.TeacherSchedule.Common;
using Domain.Model.ReturnEntity;
using MediatR;

namespace Application.Features.TeacherSchedule.Queries;


public record GetTeacherWeekScheduleQuery(string FullName, int? DaysCount = null)
    : IRequest<TResult<GetTeacherWeekScheduleDTO>>;
