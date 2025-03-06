using Domain.Common.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class HabitTrackerDbContext : DbContext, IHabitTrackerDbContext
{
    public DbSet<Habit> Habits { get; set; }
    public DbSet<HabitProgress> HabitProgresses { get; set; }

    public string DbPath { get; } = "q";

    public HabitTrackerDbContext(DbContextOptions<HabitTrackerDbContext> options) 
        : base(options) => DbPath = "q";
}
