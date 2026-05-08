using System.ComponentModel.DataAnnotations;

namespace ScoutLog.Application.DTOs.ScoutReports;

public class CreateScoutReportDto
{
    [Range(1, int.MaxValue)]
    public int PlayerId { get; set; }

    public int? ScoutId { get; set; }

    [Required]
    [StringLength(20)]
    public string ReportType { get; set; } = "Match";

    [Required]
    public DateTime EventDate { get; set; }

    [StringLength(120)]
    public string? Opponent { get; set; }

    [StringLength(120)]
    public string? Competition { get; set; }

    [Range(0, 120)]
    public int? MinutesPlayed { get; set; }

    [StringLength(60)]
    public string? ObservedPosition { get; set; }

    [Required]
    [StringLength(160)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MinLength(20)]
    public string ObservationText { get; set; } = string.Empty;

    [Range(0, 100)]
    public int TechnicalScore { get; set; }

    [Range(0, 100)]
    public int PhysicalScore { get; set; }

    [Range(0, 100)]
    public int TacticalScore { get; set; }

    [Range(0, 100)]
    public int MentalScore { get; set; }

    [Range(0, 100)]
    public int PotentialScore { get; set; }

    [Required]
    [StringLength(100)]
    public string Recommendation { get; set; } = string.Empty;
}
