namespace ScoutLog.Application.DTOs.Clubs;

public record UpdateClubDto(
    string Name,
    string City,
    string Country,
    string? LogoUrl);
