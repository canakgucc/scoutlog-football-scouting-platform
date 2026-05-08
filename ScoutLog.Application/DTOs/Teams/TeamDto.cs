namespace ScoutLog.Application.DTOs.Teams;

public record TeamDto(
    int Id,
    int ClubId,
    string Name,
    string AgeGroup,
    string Season,
    int? CoachId);
