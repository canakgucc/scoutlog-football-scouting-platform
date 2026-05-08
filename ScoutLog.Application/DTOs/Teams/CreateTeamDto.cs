namespace ScoutLog.Application.DTOs.Teams;

public record CreateTeamDto(
    int ClubId,
    string Name,
    string AgeGroup,
    string Season,
    int? CoachId);
