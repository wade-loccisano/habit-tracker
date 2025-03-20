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

    public DbSet<Habit> Habits => Set<Habit>();
    public DbSet<HabitProgress> HabitProgresses => Set<HabitProgress>();

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

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int rowsAffected = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(true);

        return rowsAffected;
    }
}
