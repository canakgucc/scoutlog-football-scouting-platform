namespace ScoutLog.Application.DTOs.Dashboard;

public record TopPotentialPlayerDto(
    int PlayerId,
    string FullName,
    string Position,
    int PotentialScore,
    int SuggestedScore,
    string Recommendation,
    string Tags);
