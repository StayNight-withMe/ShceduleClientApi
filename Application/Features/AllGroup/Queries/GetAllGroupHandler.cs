

using Domain.Model.ReturnEntity;
using MediatR;

namespace Application.Features.AllGroup.Queries;
public class GetAllGroupHandler : IRequestHandler<GetAllGroupQuery, TResult<Dictionary<string, List<string>>>>
{
    public Task<TResult<Dictionary<string, List<string>>>> Handle(GetAllGroupQuery request, CancellationToken cancellationToken) 
    {
        throw new NotImplementedException();
    }

}

