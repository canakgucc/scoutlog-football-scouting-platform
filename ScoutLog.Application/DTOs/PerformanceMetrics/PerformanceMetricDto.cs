namespace ScoutLog.Application.DTOs.PerformanceMetrics;

public record PerformanceMetricDto(
    int Id,
    int PlayerId,
    string MatchName,
    DateOnly MatchDate,
    int MinutesPlayed,
    int Goals,
    int Assists,
    int TechnicalScore,
    int PhysicalScore,
    int TacticalScore,
    int MentalScore,
    int OverallScore,
    string? CoachNote);
