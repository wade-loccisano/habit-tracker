using FluentValidation;

namespace Application.UseCases.Habits.Commands;

public class UpdateHabitCommandValidator : AbstractValidator<UpdateHabitCommand>
{
    public UpdateHabitCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleFor(x => x.HabitId)
            .NotNull();
        RuleFor(x => x.Name)
            .MaximumLength(25)
            .NotEmpty();
        RuleFor(x => x.Frequency)
            .LessThanOrEqualTo(7)
            .NotEmpty();
    }
}
