namespace ScoutLog.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;

    public int? ClubId { get; set; }
    public Club? Club { get; set; }

    public int? TeamId { get; set; }
    public Team? Team { get; set; }

    public ICollection<ScoutReport> ScoutReports { get; set; } = new List<ScoutReport>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
