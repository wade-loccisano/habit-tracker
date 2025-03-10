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


}
