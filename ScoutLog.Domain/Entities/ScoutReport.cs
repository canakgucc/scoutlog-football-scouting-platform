namespace ScoutLog.Domain.Entities;

public class ScoutReport
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ObservationText { get; set; } = string.Empty;
    public int TechnicalScore { get; set; }
    public int PhysicalScore { get; set; }
    public int TacticalScore { get; set; }
    public int MentalScore { get; set; }
    public int PotentialScore { get; set; }
    public int OverallScore { get; set; }
    public string Recommendation { get; set; } = string.Empty;
    public string? AnalysisSummary { get; set; }
    public string? Strengths { get; set; }
    public string? Weaknesses { get; set; }
    public string? SuggestedActions { get; set; }
    public int? SuggestedScore { get; set; }
    public string? Tags { get; set; }
    public string? DevelopmentAdvice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;

    public int ScoutId { get; set; }
    public User Scout { get; set; } = null!;
}
