

using Domain.Model.ReturnEntity;
using MediatR;

namespace Application.Features.AllGroup.Queries;

public record GetAllGroupQuery() : IRequest<TResult<Dictionary<string, List<string>>>>;
