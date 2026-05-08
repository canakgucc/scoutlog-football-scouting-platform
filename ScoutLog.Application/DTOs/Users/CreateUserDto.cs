namespace ScoutLog.Application.DTOs.Users;

public record CreateUserDto(
    string FullName,
    string Email,
    string Password,
    int RoleId,
    int? ClubId,
    int? TeamId);
