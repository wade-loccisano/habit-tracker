using Application.Common.Interfaces;
using Domain.DTOs.Mappers;
using Domain.DTOs.Output;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Progress.Queries;

public record GetHabitProgressQuery(
    string UserId,
    Guid HabitId) : IRequest<ICollection<HabitProgressDTO>>;

public class GetHabitProgressQueryHandler : IRequestHandler<GetHabitProgressQuery, ICollection<HabitProgressDTO>>
{
    private readonly IHabitTrackerDbContext _context;

    public GetHabitProgressQueryHandler(IHabitTrackerDbContext context) => _context = context;

    public async Task<ICollection<HabitProgressDTO>> Handle(
        GetHabitProgressQuery request,
        CancellationToken cancellationToken)
    {
        ICollection<HabitProgressDTO> result = await _context.HabitProgresses
            .AsNoTracking()
            .Where(x => x.HabitId == request.HabitId)
            .Select(x => x.MapAsDTO())
            .ToListAsync(cancellationToken);

        return result;
    }
}
