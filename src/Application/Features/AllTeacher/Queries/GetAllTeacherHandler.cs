

using Application.Features.GeneralData;
using Domain.Model.ReturnEntity;
using MediatR;

namespace Application.Features.AllTeacher.Queries;
public class GetAllTeacherHandler : IRequestHandler<GetAllTeacherQuery, TResult<List<string>>>
{
    public Task<TResult<List<string>>> Handle(
        GetAllTeacherQuery request,
        CancellationToken ct = default)
    {
        throw new NotImplementedException();

    }
}
