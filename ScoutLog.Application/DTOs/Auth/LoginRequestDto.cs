using System.ComponentModel.DataAnnotations;

namespace ScoutLog.Application.DTOs.Auth;

public class LoginRequestDto
{
    [Required]
    [EmailAddress]
    [StringLength(160)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(80)]
    public string Password { get; set; } = string.Empty;
}
