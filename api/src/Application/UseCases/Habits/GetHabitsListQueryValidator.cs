using Domain.UseCases.Habits;
using FluentValidation;

namespace Application.UseCases.Habits;

public class GetHabitsListQueryValidator : AbstractValidator<GetHabitsListQuery>
{
    public GetHabitsListQueryValidator()
    {
    }
}
