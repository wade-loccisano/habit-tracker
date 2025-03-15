using Application.Common.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.UseCases.Habits.Commands;

public record CreateHabitCommand(
    string UserId,
    string Name,
    int Frequency,
    DateTime? ReminderTime) : IRequest<Guid>;

public class CreateHabitCommandHanlder : IRequestHandler<CreateHabitCommand, Guid>
{
    private readonly IHabitTrackerDbContext _context;

    public CreateHabitCommandHanlder(IHabitTrackerDbContext context) => _context = context;

    public async Task<Guid> Handle(CreateHabitCommand request, CancellationToken cancellationToken)
    {
        var entity = new Habit
        {
            UserId = request.UserId,
            Name = request.Name,
            Frequency = request.Frequency,
            ReminderTime = request.ReminderTime
        };

        _context.Habits.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
