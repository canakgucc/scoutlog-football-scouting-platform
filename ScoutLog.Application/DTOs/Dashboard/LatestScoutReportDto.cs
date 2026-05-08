namespace ScoutLog.Application.DTOs.Dashboard;

public record LatestScoutReportDto(
    int Id,
    int PlayerId,
    string PlayerName,
    string ReportType,
    DateTime EventDate,
    string? Opponent,
    string? ObservedPosition,
    string Title,
    int PotentialScore,
    int OverallScore,
    string Recommendation,
    DateTime CreatedAt);
