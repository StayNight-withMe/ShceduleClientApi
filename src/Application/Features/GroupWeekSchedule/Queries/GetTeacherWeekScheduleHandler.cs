
using Application.Features.TeacherSchedule.Common;
using Domain.Model.ReturnEntity;
using MediatR;

namespace Application.Features.TeacherSchedule.Queries;
public class GetGroupWeekScheduleHandler : IRequestHandler<GetGroupWeekScheduleQuery, TResult<GetGroupWeekScheduleDTO>>
{
    public Task<TResult<GetGroupWeekScheduleDTO>> Handle(GetGroupWeekScheduleQuery request, CancellationToken cancellationToken) => throw new NotImplementedException();
}

