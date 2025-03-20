namespace Domain.DTOs.Output;

public record HabitProgressDTO(
    Guid Id,
    Guid HabitId,
    bool Completed,
    DateTime CompetedDate);
