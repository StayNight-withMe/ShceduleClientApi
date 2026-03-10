
using Contracts.Schedules;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.ScheduleSave;
public class ScheduleSaveHandler : IRequestHandler<DayScheduleDTO>
{
    public readonly ILogger<ScheduleSaveHandler> _logger;
    public ScheduleSaveHandler(ILogger<ScheduleSaveHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DayScheduleDTO request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("broker continue");
        return Task.CompletedTask;
    }
}
