using Application.Features.TeacherSchedule.Queries;
using ClientScheduleApi.Extensions.Other;
using Domain.Model.TargetEntity.DaySchedule;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClientScheduleApi.Endpoints;

public static class ScheduleEndpoints
{
     public static IEndpointRouteBuilder ScheduleService(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("Schedule")
            .WithDescription("Получение расписания");


        group.MapGet("/teacher/week", GetTeacherWeekSchedule)
            .WithTags("Расписание преподавателя на след N дней")
            .Produces<List<DayShceduleDTO>>(200)
            .Produces(500)
            .Produces(400)
            .Produces(429);

        group.MapGet("/teacher/day", GetTeacherDaySchedule)
            .WithTags("Расписания преподавателя на конкретный день")
            .Produces<DayShceduleDTO>(200)
            .Produces(500)
            .Produces(400)
            .Produces(429);


        group.MapGet("/group/day", GetGroupDaySchedule)
            .WithTags("Расписания группы на конкретный день")
            .Produces<DayShceduleDTO>(200)
            .Produces(500)
            .Produces(400)
            .Produces(429);


        group.MapGet("/group/week", GetGroupWeekSchedule)
            .WithTags("Расписание группы на след N дней")
            .Produces<List<DayShceduleDTO>>(200)
            .Produces(500)
            .Produces(400)
            .Produces(429);

        group.MapGet("", GetFullSchedule)
            .WithTags("Полное расписание на день")
            .WithSummary("Самаранта")
            .WithDescription("grwhui")
            .WithName("Имя")
            .Produces<List<DayShceduleDTO>>(200)
            .Produces(500)
            .Produces(429);

        return group;
    }


    public static async Task<IResult> GetTeacherWeekSchedule(
        [FromServices]Mediator mediatR,
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
