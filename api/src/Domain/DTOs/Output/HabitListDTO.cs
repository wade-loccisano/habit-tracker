namespace Domain.DTOs.Output;

public record HabitListDTO(
    Guid Id,
    string Name,
    int Frequency,
    DateTime? ReminderTime,
    int StreakCount);
