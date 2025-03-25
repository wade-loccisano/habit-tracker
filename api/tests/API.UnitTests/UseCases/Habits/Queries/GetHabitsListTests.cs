using Domain.DTOs.Output;
using Domain.Models;
using Domain.UseCases.Habits;
using FluentAssertions;
using static API.UnitTests.Testing;

namespace API.UnitTests.UseCases.Habits.Queries;

public class GetHabitsListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnAllItems()
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

        //var habitProgresses = new List<HabitProgress>
        //{
        //};

        var prog = new HabitProgress { HabitId = habit.Id, Completed = true, CompletedDate = DateTime.UtcNow };

        //await AddAsync(habitProgresses);
        await AddAsync(prog);

        habit.HabitProgresses.Add(prog);

        var query = new GetHabitsListQuery();

        ICollection<HabitListDTO> result = await SendAsync(query);

        //result.Count.Should().HaveCount(1);
        result.First().Name.Should().Be(habit.Name);
    }

    //[Test]
    //public async Task ShouldGetList()
    //{
    //    var listId = await SendAsync(new CreateTodoListCommand
    //    {
    //        Title = "New List"
    //    });

    //    var command = new CreateTodoItemCommand
    //    {
    //        ListId = listId,
    //        Title = "Tasks"
    //    };

    //    var itemId = await SendAsync(command);

    //    var item = await FindAsync<TodoItem>(itemId);

    //    item.Should().NotBeNull();
    //    item!.ListId.Should().Be(command.ListId);
    //    item.Title.Should().Be(command.Title);
    //    item.CreatedBy.Should().Be(userId);
    //    item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    //    item.LastModifiedBy.Should().Be(userId);
    //    item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    //}
}
