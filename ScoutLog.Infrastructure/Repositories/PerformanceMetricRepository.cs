using Microsoft.EntityFrameworkCore;
using ScoutLog.Application.Interfaces.Repositories;
using ScoutLog.Domain.Entities;
using ScoutLog.Infrastructure.Persistence;

namespace ScoutLog.Infrastructure.Repositories;

public class PerformanceMetricRepository(ScoutLogDbContext context)
    : Repository<PerformanceMetric>(context), IPerformanceMetricRepository
{
    public async Task<IReadOnlyList<PerformanceMetric>> GetByPlayerIdAsync(
        int playerId,
        CancellationToken cancellationToken = default)
    {
        return await Query()
            .AsNoTracking()
            .Where(metric => metric.PlayerId == playerId)
            .ToListAsync(cancellationToken);
    }
}
