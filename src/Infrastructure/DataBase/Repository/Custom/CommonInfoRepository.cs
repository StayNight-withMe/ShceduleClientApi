
using Application.Abstraction.DataBase;
using Ardalis.Specification;
using Domain.Model.Entities;
using Infrastructure.DataBase.Context;
using Infrastructure.DataBase.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase.Repository.Custom;

public class CommonInfoRepository : BaseRepository<GroupEntity>, ICommonInfoRepository
{
    public CommonInfoRepository(AppDbContext AppDbContext) : base(AppDbContext) { }

    public async Task<Dictionary<string, List<string>>> GetAllGroup() 
    {

        return await DbContext.Set<GroupEntity>()
            .AsNoTracking()
            .Include(c => c.Specialty)
            .Where(c => c.Specialty != null)
            .GroupBy(c => c.Specialty.Name)
            .Select(c => new
            {
                SpecName = c.Key,
                GroupNames = c.Select(x => x.Name).ToList()
            })
            .ToDictionaryAsync(c => c.SpecName, x => x.GroupNames);

        
    }


}
