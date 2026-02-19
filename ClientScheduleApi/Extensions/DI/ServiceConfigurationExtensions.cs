using Application;
using Application.Common.Behaviors;
using FluentValidation;
using MediatR;

namespace ClientScheduleApi.Extensions.DI;

public static class ServiceConfigurationExtensions
{
    public static IServiceCollection AddCustomService(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        // MediatR + FluentValidationBehavior
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        // FluentValidation
        services.AddValidatorsFromAssemblyContaining<AssemblyMarker>();



        return services;
    }
}
