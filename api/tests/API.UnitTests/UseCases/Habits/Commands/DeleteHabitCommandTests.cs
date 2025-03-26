using Application.UseCases.Habits.Commands;
using Domain.Models;
using FluentAssertions;
using static API.UnitTests.Testing;

namespace API.UnitTests.UseCases.Habits.Commands;

public class DeleteHabitCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldDeleteHabit()
    {
        await RunAsDefaultUserAsync();
        string userId = GetUserId() ?? string.Empty;
        var habit = new Habit
        {
            UserId = userId,
            Name = "Name",
            Frequency = 12,
            ReminderTime = DateTime.UtcNow,
            StreakCount = 4,
            HabitProgresses = { }
        };
        await AddAsync(habit);

        var command = new DeleteHabitCommand(
            userId,
            habit.Id);

        Guid result = await SendAsync(command);

        result.Should().Be(habit.Id);
    }
}
