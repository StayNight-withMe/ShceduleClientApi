using Application.Abstraction.DataBase;
using Application.Features.GroupWeekSchedule.Queries;
using Domain.Model.Entity;
using Domain.Specification;
using Moq;

namespace Tests.Features;

public class GetGroupWeekScheduleHandlerTests
{
    private readonly Mock<IBaseRepository<LessonEntity>> _lessonRepoMock;
    private readonly GetGroupWeekScheduleHandler _handler;

    public GetGroupWeekScheduleHandlerTests()
    {
        _lessonRepoMock = new Mock<IBaseRepository<LessonEntity>>();

        _handler = new GetGroupWeekScheduleHandler(
            _lessonRepoMock.Object
        );
    }

    [Fact]
    public async Task Handle_WithWeekLessons_ReturnsWeekSchedule()
    {
        // === ARRANGE ===
        var groupName = "П32";
        var date = DateOnly.FromDateTime(DateTime.Today);
        var request = new GetGroupWeekScheduleQuery(groupName, date);

        var dayScheduleEntity1 = new DayScheduleEntity
        {
            Id = Guid.NewGuid(),
            GroupName = groupName,
            Date = date.AddDays(-2)
        };

        var dayScheduleEntity2 = new DayScheduleEntity
        {
            Id = Guid.NewGuid(),
            GroupName = groupName,
            Date = date.AddDays(-1)
        };

        var lessonEntities = new List<LessonEntity>
        {
            new()
            {
                Subject1 = "Математика",
                Teacher1 = "Иванов И.И.",
                Classroom1 = "101",
                StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(9)),
                EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(10, 35)),
                LessonNumber = 1,
                daySchedule = dayScheduleEntity1
            },
            new()
            {
                Subject1 = "Физика",
                Teacher1 = "Петров П.П.",
                Classroom1 = "205",
                StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(11)),
                EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(12, 35)),
                LessonNumber = 2,
                daySchedule = dayScheduleEntity1
            },
            new()
            {
                Subject1 = "Химия",
                Teacher1 = "Сидоров С.С.",
                Classroom1 = "301",
                StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(13)),
                EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(14, 35)),
                LessonNumber = 3,
                daySchedule = dayScheduleEntity2
            }
        };

        _lessonRepoMock.Setup(r => r.ListAsync(
            It.IsAny<GetGroupWeekScheduleSpec>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(lessonEntities);

        // === ACT ===
        var response = await _handler.Handle(request, CancellationToken.None);

        // === ASSERT ===
        Assert.True(response.IsCompleted);
        Assert.NotNull(response.Value);
        Assert.NotNull(response.Value.Schedule);
        Assert.Equal(2, response.Value.Schedule.Count);

        var firstDay = response.Value.Schedule.First(d => d.Date == date.AddDays(-2));
        Assert.Equal(2, firstDay.Lessons.Count);
        Assert.Equal("Математика", firstDay.Lessons[0].Lesson1);
        Assert.Equal("Физика", firstDay.Lessons[1].Lesson1);

        var secondDay = response.Value.Schedule.First(d => d.Date == date.AddDays(-1));
        Assert.Single(secondDay.Lessons);
        Assert.Equal("Химия", secondDay.Lessons[0].Lesson1);
    }

    [Fact]
    public async Task Handle_WithNoLessons_ReturnsEmptySchedule()
    {
        // === ARRANGE ===
        var groupName = "П99";
        var date = DateOnly.FromDateTime(DateTime.Today);
        var request = new GetGroupWeekScheduleQuery(groupName, date);

        var emptyLessonsList = new List<LessonEntity>();

        _lessonRepoMock.Setup(r => r.ListAsync(
            It.IsAny<GetGroupWeekScheduleSpec>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(emptyLessonsList);

        // === ACT ===
        var response = await _handler.Handle(request, CancellationToken.None);

        // === ASSERT ===
        Assert.True(response.IsCompleted);
        Assert.NotNull(response.Value);
        Assert.NotNull(response.Value.Schedule);
        Assert.Empty(response.Value.Schedule);
    }

    [Fact]
    public async Task Handle_WithSingleDayLessons_ReturnsSingleDaySchedule()
    {
        // === ARRANGE ===
        var groupName = "П45";
        var date = DateOnly.FromDateTime(DateTime.Today);
        var request = new GetGroupWeekScheduleQuery(groupName, date);

        var dayScheduleEntity = new DayScheduleEntity
        {
            Id = Guid.NewGuid(),
            GroupName = groupName,
            Date = date
        };

        var lessonEntities = new List<LessonEntity>
        {
            new()
            {
                Subject1 = "Программирование",
                Teacher1 = "Алексеев А.А.",
                Classroom1 = "404",
                StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(9)),
                EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(10, 35)),
                LessonNumber = 1,
                daySchedule = dayScheduleEntity
            },
            new()
            {
                Subject1 = "Базы данных",
                Teacher1 = "Борисов Б.Б.",
                Classroom1 = "405",
                StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(11)),
                EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(12, 35)),
                LessonNumber = 2,
                daySchedule = dayScheduleEntity
            }
        };

        _lessonRepoMock.Setup(r => r.ListAsync(
            It.IsAny<GetGroupWeekScheduleSpec>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(lessonEntities);

        // === ACT ===
        var response = await _handler.Handle(request, CancellationToken.None);

        // === ASSERT ===
        Assert.True(response.IsCompleted);
        Assert.NotNull(response.Value);
        Assert.NotNull(response.Value.Schedule);
        Assert.Single(response.Value.Schedule);
        Assert.Equal(date, response.Value.Schedule[0].Date);
        Assert.Equal(groupName, response.Value.Schedule[0].Group);
        Assert.Equal(2, response.Value.Schedule[0].Lessons.Count);
    }

    [Fact]
    public async Task Handle_WithMultipleDaysLessons_ReturnsAllDaysSchedule()
    {
        // === ARRANGE ===
        var groupName = "П67";
        var date = DateOnly.FromDateTime(DateTime.Today);
        var request = new GetGroupWeekScheduleQuery(groupName, date);

        var dayScheduleEntity1 = new DayScheduleEntity
        {
            Id = Guid.NewGuid(),
            GroupName = groupName,
            Date = date.AddDays(-3)
        };

        var dayScheduleEntity2 = new DayScheduleEntity
        {
            Id = Guid.NewGuid(),
            GroupName = groupName,
            Date = date.AddDays(-2)
        };

        var dayScheduleEntity3 = new DayScheduleEntity
        {
            Id = Guid.NewGuid(),
            GroupName = groupName,
            Date = date.AddDays(-1)
        };

        var lessonEntities = new List<LessonEntity>
        {
            new()
            {
                Subject1 = "Математика",
                Teacher1 = "Иванов И.И.",
                Classroom1 = "101",
                StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(9)),
                EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(10, 35)),
                LessonNumber = 1,
                daySchedule = dayScheduleEntity1
            },
            new()
            {
                Subject1 = "Физика",
                Teacher1 = "Петров П.П.",
                Classroom1 = "205",
                StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(11)),
                EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(12, 35)),
                LessonNumber = 2,
                daySchedule = dayScheduleEntity2
            },
            new()
            {
                Subject1 = "Химия",
                Teacher1 = "Сидоров С.С.",
                Classroom1 = "301",
                StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(13)),
                EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(14, 35)),
                LessonNumber = 3,
                daySchedule = dayScheduleEntity3
            }
        };

        _lessonRepoMock.Setup(r => r.ListAsync(
            It.IsAny<GetGroupWeekScheduleSpec>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(lessonEntities);

        // === ACT ===
        var response = await _handler.Handle(request, CancellationToken.None);

        // === ASSERT ===
        Assert.True(response.IsCompleted);
        Assert.NotNull(response.Value);
        Assert.NotNull(response.Value.Schedule);
        Assert.Equal(3, response.Value.Schedule.Count);

        Assert.Equal(date.AddDays(-3), response.Value.Schedule[0].Date);
        Assert.Equal(date.AddDays(-2), response.Value.Schedule[1].Date);
        Assert.Equal(date.AddDays(-1), response.Value.Schedule[2].Date);
    }
}
