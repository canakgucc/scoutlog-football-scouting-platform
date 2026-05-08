namespace ScoutLog.Application.DTOs.Notifications;

public record NotificationDto(
    int Id,
    int UserId,
    string Title,
    string Message,
    bool IsRead,
    DateTime CreatedAt);
