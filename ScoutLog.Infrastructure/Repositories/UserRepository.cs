using Microsoft.EntityFrameworkCore;
using ScoutLog.Application.Interfaces.Repositories;
using ScoutLog.Domain.Entities;
using ScoutLog.Infrastructure.Persistence;

namespace ScoutLog.Infrastructure.Repositories;

public class UserRepository(ScoutLogDbContext context)
    : Repository<User>(context), IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await Query()
            .Include(user => user.Role)
            .Include(user => user.Club)
            .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
    }

    public async Task<IReadOnlyList<User>> GetByClubIdAsync(
        int clubId,
        CancellationToken cancellationToken = default)
    {
        return await Query()
            .AsNoTracking()
            .Include(user => user.Role)
            .Where(user => user.ClubId == clubId)
            .OrderBy(user => user.FullName)
            .ToListAsync(cancellationToken);
    }
}
