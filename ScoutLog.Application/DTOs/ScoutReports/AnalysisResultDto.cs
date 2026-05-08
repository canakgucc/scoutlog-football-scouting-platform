namespace ScoutLog.Application.DTOs.ScoutReports;

public record AnalysisResultDto(
    int ScoutReportId,
    string Summary,
    IReadOnlyList<string> Strengths,
    IReadOnlyList<string> Weaknesses,
    int SuggestedScore,
    IReadOnlyList<string> Tags,
    string DevelopmentAdvice);
