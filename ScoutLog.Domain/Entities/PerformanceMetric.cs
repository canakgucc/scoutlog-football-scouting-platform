namespace ScoutLog.Domain.Entities;

public class PerformanceMetric
{
    public int Id { get; set; }
    public string MatchName { get; set; } = string.Empty;
    public DateOnly MatchDate { get; set; }
    public int MinutesPlayed { get; set; }
    public int Goals { get; set; }
    public int Assists { get; set; }
    public int TechnicalScore { get; set; }
    public int PhysicalScore { get; set; }
    public int TacticalScore { get; set; }
    public int MentalScore { get; set; }
    public int OverallScore { get; set; }
    public string? CoachNote { get; set; }

    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;
}
