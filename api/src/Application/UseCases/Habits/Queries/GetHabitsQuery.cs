using Application.Common.Interfaces;
using Domain.DTOs.Mappers;
using Domain.DTOs.Output;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Habits.Queries;

public record GetHabitsQuery(
    string UserId) : IRequest<ICollection<HabitListDTO>>;

public class GetHabitsQueryHandler : IRequestHandler<GetHabitsQuery, ICollection<HabitListDTO>>
{
    private readonly IHabitTrackerDbContext _context;

    public GetHabitsQueryHandler(IHabitTrackerDbContext context)
        => _context = context;

    public async Task<ICollection<HabitListDTO>> Handle(
        GetHabitsQuery query,
        CancellationToken cancellationToken)
    {
        ICollection<HabitListDTO> result = await _context.Habits
            .AsNoTracking()
            .Where(x => x.UserId == query.UserId)
            .Select(x => x.MapAsDTO())
            .ToListAsync(cancellationToken);

        return result;
    }
}
