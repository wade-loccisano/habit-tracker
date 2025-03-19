using Application.UseCases.Habits.Commands;
using Application.UseCases.Habits.Queries;
using Domain.DTOs.Output;
using Domain.UseCases.Habits;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class HabitController : APIControllerBase
{
    [Authorize]
    [HttpGet("list")]
    public async Task<ActionResult<ICollection<HabitListDTO>>> GetHabitsList(CancellationToken cancellationToken)
    {
        ICollection<HabitListDTO> results = await Mediator.Send(
            new GetHabitsListQuery(),
            cancellationToken);

        return Ok(results);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ICollection<HabitListDTO>>> GetHabits(
        string UserId,
        CancellationToken cancellationToken)
    {
        ICollection<HabitListDTO> results = await Mediator.Send(
            new GetHabitsQuery(
                UserId),
            cancellationToken);

        return Ok(results);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateHabit(
        string userId,
        string name,
        int frequency,
        DateTime? reminderTIme,
        CancellationToken cancellationToken)
    {
        Guid results = await Mediator.Send(
            new CreateHabitCommand(
                userId,
                name,
                frequency,
                reminderTIme),
            cancellationToken);

        return Ok(results);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<Guid>> UpdateHabt(
        string userId,
        Guid habitId,
        string? name,
        int? frequency,
        DateTime? reminderTime,
        CancellationToken cancellationToken)
    {
        Guid results = await Mediator.Send(
            new UpdateHabitCommand(
                userId,
                habitId,
                name,
                frequency,
                reminderTime),
            cancellationToken);

        return Ok(results);
    }


}
