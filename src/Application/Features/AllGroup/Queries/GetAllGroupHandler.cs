using Application.Abstraction.DataBase;
using Domain.Model.Entities;
using Domain.Model.ReturnEntity;
using Domain.Specification;
using MediatR;

namespace Application.Features.AllGroup.Queries;
public class GetAllGroupHandler : IRequestHandler<GetAllGroupQuery, TResult<Dictionary<string, List<string>>>>
{
    public readonly ICommonInfoRepository _groupRepo;

    

    public GetAllGroupHandler(ICommonInfoRepository groupRepo)
    {
        _groupRepo = groupRepo;
    }

    public async Task<TResult<Dictionary<string, List<string>>>> Handle(
        GetAllGroupQuery request,
        CancellationToken ct = default) 
    {
        var result = await _groupRepo.GetAllGroup();


        foreach (var item in result)
        {
            Console.WriteLine("итерация");
            Console.WriteLine(item.Key);
            Console.WriteLine(item.Value);
        }

        return TResult<Dictionary<string, List<string>>>.CompletedOperation(result);
    }

}

