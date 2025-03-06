using Domain.DTOs.Output;
using Domain.UseCases.Habits;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

internal class HabitController : APIControllerBase
{
    [HttpGet("list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ICollection<HabitListDTO>>> GetHabitsList(CancellationToken cancellationToken)
    {
        ICollection<HabitListDTO> results = await Mediator.Send(
            new GetHabitsListQuery(
                "hat",
                1,
                DateTime.Now,
                1),
            cancellationToken);

        return Ok(results);
    }
}
