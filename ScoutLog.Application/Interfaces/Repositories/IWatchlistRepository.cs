using ScoutLog.Domain.Entities;

namespace ScoutLog.Application.Interfaces.Repositories;

public interface IWatchlistRepository : IRepository<WatchlistItem>
{
    Task<IReadOnlyList<WatchlistItem>> GetByClubIdAsync(
        int clubId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<WatchlistItem>> GetByPlayerIdsAsync(
        IReadOnlyCollection<int> playerIds,
        CancellationToken cancellationToken = default);

    Task<WatchlistItem?> GetByPlayerIdAsync(
        int playerId,
        CancellationToken cancellationToken = default);
}
