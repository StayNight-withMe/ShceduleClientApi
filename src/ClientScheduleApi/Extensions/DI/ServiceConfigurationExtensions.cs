using Application;
using Application.Common.Behaviors;
using FluentValidation;
using MassTransit;
using MediatR;
using Web.Extensions.Other;

namespace ClientScheduleApi.Extensions.DI;

public static class ServiceConfigurationExtensions
{
    public static IServiceCollection AddCustomService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services.AddRateLimit();

        // MediatR + FluentValidationBehavior
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        services.AddAutoMapper(typeof(AssemblyMarker));


        return services;
    }
}
