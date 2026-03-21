using Contracts.Schedules;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Messaging;

public class ScheduleUpdateConsumer : IConsumer<DayScheduleDTO>
{
    private readonly ISender _mediator;

    private readonly ILogger<ScheduleUpdateConsumer> _logger;
    public ScheduleUpdateConsumer(
        ISender mediator,
        ILogger<ScheduleUpdateConsumer> logger
        )
    {
        _mediator = mediator;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<DayScheduleDTO> context)
    {
        _logger.LogInformation("Consuming schedule update for group {Group}", context.Message.Group);
        await _mediator.Send(context.Message, context.CancellationToken);
    }
}
