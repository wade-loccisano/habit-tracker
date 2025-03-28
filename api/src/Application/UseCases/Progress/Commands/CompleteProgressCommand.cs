using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Progress.Commands;

public record CompleteProgressCommand(
    string UserId,
    Guid HabitId) : IRequest<bool>;

public class CompleteProgressCommandHandler : IRequestHandler<CompleteProgressCommand, bool>
{
    private readonly IHabitTrackerDbContext _context;

    public CompleteProgressCommandHandler(IHabitTrackerDbContext context)
        => _context = context;

    public async Task<bool> Handle(
        CompleteProgressCommand request,
        CancellationToken cancellationToken)
    {
        Habit? habit = await _context.Habits
            .Where(x => x.UserId == request.UserId && x.Id == request.HabitId)
            .Include(x => x.HabitProgresses)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (habit is null)
        {
            return false;
        }

        // check if habit was already completed today

        var entity = new HabitProgress
        {
            Completed = true,
            CompletedDate = DateTime.UtcNow,
        };

        await _context.HabitProgresses.AddAsync(entity, cancellationToken);

        habit.HabitProgresses.Add(entity);
        habit.StreakCount += 1;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
