using Application.Common.Messaging;
using MassTransit;

namespace ClientScheduleApi.Extensions.DI;

public static class MessagingExtensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
  
            x.AddConsumer<ScheduleUpdateConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/");

                cfg.ReceiveEndpoint("client-lesson-updates", e =>
                {
                    e.ConfigureConsumer<ScheduleUpdateConsumer>(context);
                });
            });
        });

        return services;
    }

}

