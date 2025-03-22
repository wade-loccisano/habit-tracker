using Domain.UseCases.Habits;
using FluentValidation;

namespace Application.UseCases.Habits.Queries;

public class GetHabitsQueryValidator : AbstractValidator<GetHabitsQuery>
{
    public GetHabitsQueryValidator()
    {
    }
}
