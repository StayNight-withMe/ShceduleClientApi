using Application.Abstraction.DataBase;
using Application.Features.ScheduleSave;
using AutoMapper;
using Contracts.Common;
using Contracts.Schedules;
using Domain.Common.CustomException;
using Domain.Model.Entity;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.Features;

public class DayScheduleHandlerTests
{
    private readonly Mock<ILogger<ScheduleSaveHandler>> _loggerMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IBaseRepository<DayScheduleEntity>> _scheduleRepoMock;
    private readonly Mock<IBaseRepository<LessonEntity>> _lessonRepoMock;
    private readonly ScheduleSaveHandler _handler;

    public DayScheduleHandlerTests()
    {
        _loggerMock = new Mock<ILogger<ScheduleSaveHandler>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _scheduleRepoMock = new Mock<IBaseRepository<DayScheduleEntity>>();
        _lessonRepoMock = new Mock<IBaseRepository<LessonEntity>>();


        _handler = new ScheduleSaveHandler(
            _loggerMock.Object,
            _scheduleRepoMock.Object,
            _lessonRepoMock.Object,
            _unitOfWorkMock.Object,
            _mapperMock.Object

        );
    }

    [Fact]
    public async Task Handle_ValidRequest_MapsAndAddsEntities()
    {
        // === ARRANGE ===
        var request = new DayScheduleDTO
        {
            Group = "П32",
            Date = DateOnly.FromDateTime(DateTime.Today),
            Lessons = new List<Lesson>
            {
                new()
                {
                    StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(9)),
                    EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(10, 35)),
                    Lesson1 = "Математика",
                    Lesson2 = "",
                    Fio1 = "Иванов И.И.",
                    Fio2 = "",
                    ClassRoom1 = "101",
                    ClassRoom2 = ""
                }
            }
        };

        var scheduleId = Guid.NewGuid();
        var groupEntity = new DayScheduleEntity
        {
            Id = scheduleId,
            GroupName = request.Group,
            Date = request.Date
        };

        var lessonEntity = new LessonEntity
        {
            Subject1 = request.Lessons[0].Lesson1,
            Teacher1 = request.Lessons[0].Fio1,
            Classroom1 = request.Lessons[0].ClassRoom1,
            StartTime = request.Lessons[0].StartTime,
            EndTime = request.Lessons[0].EndTime,
            LessonNumber = 1
        };

        _mapperMock.Setup(m => m.Map<DayScheduleEntity>(request)).Returns(groupEntity);
        _mapperMock.Setup(m => m.Map<List<LessonEntity>>(request.Lessons))
            .Returns(new List<LessonEntity> { lessonEntity });

        // === ACT ===
        await _handler.Handle(request, CancellationToken.None);

        // === ASSERT ===

        // 1. Маппинг вызван
        _mapperMock.Verify(m => m.Map<DayScheduleEntity>(request), Times.Once);
        _mapperMock.Verify(m => m.Map<List<LessonEntity>>(request.Lessons), Times.Once);


        _scheduleRepoMock.Verify(r => r.AddAsync(
            It.Is<DayScheduleEntity>(e => e.Id == scheduleId && e.GroupName == "П32"),
            It.IsAny<CancellationToken>()), Times.Once);

        _lessonRepoMock.Verify(r => r.AddAsync(
            It.Is<LessonEntity>(l => l.Groupid == scheduleId),
            It.IsAny<CancellationToken>()), Times.Once);


        _unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
    }

    [Fact]
    public async Task Handle_EmptyLessonsList_WorksCorrectly()
    {
        // === ARRANGE ===
        var request = new DayScheduleDTO
        {
            Group = "П33",
            Date = DateOnly.FromDateTime(DateTime.Today),
            Lessons = new List<Lesson>() // Пустой список
        };

        var groupEntity = new DayScheduleEntity { Id = Guid.NewGuid() };

        _mapperMock.Setup(m => m.Map<DayScheduleEntity>(request)).Returns(groupEntity);
        _mapperMock.Setup(m => m.Map<List<LessonEntity>>(request.Lessons))
            .Returns(new List<LessonEntity>());

        // === ACT ===
        await _handler.Handle(request, CancellationToken.None);

        // === ASSERT ===
        _scheduleRepoMock.Verify(r => r.AddAsync(groupEntity, It.IsAny<CancellationToken>()), Times.Once);

        _lessonRepoMock.Verify(r => r.AddAsync(It.IsAny<LessonEntity>(), It.IsAny<CancellationToken>()), Times.Never);

        _unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
    }

    [Fact]
    public async Task Handle_CustomDbException_ThrowsAndLogs()
    {
        // === ARRANGE ===
        var request = new DayScheduleDTO
        {
            Group = "П12",
            Date = DateOnly.FromDateTime(DateTime.Today),
            Lessons = new List<Lesson> { new() { Lesson1 = "Тест" } }
        };

        var groupEntity = new DayScheduleEntity { Id = Guid.NewGuid() };
        var lessonEntity = new LessonEntity { Subject1 = "Тест" };

        _mapperMock.Setup(m => m.Map<DayScheduleEntity>(request)).Returns(groupEntity);
        _mapperMock.Setup(m => m.Map<List<LessonEntity>>(request.Lessons))
            .Returns(new List<LessonEntity> { lessonEntity });

        _unitOfWorkMock.SetupSequence(u => u.CommitAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(2))
            .ThrowsAsync(new CustomDbException("Ошибка БД при сохранении", "1")
            {

            });

        // === ACT & ASSERT ===
        var exception = await Assert.ThrowsAsync<CustomDbException>(
            () => _handler.Handle(request, CancellationToken.None));

        Assert.Equal("Ошибка БД при сохранении", exception.Message);

        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) =>
                    v.ToString().Contains("message to broker", StringComparison.OrdinalIgnoreCase)),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);

        _unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
    }

    [Fact]
    public async Task Handle_MultipleLessons_AllLinkedToSameSchedule()
    {
        // === ARRANGE ===
        var request = new DayScheduleDTO
        {
            Group = "П32",
            Date = DateOnly.FromDateTime(DateTime.Today),
            Lessons = new List<Lesson>
            {
                new() { Lesson1 = "Урок 1", StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(9)) },
                new() { Lesson1 = "Урок 2", StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(11)) },
                new() { Lesson1 = "Урок 3", StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(13)) },
            }
        };

        var scheduleId = Guid.NewGuid();
        var groupEntity = new DayScheduleEntity { Id = scheduleId };

        var lessonEntities = request.Lessons.Select(l => new LessonEntity { Subject1 = l.Lesson1 }).ToList();

        _mapperMock.Setup(m => m.Map<DayScheduleEntity>(request)).Returns(groupEntity);
        _mapperMock.Setup(m => m.Map<List<LessonEntity>>(request.Lessons)).Returns(lessonEntities);

        // === ACT ===
        await _handler.Handle(request, CancellationToken.None);

        // === ASSERT ===

        _lessonRepoMock.Verify(r => r.AddAsync(
            It.Is<LessonEntity>(l => l.Groupid == scheduleId && l.Subject1 == "Урок 1"),
            It.IsAny<CancellationToken>()), Times.Once);

        _lessonRepoMock.Verify(r => r.AddAsync(
            It.Is<LessonEntity>(l => l.Groupid == scheduleId && l.Subject1 == "Урок 2"),
            It.IsAny<CancellationToken>()), Times.Once);

        _lessonRepoMock.Verify(r => r.AddAsync(
            It.Is<LessonEntity>(l => l.Groupid == scheduleId && l.Subject1 == "Урок 3"),
            It.IsAny<CancellationToken>()), Times.Once);
    }
}
