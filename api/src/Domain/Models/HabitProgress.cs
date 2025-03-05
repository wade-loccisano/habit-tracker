using Domain.Common;

namespace Domain.Models;

public class HabitProgress : BaseAuditableEntity
{
    public DateTime CompletedDate { get; set; }
    public bool Completed { get; set; }
    
    public Guid HabitId { get; set; }
    public required Habit Habit { get; set; }
}
