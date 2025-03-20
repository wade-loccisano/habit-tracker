using Domain.DTOs.Output;
using Domain.Models;

namespace Domain.DTOs.Mappers;

public static class HabitMappers
{
    public static HabitListDTO MapAsDTO(this Habit entity) => new(
        entity.Id,
        entity.Name,
        entity.Frequency,
        entity.ReminderTime,
        entity.StreakCount);

    public static HabitProgressDTO MapAsDTO(this HabitProgress entity) => new(
        entity.Id,
        entity.HabitId,
        entity.Completed,
        entity.CompletedDate);
}
