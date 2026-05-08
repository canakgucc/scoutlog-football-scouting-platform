namespace ScoutLog.Domain.Entities;

public class Club
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Team> Teams { get; set; } = new List<Team>();
    public ICollection<Player> Players { get; set; } = new List<Player>();
    public ICollection<User> Users { get; set; } = new List<User>();
}
