namespace ScoutLog.Domain.Entities;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string AgeGroup { get; set; } = string.Empty;
    public string Season { get; set; } = string.Empty;

    public int ClubId { get; set; }
    public Club Club { get; set; } = null!;

    public int? CoachId { get; set; }
    public User? Coach { get; set; }

    public ICollection<Player> Players { get; set; } = new List<Player>();
    public ICollection<User> Users { get; set; } = new List<User>();
}
