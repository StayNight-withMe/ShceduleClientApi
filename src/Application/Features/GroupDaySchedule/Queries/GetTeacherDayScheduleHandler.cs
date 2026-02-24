
using Application.Features.TeacherSchedule.Common;
using Domain.Model.ReturnEntity;
using MediatR;

namespace Application.Features.TeacherSchedule.Queries;
public class GetGroupDayScheduleHandler : IRequestHandler<GetGroupDayScheduleQuery, TResult<GetGroupDayScheduleDTO>>
{
    public Task<TResult<GetGroupDayScheduleDTO>> Handle(GetGroupDayScheduleQuery request, CancellationToken cancellationToken) => throw new NotImplementedException();
}

