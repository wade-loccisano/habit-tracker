using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Progress.Commands;

public record CompleteProgressCommand(
    string UserId,
    Guid HabitId) : IRequest<bool>;

public class CompleteProgressCommandHanlder : IRequestHandler<CompleteProgressCommand, bool>
{
    private readonly IHabitTrackerDbContext _context;

    public CompleteProgressCommandHanlder(IHabitTrackerDbContext context)
        => _context = context;

    public async Task<bool> Handle(
        CompleteProgressCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new HabitProgress
        {
            Completed = true,
            CompletedDate = DateTime.UtcNow,
        };

        Habit? habit = await _context.Habits
            .Where(x => x.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (habit is null)
        {
            return false;
        }

        habit.HabitProgresses.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
