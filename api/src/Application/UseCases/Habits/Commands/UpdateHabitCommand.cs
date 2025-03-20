using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Habits.Commands;

public record UpdateHabitCommand(
    string UserId,
    Guid HabitId,
    string? Name,
    int? Frequency,
    DateTime? ReminderTime) : IRequest<Guid>;

public class UpdateHabitCommandHandler : IRequestHandler<UpdateHabitCommand, Guid>
{
    private readonly IHabitTrackerDbContext _context;

    public UpdateHabitCommandHandler(IHabitTrackerDbContext context) => _context = context;

    public async Task<Guid> Handle(UpdateHabitCommand request, CancellationToken cancellationToken)
    {
        Habit? entity = await _context.Habits
            .Where(x => x.Id == request.HabitId)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            // not found
            return Guid.Empty;
        }

        entity.Name = string.IsNullOrEmpty(request.Name) ? entity.Name : request.Name;

        if (request.Frequency.HasValue)
        {
            entity.Frequency = (int)request.Frequency;
        }

        if (request.ReminderTime.HasValue)
        {
            entity.ReminderTime = request.ReminderTime;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
