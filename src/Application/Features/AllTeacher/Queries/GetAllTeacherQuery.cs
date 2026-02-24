using Domain.Model.ReturnEntity;
using MediatR;

namespace Application.Features.GeneralData;

public record GetAllTeacherQuery() : IRequest<TResult<List<string>>>;
