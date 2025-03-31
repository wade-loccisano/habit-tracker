using FluentValidation;

namespace Application.UseCases.Progress.Queries;

public class GetHabitProgressQueryValidator : AbstractValidator<GetHabitProgressQuery>
{
    public GetHabitProgressQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull();
        RuleFor(x => x.HabitId)
            .NotNull();
    }
}
