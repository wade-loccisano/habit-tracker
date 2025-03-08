using Domain.Models;
using Domain.UseCases.Habits;
using Microsoft.EntityFrameworkCore;

namespace Domain.Common.Interfaces;

public interface IHabitTrackerDbContext
{
    DbSet<Habit> Habits { get; set; }
}
