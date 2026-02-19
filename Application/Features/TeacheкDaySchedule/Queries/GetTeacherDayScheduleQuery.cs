using Application.Features.TeacherSchedule.Common;
using Domain.Model.ReturnEntity;
using MediatR;

namespace Application.Features.TeacherSchedule.Queries;

public record GetTeacherDayScheduleQuery(string FullName) : IRequest<TResult<GetTeacherDayScheduleDTO>>;
