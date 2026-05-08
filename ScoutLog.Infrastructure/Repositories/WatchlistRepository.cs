using Microsoft.EntityFrameworkCore;
using ScoutLog.Application.Interfaces.Repositories;
using ScoutLog.Domain.Entities;
using ScoutLog.Infrastructure.Persistence;

namespace ScoutLog.Infrastructure.Repositories;

public class WatchlistRepository(ScoutLogDbContext context)
    : Repository<WatchlistItem>(context), IWatchlistRepository
{
    public async Task<IReadOnlyList<WatchlistItem>> GetByClubIdAsync(
        int clubId,
        CancellationToken cancellationToken = default)
    {
        return await Query()
            .AsNoTracking()
            .Where(item => item.ClubId == clubId)
            .OrderByDescending(item => item.UpdatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<WatchlistItem>> GetByPlayerIdsAsync(
        IReadOnlyCollection<int> playerIds,
        CancellationToken cancellationToken = default)
    {
        return await Query()
            .AsNoTracking()
            .Where(item => playerIds.Contains(item.PlayerId))
            .ToListAsync(cancellationToken);
    }

    public async Task<WatchlistItem?> GetByPlayerIdAsync(
        int playerId,
        CancellationToken cancellationToken = default)
    {
        return await Query()
            .FirstOrDefaultAsync(item => item.PlayerId == playerId, cancellationToken);
    }
}
