using Application.Abstraction.DataBase;
using Application.Features.TeacherSchedule.Queries;
using AutoMapper;
using Contracts.Common;
using Contracts.Schedules;
using Domain.Model.Entity;
using Domain.Specification;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.Features;

public class GetGroupDayScheduleHandlerTests
{
    private readonly Mock<ILogger<GetGroupDayScheduleHandler>> _loggerMock;
    private readonly Mock<IBaseRepository<LessonEntity>> _lessonRepoMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetGroupDayScheduleHandler _handler;

    public GetGroupDayScheduleHandlerTests()
    {
        _loggerMock = new Mock<ILogger<GetGroupDayScheduleHandler>>();
        _lessonRepoMock = new Mock<IBaseRepository<LessonEntity>>();
        _mapperMock = new Mock<IMapper>();

        _handler = new GetGroupDayScheduleHandler(
            _loggerMock.Object,
            _lessonRepoMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task Handle_WithGroupLessons_ReturnsDaySchedule()
    {
        // === ARRANGE ===
        var groupName = "П32";
        var date = DateOnly.FromDateTime(DateTime.Today);
        var request = new GetGroupDayScheduleQuery(groupName, date);

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
                Subject1 = "Математика",
                Teacher1 = "Иванов И.И.",
                Classroom1 = "101",
                StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(9)),
                EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(10, 35)),
                LessonNumber = 1,
                daySchedule = dayScheduleEntity
            },
            new()
            {
                Subject1 = "Физика",
                Teacher1 = "Петров П.П.",
                Classroom1 = "205",
                StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(11)),
                EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(12, 35)),
                LessonNumber = 2,
                daySchedule = dayScheduleEntity
            }
        };

        var dayScheduleDTO = new DayScheduleDTO
        {
            Group = groupName,
            Date = date,
            Lessons = new List<Lesson>
            {
                new()
                {
                    Lesson1 = "Математика",
                    Fio1 = "Иванов И.И.",
                    ClassRoom1 = "101",
                    StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(9)),
                    EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(10, 35))
                },
                new()
                {
                    Lesson1 = "Физика",
                    Fio1 = "Петров П.П.",
                    ClassRoom1 = "205",
                    StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(11)),
                    EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(12, 35))
                }
            }
        };

        _lessonRepoMock.Setup(r => r.ListAsync(
            It.IsAny<LessonsByGroupAndDateSpec>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(lessonEntities);

        _mapperMock.Setup(m => m.Map<List<Lesson>>(lessonEntities)).Returns(dayScheduleDTO.Lessons);
        _mapperMock.Setup(m => m.Map<DayScheduleDTO>(dayScheduleEntity)).Returns(dayScheduleDTO);

        // === ACT ===
        var response = await _handler.Handle(request, CancellationToken.None);

        // === ASSERT ===
        Assert.True(response.IsCompleted);
        Assert.NotNull(response.Value);
        Assert.NotNull(response.Value.Shcedule);
        Assert.Equal(groupName, response.Value.Shcedule.Group);
        Assert.Equal(2, response.Value.Shcedule.Lessons.Count);
    }

    [Fact]
    public async Task Handle_WithNoLessons_ThrowsException()
    {
        // === ARRANGE ===
        var groupName = "П99";
        var date = DateOnly.FromDateTime(DateTime.Today);
        var request = new GetGroupDayScheduleQuery(groupName, date);

        var emptyLessonsList = new List<LessonEntity>();

        _lessonRepoMock.Setup(r => r.ListAsync(
            It.IsAny<LessonsByGroupAndDateSpec>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(emptyLessonsList);

        // === ACT & ASSERT ===
        // Если уроков нет, первая же операция .First() выбросит InvalidOperationException
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithSingleLesson_ReturnsSingleLessonSchedule()
    {
        // === ARRANGE ===
        var groupName = "П45";
        var date = DateOnly.FromDateTime(DateTime.Today);
        var request = new GetGroupDayScheduleQuery(groupName, date);

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
                Subject1 = "Химия",
                Teacher1 = "Сидоров С.С.",
                Classroom1 = "301",
                StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(13)),
                EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(14, 35)),
                LessonNumber = 3,
                daySchedule = dayScheduleEntity
            }
        };

        var dayScheduleDTO = new DayScheduleDTO
        {
            Group = groupName,
            Date = date,
            Lessons = new List<Lesson>
            {
                new()
                {
                    Lesson1 = "Химия",
                    Fio1 = "Сидоров С.С.",
                    ClassRoom1 = "301",
                    StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(13)),
                    EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(14, 35))
                }
            }
        };

        _lessonRepoMock.Setup(r => r.ListAsync(
            It.IsAny<LessonsByGroupAndDateSpec>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(lessonEntities);

        _mapperMock.Setup(m => m.Map<List<Lesson>>(lessonEntities)).Returns(dayScheduleDTO.Lessons);
        _mapperMock.Setup(m => m.Map<DayScheduleDTO>(dayScheduleEntity)).Returns(dayScheduleDTO);

        // === ACT ===
        var response = await _handler.Handle(request, CancellationToken.None);

        // === ASSERT ===
        Assert.True(response.IsCompleted);
        Assert.NotNull(response.Value);
        Assert.NotNull(response.Value.Shcedule);
        Assert.Equal(groupName, response.Value.Shcedule.Group);
        Assert.Single(response.Value.Shcedule.Lessons);
    }

    [Fact]
    public async Task Handle_WithMultipleLessons_ReturnsAllLessons()
    {
        // === ARRANGE ===
        var groupName = "П67";
        var date = DateOnly.FromDateTime(DateTime.Today);
        var request = new GetGroupDayScheduleQuery(groupName, date);

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
            },
            new()
            {
                Subject1 = "Сети",
                Teacher1 = "Васильев В.В.",
                Classroom1 = "406",
                StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(13)),
                EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(14, 35)),
                LessonNumber = 3,
                daySchedule = dayScheduleEntity
            }
        };

        var dayScheduleDTO = new DayScheduleDTO
        {
            Group = groupName,
            Date = date,
            Lessons = new List<Lesson>
            {
                new()
                {
                    Lesson1 = "Программирование",
                    Fio1 = "Алексеев А.А.",
                    ClassRoom1 = "404",
                    StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(9)),
                    EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(10, 35))
                },
                new()
                {
                    Lesson1 = "Базы данных",
                    Fio1 = "Борисов Б.Б.",
                    ClassRoom1 = "405",
                    StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(11)),
                    EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(12, 35))
                },
                new()
                {
                    Lesson1 = "Сети",
                    Fio1 = "Васильев В.В.",
                    ClassRoom1 = "406",
                    StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(13)),
                    EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(14, 35))
                }
            }
        };

        _lessonRepoMock.Setup(r => r.ListAsync(
            It.IsAny<LessonsByGroupAndDateSpec>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(lessonEntities);

        _mapperMock.Setup(m => m.Map<List<Lesson>>(lessonEntities)).Returns(dayScheduleDTO.Lessons);
        _mapperMock.Setup(m => m.Map<DayScheduleDTO>(dayScheduleEntity)).Returns(dayScheduleDTO);

        // === ACT ===
        var response = await _handler.Handle(request, CancellationToken.None);

        // === ASSERT ===
        Assert.True(response.IsCompleted);
        Assert.NotNull(response.Value);
        Assert.NotNull(response.Value.Shcedule);
        Assert.Equal(groupName, response.Value.Shcedule.Group);
        Assert.Equal(3, response.Value.Shcedule.Lessons.Count);
    }
}
