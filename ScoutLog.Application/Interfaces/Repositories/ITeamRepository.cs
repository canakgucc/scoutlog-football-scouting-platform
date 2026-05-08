using ScoutLog.Domain.Entities;

namespace ScoutLog.Application.Interfaces.Repositories;

public interface ITeamRepository : IRepository<Team>
{
    Task<IReadOnlyList<Team>> GetByClubIdAsync(int clubId, CancellationToken cancellationToken = default);
}
