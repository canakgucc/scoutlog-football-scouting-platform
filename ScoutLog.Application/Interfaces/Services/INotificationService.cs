using ScoutLog.Application.DTOs.Notifications;

namespace ScoutLog.Application.Interfaces.Services;

public interface INotificationService
{
    Task<IReadOnlyList<NotificationDto>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<NotificationDto>> GetUnreadByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<bool> MarkAsReadAsync(int id, CancellationToken cancellationToken = default);
}
