// Tests/IntegrationTestBase.cs
using System;
using System.Threading.Tasks;
using Application.Abstraction.DataBase;
using Application.Features.AllGroup.Queries;
using Infrastructure.DataBase.Context;
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

    // 🔥 Только репозиторий — никакого Mediator!
    protected ICommonInfoRepository GetCommonRepo() =>
        new CommonInfoRepository(_dbContext);

    // 🔥 Хендлер создаём напрямую
    protected GetAllGroupHandler CreateHandler() =>
        new GetAllGroupHandler(GetCommonRepo());

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        _dbContext?.Dispose();
        _sqliteConnection?.Dispose();
        await Task.CompletedTask;
    }
}
