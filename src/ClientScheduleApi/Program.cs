using ClientScheduleApi.Extensions.DI;
using Infrastructure.DataBase.Context;
using Scalar.AspNetCore;



var builder = WebApplication.CreateBuilder(args);




builder.Services.AddOpenApi();
builder.Services.AddControllers();


builder.Services.AddCustomService(builder.Configuration);

builder.Services.AddMessaging();

builder.Services.AddDataBaseDependency(builder.Configuration);

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapCustomRoute();



app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRateLimiter();

app.MapControllers();

app.Run();
