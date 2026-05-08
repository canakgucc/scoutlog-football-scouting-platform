namespace ScoutLog.Application.DTOs.Players;

public record PlayerDto(
    int Id,
    int ClubId,
    int? TeamId,
    string FirstName,
    string LastName,
    DateOnly BirthDate,
    string Position,
    string PreferredFoot,
    int Height,
    int Weight,
    string Nationality,
    string? PhotoUrl,
    string Status,
    string PipelineStatus,
    bool IsWatchlisted,
    string? WatchlistPriority,
    string? WatchlistReason);
