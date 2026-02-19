using Application.Abstraction.DataBase;
using Domain.Common.CustomException;
using Infrastructure.DataBase.Context;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Infrastructure.DataBase.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
    public AppDbContext _context;
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    public async Task<int> CommitAsync(CancellationToken ct = default)
    {
        try
        {
           return await _context.SaveChangesAsync(ct);
        }
        catch(DbUpdateException ex) when (ex.InnerException is PostgresException pgEx)
        {
            throw new CustomDbException(pgEx.Message, pgEx.SqlState);
        }
         
    }
    public void Dispose() => _context.Dispose();
}
