using Domain.Model.Entities;

namespace Application.Abstraction.DataBase;

public interface ICommonInfoRepository : IBaseRepository<GroupEntity>
{
    Task<Dictionary<string, List<string>>> GetAllGroup();
}
