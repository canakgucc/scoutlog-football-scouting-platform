namespace ScoutLog.Domain.Entities;

public class Player
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public string Position { get; set; } = string.Empty;
    public string PreferredFoot { get; set; } = string.Empty;
    public int Height { get; set; }
    public int Weight { get; set; }
    public string Nationality { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
    public string Status { get; set; } = "Active";

    public int ClubId { get; set; }
    public Club Club { get; set; } = null!;

    public int? TeamId { get; set; }
    public Team? Team { get; set; }

    public ICollection<ScoutReport> ScoutReports { get; set; } = new List<ScoutReport>();
    public ICollection<PerformanceMetric> PerformanceMetrics { get; set; } = new List<PerformanceMetric>();
}
