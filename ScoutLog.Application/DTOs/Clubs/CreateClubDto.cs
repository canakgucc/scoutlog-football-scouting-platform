namespace ScoutLog.Application.DTOs.Clubs;

public record CreateClubDto(
    string Name,
    string City,
    string Country,
    string? LogoUrl);
