using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Habits.Commands;

public record DeleteHabitCommand(
    string UserId,
    Guid HabitId) : IRequest<Guid>;

public class DeleteHabitCommandHandler: IRequestHandler<DeleteHabitCommand, Guid>
{
    private readonly IHabitTrackerDbContext _context;

    public DeleteHabitCommandHandler(IHabitTrackerDbContext context) => _context = context;

    public async Task<Guid> Handle(DeleteHabitCommand request, CancellationToken cancellationToken)
    {
        Habit? entity = await _context.Habits
            .Where(x => x.UserId == request.UserId && x.Id == request.HabitId)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            return Guid.Empty;
        }

        _context.Habits.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
