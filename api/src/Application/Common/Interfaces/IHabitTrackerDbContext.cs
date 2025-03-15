using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IHabitTrackerDbContext
{
    DbSet<Habit> Habits { get; }
    DbSet<HabitProgress> HabitProgresses { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
