namespace Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime Created { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? LastUpdated { get; set; }
    public Guid? LastUpdatedBy { get; set; }
}
