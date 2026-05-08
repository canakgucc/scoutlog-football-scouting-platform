using System.ComponentModel.DataAnnotations;

namespace ScoutLog.Application.DTOs.ScoutReports;

public class UpdateScoutReportDto
{
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
