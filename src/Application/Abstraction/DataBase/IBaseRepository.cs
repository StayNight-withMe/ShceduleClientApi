

using Ardalis.Specification;

namespace Application.Abstraction.DataBase;
public interface IBaseRepository<T> : IRepositoryBase<T> where T : class
{
}
