using System.Reflection.Emit;
using Application.Common.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class HabitTrackerDbContext : IdentityDbContext<User>, IHabitTrackerDbContext
{
    public HabitTrackerDbContext(DbContextOptions<HabitTrackerDbContext> options) 
        : base(options) => DbPath = "q";

    public DbSet<Habit> Habits { get; set; }
    public DbSet<HabitProgress> HabitProgresses { get; set; }

    public string DbPath { get; } = "q";


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>().Property(u => u.Intitials).HasMaxLength(5);

        builder.Entity<Habit>()
            .HasOne(h => h.User)
            .WithMany(u => u.Habits)
            .HasForeignKey(h => h.UserId)
            .OnDelete(DeleteBehavior.Cascade); // consider soft delete

        builder.HasDefaultSchema("identity");
    }
}
