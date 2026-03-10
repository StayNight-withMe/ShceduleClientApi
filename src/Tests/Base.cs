// Tests/IntegrationTestBase.cs
using System;
using System.Threading.Tasks;
using Application;
using Application.Abstraction.DataBase;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using FluentValidation;
using Infrastructure.DataBase.Context;
using Infrastructure.DataBase.Repository.Base;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public abstract class IntegrationTestBase : IAsyncLifetime
{
    protected readonly IServiceProvider _serviceProvider;
    protected readonly AppDbContext _dbContext;
    private readonly SqliteConnection _sqliteConnection;

    protected IntegrationTestBase()
    {
        // 1. Создаем соединение SQLite в памяти
        _sqliteConnection = new SqliteConnection("Filename=:memory:");
        _sqliteConnection.Open();

        // 2. Настраиваем Options для EF Core
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_sqliteConnection)
            .Options;

        // 3. Создаем контекст и создаем схему БД (аналог миграций)
        _dbContext = new AppDbContext(options);
        _dbContext.Database.EnsureCreated();

        // 4. Настраиваем DI контейнер
        var services = new ServiceCollection();

        // Регистрируем DbContext (Scoped, как в реальном приложении)
        services.AddScoped<AppDbContext>(_ => _dbContext);

        // Регистрируем Ardalis Repository
        services.AddScoped(typeof(IBaseRepository<>), typeof(IBaseRepository<>));
        services.AddScoped(typeof(IReadRepositoryBase<>), typeof(IBaseRepository<>));

        // Регистрируем MediatR
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly));

        // Регистрируем FluentValidation (если используется в пайплайне)
        services.AddValidatorsFromAssemblyContaining<AssemblyMarker>();

        // Регистрируем другие сервисы из Application/Infrastructure, если нужны
        // services.AddScoped<IEmailSender, EmailSender>();

        _serviceProvider = services.BuildServiceProvider();
    }

    // Метод для получения Mediator из контейнера
    protected IMediator GetMediator()
    {
        return _serviceProvider.GetRequiredService<IMediator>();
    }

    // Метод для получения репозитория, если нужно подготовить данные напрямую
    protected IBaseRepository<T> GetRepository<T>() where T : class
    {
        return _serviceProvider.GetRequiredService<IBaseRepository<T>>();
    }

    // Очистка после каждого теста
    public async Task InitializeAsync()
    {
        // Можно добавить глобальный сидинг, если нужно
        await Task.CompletedTask;
    }

    // Уничтожение ресурсов
    public async Task DisposeAsync()
    {
        _dbContext.Dispose();
        _sqliteConnection.Dispose();
        await Task.CompletedTask;
    }
}
