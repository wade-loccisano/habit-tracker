using Application.UseCases.Progress.Commands;
using Application.UseCases.Progress.Queries;
using Domain.DTOs.Output;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProgressController : APIControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ICollection<HabitListDTO>>> GetProgress(
        string UserId,
        Guid HabitId,
        CancellationToken cancellationToken)
    {
        ICollection<HabitProgressDTO> results = await Mediator.Send(
            new GetHabitProgressQuery(
                UserId,
                HabitId),
            cancellationToken);

        return Ok(results);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<bool>> CompleteProgress(
        string UserId,
        Guid HabitId,
        CancellationToken cancellationToken)
    {
        bool results = await Mediator.Send(
            new CompleteProgressCommand(
                UserId,
                HabitId),
            cancellationToken);

        return Ok(results);
    }
}
