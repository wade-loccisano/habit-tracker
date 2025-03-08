using Domain.Common;

namespace Domain.Models;

public class HabitProgress : BaseAuditableEntity
{
    public bool Completed { get; set; }
    public DateTime CompletedDate { get; set; } // make nullable
    
    public Guid HabitId { get; set; }
    public required Habit Habit { get; set; }
}
