using System.ComponentModel.DataAnnotations;

namespace ScoutLog.Application.DTOs.Players;

public class UpsertWatchlistItemDto
{
    [Required]
    [MaxLength(20)]
    public string Priority { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Reason { get; set; } = string.Empty;
}
