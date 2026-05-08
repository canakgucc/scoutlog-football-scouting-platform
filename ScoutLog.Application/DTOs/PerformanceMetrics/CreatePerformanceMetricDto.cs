namespace ScoutLog.Application.DTOs.PerformanceMetrics;

public record CreatePerformanceMetricDto(
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
    string? CoachNote);
