namespace ScoutLog.Application.DTOs.Users;

public record UpdateUserDto(
    string FullName,
    int RoleId,
    int? ClubId,
    int? TeamId,
    bool IsActive);
