using FluentValidation;

namespace Application.UseCases.Habits.Commands;

public class DeleteHabitCommandValidator : AbstractValidator<DeleteHabitCommand>
{
    public DeleteHabitCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleFor(x => x.HabitId)
            .NotEmpty();
    }
}
