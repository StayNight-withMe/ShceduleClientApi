using Application.Features.GeneralData;
using MediatR;
using ClientScheduleApi.Extensions.Other;
using Application.Features.AllGroup.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ClientScheduleApi.Endpoints;

public static class DataEndpoints
{
    public static IEndpointRouteBuilder DataService(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/api/data")
            .WithTags("Общие данные")
             .WithDescription("получение общих данных");

        group.MapGet("/teachers", GetAllTeachers)
            .WithTags("Все преподаватели");

        group.MapGet("/groups", GetAllGroups)
           .WithTags("Все группы");

        return group;
    }



    public static async Task<IResult> GetAllTeachers(
        [FromServices]IMediator mediator
        )
    {
        var result = await mediator.Send(new GetAllTeacherQuery());

        return result.ToApiResult();
    }


    public static async Task<IResult> GetAllGroups(
        [FromServices]IMediator mediator
        )
    {
        var result = await mediator.Send(new GetAllGroupQuery());

        return result.ToApiResult();
    }

}
