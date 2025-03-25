using Application.UseCases.Habits.Commands;
using Domain.Models;
using FluentAssertions;
using static API.UnitTests.Testing;

namespace API.UnitTests.UseCases.Habits.Commands;

public class CreateHabitCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateNewHabit()
    {
        await RunAsDefaultUserAsync();
        string userId = GetUserId() ?? string.Empty;
        string name = "Name";
        int frequency = 68;
        var time = new DateTime();

        var command = new CreateHabitCommand(
            userId,
            name,
            frequency,
            time);

        Guid result = await SendAsync(command);

        result.Should().NotBeEmpty();
    }
}
