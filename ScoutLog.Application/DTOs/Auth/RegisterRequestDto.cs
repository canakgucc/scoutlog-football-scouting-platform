using System.ComponentModel.DataAnnotations;

namespace ScoutLog.Application.DTOs.Auth;

public class RegisterRequestDto
{
    [Required]
    [StringLength(120)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(160)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    [StringLength(80)]
    public string Password { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int RoleId { get; set; }

    [Range(1, int.MaxValue)]
    public int? ClubId { get; set; }

    [Range(1, int.MaxValue)]
    public int? TeamId { get; set; }
}
