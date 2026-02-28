using Application.Abstraction.DataBase;
using Domain.Model.Entities;
using Domain.Model.ReturnEntity;
using Domain.Specification;
using MediatR;

namespace Application.Features.AllGroup.Queries;
public class GetAllGroupHandler : IRequestHandler<GetAllGroupQuery, TResult<Dictionary<string, List<string>>>>
{
    public readonly IBaseRepository<GroupEntity> _groupRepo;
    public GetAllGroupHandler(IBaseRepository<GroupEntity> groupRepo)
    {
        _groupRepo = groupRepo;
    }

    public async Task<TResult<Dictionary<string, List<string>>>> Handle(
        GetAllGroupQuery request,
        CancellationToken ct = default) 
    {
        var dtoS = await _groupRepo.ListAsync(new AllGroupSpec());

        var result =  dtoS.ToDictionary(c => c.Speciality.Name, c => new List<string>());

        foreach(var item in result)
        {
            Console.WriteLine("итерация");
            Console.WriteLine(item.Key);
            Console.WriteLine(item.Value);
        }

        return TResult<Dictionary<string, List<string>>>.CompletedOperation(null);
    }

}

