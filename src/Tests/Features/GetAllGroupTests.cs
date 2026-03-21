using Application.Features.AllGroup.Queries;
using Domain.Model.Entities;

namespace Tests.Features;

public class GetAllGroupHandlerTests : IntegrationTestBase
{
    [Fact]
    public async Task Handle_WithGroupsAndSpecialties_ReturnsDictionary()
    {
        // ARRANGE 
        var specialty1 = new SpecialtyEntity { Name = "РИСПОИС" };
        var specialty2 = new SpecialtyEntity { Name = "ПОИТ" };

        _dbContext.Specialty.AddRange(specialty1, specialty2);
        await _dbContext.SaveChangesAsync();

        var groups = new List<GroupEntity>
        {
            new() { Name = "П32", SpecialtyId = specialty1.Id },
            new() { Name = "П33", SpecialtyId = specialty1.Id },
            new() { Name = "П12", SpecialtyId = specialty2.Id },
        };

        _dbContext.Group.AddRange(groups);
        await _dbContext.SaveChangesAsync();

        // ACT
        var handler = CreateHandler();
        var query = new GetAllGroupQuery();
        var response = await handler.Handle(query, CancellationToken.None);

        // ASSERT 
        Assert.True(response.IsCompleted);
        Assert.NotNull(response.Value);
        Assert.Equal(2, response.Value.Count);

        Assert.Contains("РИСПОИС", response.Value.Keys);
        var rispoisGroups = response.Value["РИСПОИС"];
        Assert.Equal(2, rispoisGroups.Count);
        Assert.Contains("П32", rispoisGroups);
        Assert.Contains("П33", rispoisGroups);

        Assert.Contains("ПОИТ", response.Value.Keys);
        var poitGroups = response.Value["ПОИТ"];
        Assert.Single(poitGroups);
        Assert.Contains("П12", poitGroups);
    }

    [Fact]
    public async Task Handle_WithNoGroups_ReturnsEmptyDictionary()
    {
        // ARRANGE

        // ACT 
        var handler = CreateHandler();
        var response = await handler.Handle(new GetAllGroupQuery(), CancellationToken.None);

        // ASSERT 
        Assert.True(response.IsCompleted);
        Assert.NotNull(response.Value);
        Assert.Empty(response.Value);
    }

    [Fact]
    public async Task Handle_WithOneSpecialty_NoGroups_ReturnsEmptyForThatSpecialty()
    {
        // ARRANGE 
        var specialty = new SpecialtyEntity { Name = "РИСПОИС" };
        _dbContext.Specialty.Add(specialty);
        await _dbContext.SaveChangesAsync();

        // ACT 
        var handler = CreateHandler();
        var response = await handler.Handle(new GetAllGroupQuery(), CancellationToken.None);

        // ASSERT 
        Assert.True(response.IsCompleted);
        Assert.NotNull(response.Value);
        Assert.Empty(response.Value);
    }

    [Fact]
    public async Task Handle_WithManyGroups_SameSpecialty_GroupsCorrectly()
    {
        // ARRANGE 
        var specialty = new SpecialtyEntity { Name = "ТЕСТ" };
        _dbContext.Specialty.Add(specialty);
        await _dbContext.SaveChangesAsync();

        var groups = new List<GroupEntity>
        {
            new() { Name = "Группа-1", SpecialtyId = specialty.Id },
            new() { Name = "Группа-2", SpecialtyId = specialty.Id },
            new() { Name = "Группа-3", SpecialtyId = specialty.Id },
        };
        _dbContext.Group.AddRange(groups);
        await _dbContext.SaveChangesAsync();

        // ACT 
        var handler = CreateHandler();
        var response = await handler.Handle(new GetAllGroupQuery(), CancellationToken.None);

        // ASSERT 
        Assert.True(response.IsCompleted);
        Assert.NotNull(response.Value);
        Assert.Single(response.Value);
        Assert.Contains("ТЕСТ", response.Value.Keys);
        Assert.Equal(3, response.Value["ТЕСТ"].Count);
    }
}
