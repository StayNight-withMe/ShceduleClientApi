using Application.Features.GroupWeekSchedule.Queries;
using Application.Features.TeacherSchedule.Queries;
using ClientScheduleApi.Extensions.Other;
using Contracts.Schedules;
using Domain.Model.ReturnEntity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClientScheduleApi.Endpoints;

public static class ScheduleEndpoints
{
    public static IEndpointRouteBuilder ScheduleService(this IEndpointRouteBuilder builder)
    {
        var group = builder
            .MapGroup("Schedule")
            .RequireRateLimiting("DefaultLimiter")
            .WithTags("Расписание");

        group.MapGet("/teacher/week", GetTeacherWeekSchedule)
            .WithSummary("Расписание преподавателя на след. N дней")
            .WithDescription("Позволяет пользователю получить расписание преподавателя на указанное количество дней вперед")
            .WithName("GetTeacherWeekSchedule")
            .Produces<List<DayScheduleDTO>>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .Produces<ErrorResponse>(StatusCodes.Status429TooManyRequests)
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);

        group.MapGet("/teacher/day", GetTeacherDaySchedule)
            .WithSummary("Расписания преподавателя на конкретный день")
            .WithDescription("Позволяет пользователю получить расписание преподавателя на конкретный день")
            .WithName("GetTeacherDaySchedule")
            .Produces<DayScheduleDTO>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .Produces<ErrorResponse>(StatusCodes.Status429TooManyRequests)
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);

        group.MapGet("/group/day", GetGroupDaySchedule)
            .WithSummary("Расписания группы на конкретный день")
            .WithDescription("Позволяет пользователю получить расписание группы на конкретный день")
            .WithName("GetGroupDaySchedule")
            .Produces<DayScheduleDTO>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .Produces<ErrorResponse>(StatusCodes.Status429TooManyRequests)
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);

        group.MapGet("/group/week", GetGroupWeekSchedule)
            .WithSummary("Расписание группы на след. N дней")
            .WithDescription("Позволяет пользователю получить расписание группы на указанное количество дней вперед")
            .WithName("GetGroupWeekSchedule")
            .Produces<List<DayScheduleDTO>>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .Produces<ErrorResponse>(StatusCodes.Status429TooManyRequests)
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);

        group.MapGet("", GetFullSchedule)
            .WithSummary("Полное расписание на день")
            .WithDescription("Позволяет пользователю получить полное расписание на день")
            .WithName("GetFullSchedule")
            .Produces<List<DayScheduleDTO>>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .Produces<ErrorResponse>(StatusCodes.Status429TooManyRequests)
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);

        return group;
    }

    public static async Task<IResult> GetTeacherWeekSchedule(
        [FromServices] Mediator mediatR,
        [AsParameters] GetTeacherWeekScheduleQuery query
    )
    {
        var result = await mediatR.Send(query);

        return result.ToApiResult();
    }


    public static async Task<IResult> GetTeacherDaySchedule(
        [FromServices] Mediator mediatR,
        [AsParameters] GetTeacherDayScheduleQuery query
    )
    {
        var result = await mediatR.Send(query);

        return result.ToApiResult();
    }

    public static async Task<IResult> GetGroupDaySchedule(
        [FromServices] Mediator mediatR,
        [AsParameters] GetGroupDayScheduleQuery query
    )
    {
        var result = await mediatR.Send(query);

        return result.ToApiResult();
    }

    public static async Task<IResult> GetGroupWeekSchedule(
        [FromServices] Mediator mediatR,
        [AsParameters] GetGroupWeekScheduleQuery query
    )
    {
        var result = await mediatR.Send(query);

        return result.ToApiResult();
    }


    public static async Task<IResult> GetFullSchedule(
        [FromServices] Mediator mediatR,
        [AsParameters] GetFullWeekScheduleQuery query
    )
    {
        var result = await mediatR.Send(query);

        return result.ToApiResult();
    }
}
