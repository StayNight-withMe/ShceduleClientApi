using Application.Features.GeneralData;
using Domain.Model.Entity;

namespace Tests.Features;

public class GetAllTeacherHandlerTests : IntegrationTestBase
{
    [Fact]
    public async Task Handle_WithTeachers_ReturnsOrderedList()
    {
        // ARRANGE
        var teachers = new List<TeacherEntity>
        {
            new() { FullName = "Иванов И.И." },
            new() { FullName = "Петров П.П." },
            new() { FullName = "Сидоров С.С." },
        };

        _dbContext.Teacher.AddRange(teachers);
        await _dbContext.SaveChangesAsync();

        // ACT
        var handler = CreateTeacherHandler();
        var query = new GetAllTeacherQuery();
        var response = await handler.Handle(query, CancellationToken.None);

        // ASSERT
        Assert.True(response.IsCompleted);
        Assert.NotNull(response.Value);
        Assert.Equal(3, response.Value.Count);
        Assert.Equal("Иванов И.И.", response.Value[0]);
        Assert.Equal("Петров П.П.", response.Value[1]);
        Assert.Equal("Сидоров С.С.", response.Value[2]);
    }

    [Fact]
    public async Task Handle_WithNoTeachers_ReturnsEmptyList()
    {
        // ARRANGE

        // ACT
        var handler = CreateTeacherHandler();
        var response = await handler.Handle(new GetAllTeacherQuery(), CancellationToken.None);

        // ASSERT
        Assert.True(response.IsCompleted);
        Assert.NotNull(response.Value);
        Assert.Empty(response.Value);
    }

    [Fact]
    public async Task Handle_WithSingleTeacher_ReturnsSingleItemList()
    {
        // ARRANGE
        var teacher = new TeacherEntity { FullName = "Один Преподаватель" };
        _dbContext.Teacher.Add(teacher);
        await _dbContext.SaveChangesAsync();

        // ACT
        var handler = CreateTeacherHandler();
        var response = await handler.Handle(new GetAllTeacherQuery(), CancellationToken.None);

        // ASSERT
        Assert.True(response.IsCompleted);
        Assert.NotNull(response.Value);
        Assert.Single(response.Value);
        Assert.Equal("Один Преподаватель", response.Value[0]);
    }

    [Fact]
    public async Task Handle_WithTeachers_ReturnsOrderedListAlphabetically()
    {
        // ARRANGE
        var teachers = new List<TeacherEntity>
        {
            new() { FullName = "Яковлев Я.Я." },
            new() { FullName = "Алексеев А.А." },
            new() { FullName = "Борисов Б.Б." },
        };
        _dbContext.Teacher.AddRange(teachers);
        await _dbContext.SaveChangesAsync();

        // ACT
        var handler = CreateTeacherHandler();
        var response = await handler.Handle(new GetAllTeacherQuery(), CancellationToken.None);

        // ASSERT
        Assert.True(response.IsCompleted);
        Assert.Equal(3, response.Value?.Count);
        Assert.Equal("Алексеев А.А.", response.Value?[0]);
        Assert.Equal("Борисов Б.Б.", response.Value?[1]);
        Assert.Equal("Яковлев Я.Я.", response.Value?[2]);
    }
}
