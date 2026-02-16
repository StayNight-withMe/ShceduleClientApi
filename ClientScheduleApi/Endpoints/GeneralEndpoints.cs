namespace ClientScheduleApi.Endpoints;

public static class GeneralEndpoints
{
    public static IEndpointRouteBuilder GeneralService(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/general").WithTags("General");




        return group;
    }
}
