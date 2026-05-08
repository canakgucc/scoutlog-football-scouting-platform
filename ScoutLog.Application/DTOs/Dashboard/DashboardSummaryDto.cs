namespace ScoutLog.Application.DTOs.Dashboard;

public record DashboardSummaryDto(
    int TotalPlayers,
    int TotalScoutReports,
    double AveragePotentialScore,
    int PlayersToWatchCount,
    IReadOnlyList<LatestScoutReportDto> LatestScoutReports,
    IReadOnlyList<TopPotentialPlayerDto> TopPotentialPlayers,
    IReadOnlyList<PositionDistributionDto> PositionDistribution,
    IReadOnlyList<string> RecentPerformanceWarnings,
    double AverageTechnicalScore,
    double AveragePhysicalScore,
    double AverageTacticalScore,
    double AverageMentalScore);
