namespace ScoutLog.Application.DTOs.Dashboard;

public record PipelineSummaryDto(
    int NewCount,
    int UnderObservationCount,
    int FollowUpNeededCount,
    int ShortlistedCount,
    int RecommendedCount,
    int RejectedCount,
    int TotalWatchlistCount,
    int HighPriorityWatchlistCount);
