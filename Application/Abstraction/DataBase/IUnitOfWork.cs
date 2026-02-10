namespace Application.Abstraction.DataBase;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
}
