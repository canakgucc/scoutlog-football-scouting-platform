namespace ScoutLog.Application.DTOs.ScoutReports;

public record ScoutReportDto(
    int Id,
    int PlayerId,
    int ScoutId,
    string Title,
    string ObservationText,
    int TechnicalScore,
    int PhysicalScore,
    int TacticalScore,
    int MentalScore,
    int PotentialScore,
    int OverallScore,
    string Recommendation,
    string? AnalysisSummary,
    string? Strengths,
    string? Weaknesses,
    string? SuggestedActions,
    int? SuggestedScore,
    string? Tags,
    string? DevelopmentAdvice,
    DateTime CreatedAt);
