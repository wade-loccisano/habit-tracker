using FluentValidation;

namespace Application.UseCases.Progress.Commands;

public class CompleteProgressCommandValidator : AbstractValidator<CompleteProgressCommand>
{
    public CompleteProgressCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleFor(x => x.HabitId)
            .NotNull();
    }
}
