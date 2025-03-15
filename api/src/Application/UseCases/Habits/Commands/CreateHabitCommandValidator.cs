using FluentValidation;

namespace Application.UseCases.Habits.Commands;

public class CreateHabitCommandValidator : AbstractValidator<CreateHabitCommand>
{
    public CreateHabitCommandValidator()
    {
        RuleFor(v => v.UserId)
            .NotEmpty();
        RuleFor(v => v.Name)
            .MaximumLength(25)
            .NotEmpty();
        RuleFor(v => v.Frequency)
            .LessThan(8)
            .NotEmpty();
    }
}
