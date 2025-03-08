using Domain.DTOs.Output;
using Domain.Models;
using Domain.UseCases.Habits;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using System.Drawing;
using static API.UnitTests.Testing;

namespace API.UnitTests.UseCases.Habits.Queries;

public class GetHabitsListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnAllItems()
    {
        //await RunAsDefaultUserAsync();

        var habit = new Habit
        {
            Name = "Name",
            Frequency = 12,
            ReminderTime = DateTime.UtcNow,
            StreakCount = 4,
            HabitProgresses = { }
        };

        await AddAsync(habit);

        var habitProgresses = new List<HabitProgress>
        {
        };

        var prog = new HabitProgress { HabitId = habit.Id, Habit = habit, Completed = true, CompletedDate = DateTime.UtcNow };

        await AddAsync(habitProgresses);
        await AddAsync(prog);

        habit.HabitProgresses.Add(prog);

        var query = new GetHabitsListQuery();

        ICollection<HabitListDTO> result = await SendAsync(query);

        //result.Count.Should().HaveCount(1);
        result.First().Name.Should().Be(habit.Name);
    }
}
