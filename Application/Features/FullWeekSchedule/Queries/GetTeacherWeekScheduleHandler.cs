
using Application.Features.TeacherSchedule.Common;
using Domain.Model.ReturnEntity;
using MediatR;

namespace Application.Features.TeacherSchedule.Queries;
public class GetFullWeekScheduleHandler : IRequestHandler<GetFullWeekScheduleQuery, TResult<GetFullWeekScheduleDTO>>
{
    public Task<TResult<GetFullWeekScheduleDTO>> Handle(GetFullWeekScheduleQuery request, CancellationToken cancellationToken) => throw new NotImplementedException();
}

