using Contracts.Schedules;
using MassTransit;
using MediatR;

namespace Application.Common.Messaging;
public class ScheduleUpdateConsumer : IConsumer<DayScheduleDTO>
{
    private readonly ISender _mediator;

    public ScheduleUpdateConsumer(ISender mediator) => _mediator = mediator;

    public async Task Consume(ConsumeContext<DayScheduleDTO> context)
    {
        await _mediator.Send(context.Message); 
    }
}
