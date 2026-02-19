
using Application.Features.TeacherSchedule.Common;
using Domain.Model.ReturnEntity;
using MediatR;

namespace Application.Features.TeacherSchedule.Queries;
public class GetTeacherDayScheduleHandler : IRequestHandler<GetTeacherDayScheduleQuery, TResult<GetTeacherDayScheduleDTO>>
{
    public Task<TResult<GetTeacherDayScheduleDTO>> Handle(GetTeacherDayScheduleQuery request, CancellationToken cancellationToken) => throw new NotImplementedException();
}

