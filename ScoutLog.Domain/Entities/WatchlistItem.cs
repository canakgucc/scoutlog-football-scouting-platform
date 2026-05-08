namespace ScoutLog.Domain.Entities;

public class WatchlistItem
{
    public int Id { get; set; }
    public string Priority { get; set; } = "Medium";
    public string Reason { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;

    public int ClubId { get; set; }
    public Club Club { get; set; } = null!;

    public int CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; } = null!;
}
