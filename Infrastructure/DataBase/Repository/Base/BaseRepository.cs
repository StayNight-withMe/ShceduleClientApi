using Application.Abstraction.DataBase;
using Ardalis.Specification.EntityFrameworkCore;
using Infrastructure.DataBase.Context;

namespace Infrastructure.DataBase.Repository.Base;

public class BaseRepository<T> : RepositoryBase<T>, IBaseRepository<T> where T : class
{
    public BaseRepository(AppDbContext AppDbContext) : base(AppDbContext) { }
}
