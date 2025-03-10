using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IHabitTrackerDbContext
{
    DbSet<Habit> Habits { get; set; }
}
