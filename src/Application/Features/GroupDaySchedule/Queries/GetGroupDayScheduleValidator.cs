
using Domain.Common.Enums;
using FluentValidation;

namespace Application.Features.TeacherSchedule.Queries;
public class GetGroupDayScheduleValidator : AbstractValidator<GetGroupDayScheduleQuery>
{
    public GetGroupDayScheduleValidator()
    {
        RuleFor(x => x.GroupName)
            .NotEmpty().WithState(_ => ErrorCode.InvalidGroupName)
            .WithMessage("Поле {PropertyName} не может быть пустым")
            .Matches(@"^[А-Я]\d{2}$").WithState(_ => ErrorCode.InvalidGroupName)
            .WithMessage("Поле {PropertyName} не соответствует формату");
        RuleFor(x => x.Day)
            .NotEmpty().WithState(_ => ErrorCode.InvalidDate)
            .WithMessage("Поле {PropertyName} не может быть пустым")
            .GreaterThan(DateOnly.FromDateTime(DateTime.Now)).WithState(_ => ErrorCode.InvalidDate)
            .WithMessage("Поле {PropertyName} должно быть больше текущего дня");
    }
}
