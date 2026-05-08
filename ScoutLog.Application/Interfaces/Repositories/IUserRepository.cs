using ScoutLog.Domain.Entities;

namespace ScoutLog.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<User>> GetByClubIdAsync(int clubId, CancellationToken cancellationToken = default);
}
