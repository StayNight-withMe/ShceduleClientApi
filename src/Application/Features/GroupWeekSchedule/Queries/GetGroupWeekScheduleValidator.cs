using Domain.Common.Enums;
using FluentValidation;

namespace Application.Features.TeacherSchedule.Queries;
public class GetGroupWeekScheduleValidator : AbstractValidator<GetGroupWeekScheduleQuery>
{
    public GetGroupWeekScheduleValidator()
    {
        RuleFor(x => x.GroupName)
            .NotEmpty().WithState(_ => ErrorCode.InvalidGroupName)
            .WithMessage("Поле {PropertyName} не должно быть пустым")
            .Matches(@"^[А-Я]\d{2}$").WithState(_ => ErrorCode.InvalidGroupName)
            .WithMessage("Поле {PropertyName} не соответствует формату");
        RuleFor(x => x.Date)
            .NotEmpty().WithState(_ => ErrorCode.InvalidDate)
            .WithMessage("Поле {PropertyName} не должно быть пустым")
            .Must(x => x >= DateOnly.FromDateTime(DateTime.Today)).WithState(_ => ErrorCode.InvalidDate)
            .WithMessage("Поле {PropertyName} должно быть больше или равно сегодняшнему дню");
    }
}
