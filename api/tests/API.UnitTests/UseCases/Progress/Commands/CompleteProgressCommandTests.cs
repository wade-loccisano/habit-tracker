using Application.UseCases.Habits.Commands;
using Application.UseCases.Progress.Commands;
using Domain.Models;
using FluentAssertions;
using static API.UnitTests.Testing;

namespace API.UnitTests.UseCases.Progress.Commands;

public class CompleteProgressCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateHabitProgress()
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

        var command = new CompleteProgressCommand(
            userId,
            habit.Id);

        bool result = await SendAsync(command);

        result.Should().BeTrue();
        habit.StreakCount.Should().Be(1);
        habit.HabitProgresses.Count.Should().Be(1);
    }
}
