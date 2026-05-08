using ScoutLog.Domain.Entities;

namespace ScoutLog.Application.Interfaces.Repositories;

public interface INotificationRepository : IRepository<Notification>
{
    Task<IReadOnlyList<Notification>> GetUnreadByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}
