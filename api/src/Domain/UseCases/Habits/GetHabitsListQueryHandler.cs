using Domain.Common.Interfaces;
using Domain.DTOs.Mappers;
using Domain.DTOs.Output;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Habits;

public record GetHabitsListQuery() : IRequest<ICollection<HabitListDTO>>;

public class GetHabitsListQueryHandler : IRequestHandler<GetHabitsListQuery, ICollection<HabitListDTO>>
{
    private readonly IHabitTrackerDbContext _context;

    public GetHabitsListQueryHandler(IHabitTrackerDbContext context) 
        => _context = context;

    public async Task<ICollection<HabitListDTO>> Handle(
        GetHabitsListQuery query,
        CancellationToken cancellationToken)
    {
        ICollection<HabitListDTO> result = await _context.Habits
            .AsNoTracking()
            .Select(x => x.MapAsDTO())
            .ToListAsync(cancellationToken);

        return result;
    }
}
