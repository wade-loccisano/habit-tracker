using Domain.Common;

namespace Domain.Models;

public class Habit : BaseAuditableEntity, IUserOwned
{
    public required string Name { get; set; }
    public required int Frequency { get; set; }
    public required DateTime? ReminderTime { get; set; }
    public int StreakCount { get; set; }

    public ICollection<HabitProgress> HabitProgresses { get; } = [];

    public required string UserId { get; set; }
    public User User { get; } = null!;
}
