namespace ScoutLog.Application.DTOs.Users;

public record UserDto(
    int Id,
    string FullName,
    string Email,
    string Role,
    int? ClubId,
    int? TeamId,
    bool IsActive);
