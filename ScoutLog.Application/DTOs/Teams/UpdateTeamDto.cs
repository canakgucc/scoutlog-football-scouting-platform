namespace ScoutLog.Application.DTOs.Teams;

public record UpdateTeamDto(
    string Name,
    string AgeGroup,
    string Season,
    int? CoachId);
