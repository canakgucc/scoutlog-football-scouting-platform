namespace ScoutLog.Application.DTOs.Auth;

public record AuthResponseDto(
    string AccessToken,
    DateTime ExpiresAt,
    int UserId,
    string FullName,
    string Email,
    string Role,
    int ClubId,
    string ClubName);
