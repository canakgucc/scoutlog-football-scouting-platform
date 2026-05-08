using System.ComponentModel.DataAnnotations;

namespace ScoutLog.Application.DTOs.Players;

public class UpdatePlayerPipelineStatusDto
{
    [Required]
    [MaxLength(40)]
    public string PipelineStatus { get; set; } = string.Empty;
}
