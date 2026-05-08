using System.ComponentModel.DataAnnotations;

namespace ScoutLog.Application.DTOs.Players;

public class UpdatePlayerDto
{
    [Range(1, int.MaxValue)]
    public int? TeamId { get; set; }

    [Required]
    [StringLength(80)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(80)]
    public string LastName { get; set; } = string.Empty;

    public DateOnly BirthDate { get; set; }

    [Required]
    [StringLength(40)]
    public string Position { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string PreferredFoot { get; set; } = string.Empty;

    [Range(120, 230)]
    public int Height { get; set; }

    [Range(35, 130)]
    public int Weight { get; set; }

    [Required]
    [StringLength(80)]
    public string Nationality { get; set; } = string.Empty;

    [StringLength(500)]
    public string? PhotoUrl { get; set; }

    [Required]
    [StringLength(40)]
    public string Status { get; set; } = string.Empty;
}
