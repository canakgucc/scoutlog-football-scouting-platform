namespace ScoutLog.Application.DTOs.Clubs;

public record ClubDto(
    int Id,
    string Name,
    string City,
    string Country,
    string? LogoUrl);
