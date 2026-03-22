using Application.Features.GeneralData;
using MediatR;
using ClientScheduleApi.Extensions.Other;
using Application.Features.AllGroup.Queries;
using Microsoft.AspNetCore.Mvc;
using Domain.Model.ReturnEntity;

namespace ClientScheduleApi.Endpoints;

public static class DataEndpoints
{
    public static IEndpointRouteBuilder DataService(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/api/data")
            .RequireRateLimiting("DefaultLimiter")
            .WithTags("Общие данные");

        group.MapGet("/teachers", GetAllTeachers)
            .WithSummary("Все преподаватели")
            .WithDescription("Позволяет пользователю получить список всех преподавателей")
            .WithName("GetAllTeachers")
            .Produces<List<string>>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status429TooManyRequests);

        group.MapGet("/groups", GetAllGroups)
            .WithSummary("Все группы")
            .WithDescription("Позволяет пользователю получить список всех специальностей и групп.")
            .WithName("GetAllGroups")
            .Produces<Dictionary<string, List<string>>>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status429TooManyRequests);

        return group;
    }

    public static async Task<IResult> GetAllTeachers(
        [FromServices] IMediator mediator
    )
    {
        var result = await mediator.Send(new GetAllTeacherQuery());

        return result.ToApiResult();
    }

    public static async Task<IResult> GetAllGroups(
        [FromServices] IMediator mediator
    )
    {
        var result = await mediator.Send(new GetAllGroupQuery());

        return result.ToApiResult();
    }
}
