using Microsoft.EntityFrameworkCore;
using ScoutLog.Application.Interfaces.Repositories;
using ScoutLog.Domain.Entities;
using ScoutLog.Infrastructure.Persistence;

namespace ScoutLog.Infrastructure.Repositories;

public class PlayerRepository(ScoutLogDbContext context)
    : Repository<Player>(context), IPlayerRepository
{
    public async Task<IReadOnlyList<Player>> GetByClubIdAsync(
        int clubId,
        CancellationToken cancellationToken = default)
    {
        return await Query()
            .AsNoTracking()
            .Where(player => player.ClubId == clubId)
            .OrderBy(player => player.LastName)
            .ThenBy(player => player.FirstName)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Player>> GetByTeamIdAsync(
        int teamId,
        CancellationToken cancellationToken = default)
    {
        return await Query()
            .AsNoTracking()
            .Where(player => player.TeamId == teamId)
            .OrderBy(player => player.LastName)
            .ThenBy(player => player.FirstName)
            .ToListAsync(cancellationToken);
    }
}
