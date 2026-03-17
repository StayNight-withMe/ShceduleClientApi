using Application.Abstraction.DataBase;
using Application.Features.GeneralData;
using Domain.Model.Entity;
using Domain.Model.ReturnEntity;
using Domain.Specification;
using MediatR;

namespace Application.Features.AllTeacher.Queries;
public class GetAllTeacherHandler : IRequestHandler<GetAllTeacherQuery, TResult<List<string>>>
{
    private readonly IBaseRepository<TeacherEntity> _teacherRepository;

    public GetAllTeacherHandler(
        IBaseRepository<TeacherEntity> teacherRepository
    )
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<TResult<List<string>>> Handle(
        GetAllTeacherQuery request,
        CancellationToken ct = default)
    {
        var teacherList = await _teacherRepository.ListAsync(new GetOrderedTeacherSpec(), ct);

        return TResult<List<string>>.CompletedOperation(
            teacherList ?? new()
        );
    }
}
