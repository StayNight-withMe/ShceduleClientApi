
using Application.Abstraction.DataBase;
using Application.Features.TeacherSchedule.Common;
using Domain.Model.Entity;
using Domain.Model.ReturnEntity;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.TeacherSchedule.Queries;
public class GetGroupDayScheduleHandler : IRequestHandler<GetGroupDayScheduleQuery, TResult<GetGroupDayScheduleDTO>>
{
    public readonly ILogger<GetGroupDayScheduleHandler> _logger;

    public readonly IBaseRepository<DayScheduleEntity> _dayScheduleRepo;

    public GetGroupDayScheduleHandler(
        ILogger<GetGroupDayScheduleHandler> logger,
        IBaseRepository<DayScheduleEntity> dayScheculeRepo
        )
    {
        _logger = logger;
        _dayScheduleRepo = dayScheculeRepo;
    }


    public Task<TResult<GetGroupDayScheduleDTO>> Handle(GetGroupDayScheduleQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

