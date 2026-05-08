using ScoutLog.Domain.Entities;

namespace ScoutLog.Application.Interfaces.Repositories;

public interface IPerformanceMetricRepository : IRepository<PerformanceMetric>
{
    Task<IReadOnlyList<PerformanceMetric>> GetByPlayerIdAsync(int playerId, CancellationToken cancellationToken = default);
}
