using System.Runtime.CompilerServices;
using Application.Abstraction.DataBase;
using Infrastructure.DataBase.Context;
using Infrastructure.DataBase.Repository.Base;
using Infrastructure.DataBase.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ClientScheduleApi.Extensions.DI;

public static class DataBaseConfiguration
{
    public static IServiceCollection AddDataBaseDependency(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );

        // UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Repository
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        return services;
    }
}
