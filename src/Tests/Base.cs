// Tests/IntegrationTestBase.cs
using System;
using System.Threading.Tasks;
using Application.Abstraction.DataBase;
using Application.Features.AllGroup.Queries;
using Application.Features.AllTeacher.Queries;
using Domain.Model.Entity;
using Infrastructure.DataBase.Context;
using Infrastructure.DataBase.Repository.Base;
using Infrastructure.DataBase.Repository.Custom;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

public abstract class IntegrationTestBase : IAsyncLifetime
{
    protected readonly AppDbContext _dbContext;
    private readonly SqliteConnection _sqliteConnection;

    protected IntegrationTestBase()
    {
        _sqliteConnection = new SqliteConnection("Filename=:memory:");
        _sqliteConnection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_sqliteConnection)
            .Options;

        _dbContext = new AppDbContext(options);
        _dbContext.Database.EnsureCreated();
    }

    protected ICommonInfoRepository GetCommonRepo() =>
        new CommonInfoRepository(_dbContext);

    protected IBaseRepository<TeacherEntity> GetTeacherRepo() =>
        new BaseRepository<TeacherEntity>(_dbContext);

    protected GetAllGroupHandler CreateHandler() =>
        new GetAllGroupHandler(GetCommonRepo());

    protected GetAllTeacherHandler CreateTeacherHandler() =>
        new GetAllTeacherHandler(GetTeacherRepo());

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        _dbContext?.Dispose();
        _sqliteConnection?.Dispose();
        await Task.CompletedTask;
    }
}
