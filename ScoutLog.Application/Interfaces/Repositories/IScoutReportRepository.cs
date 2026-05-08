using ScoutLog.Domain.Entities;

namespace ScoutLog.Application.Interfaces.Repositories;

public interface IScoutReportRepository : IRepository<ScoutReport>
{
    Task<IReadOnlyList<ScoutReport>> GetByPlayerIdAsync(int playerId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ScoutReport>> GetByScoutIdAsync(int scoutId, CancellationToken cancellationToken = default);
}
