
using Application.Features.TeacherSchedule.Common;
using Domain.Model.ReturnEntity;
using MediatR;

namespace Application.Features.TeacherSchedule.Queries;
public class GetTeacherWeekScheduleHandler : IRequestHandler<GetTeacherWeekScheduleQuery, TResult<GetTeacherWeekScheduleDTO>>
{
    public Task<TResult<GetTeacherWeekScheduleDTO>> Handle(GetTeacherWeekScheduleQuery request, CancellationToken cancellationToken) => throw new NotImplementedException();
}

