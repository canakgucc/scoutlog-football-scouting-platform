using ScoutLog.Domain.Entities;

namespace ScoutLog.Application.Interfaces.Repositories;

public interface IPlayerRepository : IRepository<Player>
{
    Task<IReadOnlyList<Player>> GetByClubIdAsync(int clubId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Player>> GetByTeamIdAsync(int teamId, CancellationToken cancellationToken = default);
}
