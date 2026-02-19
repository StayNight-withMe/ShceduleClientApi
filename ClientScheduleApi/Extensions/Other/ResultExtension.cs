using Domain.Common.Attributes;
using Domain.Model.ReturnEntity;

namespace ClientScheduleApi.Extensions.Other;

public static class ResultExtensions
{
    public static IResult ToApiResult(this EntityOfTResult result)
    {
    
        int statusCode = HttpStatusCodeAttribute.GetHttpStatusCode(result.ErrorCode);
        //var body = new
        //{
        //    error = Enum.GetName(typeof(ErrorCode), result.Error.Value) ?? "Unknown",
        //    message = result.Message,
        //    details = result.Details
        //};

        return statusCode switch
        {
            200 => Results.Ok(),
            400 => Results.BadRequest(),
            401 => Results.Unauthorized(),
            403 => Results.Forbid(),
            404 => Results.NotFound(),
            409 => Results.Conflict(),
            _ => Results.Json(statusCode),
        };
    }

    public static IResult ToApiResult<T>(this TResult<T> result)
    {
        if (result.IsCompleted)
            return Results.Ok(result.Value);

        return ((EntityOfTResult)result).ToApiResult();
    }
}
