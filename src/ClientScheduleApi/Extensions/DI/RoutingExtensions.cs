using ClientScheduleApi.Endpoints;

namespace ClientScheduleApi.Extensions.DI;

public static class RoutingExtensions
{
    public static void MapCustomRoute(this WebApplication app)
    {
        app.DataService();
        app.ScheduleService();
    }
}
