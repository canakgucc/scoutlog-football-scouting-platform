using Microsoft.EntityFrameworkCore;
using ScoutLog.Application.Interfaces.Repositories;
using ScoutLog.Domain.Entities;
using ScoutLog.Infrastructure.Persistence;

namespace ScoutLog.Infrastructure.Repositories;

public class ScoutReportRepository(ScoutLogDbContext context)
    : Repository<ScoutReport>(context), IScoutReportRepository
{
    public async Task<IReadOnlyList<ScoutReport>> GetByPlayerIdAsync(
        int playerId,
        CancellationToken cancellationToken = default)
    {
        return await Query()
            .AsNoTracking()
            .Where(report => report.PlayerId == playerId)
            .OrderByDescending(report => report.EventDate)
            .ThenByDescending(report => report.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ScoutReport>> GetByScoutIdAsync(
        int scoutId,
        CancellationToken cancellationToken = default)
    {
        return await Query()
            .AsNoTracking()
            .Where(report => report.ScoutId == scoutId)
            .OrderByDescending(report => report.EventDate)
            .ThenByDescending(report => report.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}
