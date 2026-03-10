using Application;
using Application.Common.Behaviors;
using FluentValidation;
using MassTransit;
using MediatR;

namespace ClientScheduleApi.Extensions.DI;

public static class ServiceConfigurationExtensions
{
    public static IServiceCollection AddCustomService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        // MediatR + FluentValidationBehavior
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

     
       

        return services;
    }
}
