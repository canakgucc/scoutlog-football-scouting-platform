using ScoutLog.Application.DTOs.Dashboard;
using ScoutLog.Application.Interfaces.Repositories;
using ScoutLog.Application.Interfaces.Security;
using ScoutLog.Application.Interfaces.Services;
using ScoutLog.Domain.Entities;

namespace ScoutLog.Application.Services;

public class DashboardService(
    IPlayerRepository playerRepository,
    IScoutReportRepository scoutReportRepository,
    ICurrentUserContext currentUserContext) : IDashboardService
{
    public async Task<DashboardSummaryDto> GetSummaryAsync(CancellationToken cancellationToken = default)
    {
        var players = await playerRepository.GetAllAsync(cancellationToken);
        var reports = await scoutReportRepository.GetAllAsync(cancellationToken);
        var playersById = players.ToDictionary(player => player.Id);

        var activePlayers = players
            .Where(player =>
                player.ClubId == currentUserContext.ClubId
                && !string.Equals(player.Status, "Passive", StringComparison.OrdinalIgnoreCase))
            .ToList();
        var activePlayerIds = activePlayers
            .Select(player => player.Id)
            .ToHashSet();
        var activeReports = reports
            .Where(report => activePlayerIds.Contains(report.PlayerId))
            .ToList();

        var latestReports = activeReports
            .OrderByDescending(report => report.CreatedAt)
            .Take(6)
            .Select(report => MapLatestReport(report, playersById))
            .ToList();

        var latestReportPerPlayer = activeReports
            .GroupBy(report => report.PlayerId)
            .Select(group => group
                .OrderByDescending(report => report.CreatedAt)
                .First())
            .ToList();

        var topPotentialPlayers = latestReportPerPlayer
            .OrderByDescending(report => report.PotentialScore)
            .ThenByDescending(report => report.OverallScore)
            .Take(6)
            .Select(report => MapTopPotentialPlayer(report, playersById))
            .ToList();

        var positionDistribution = activePlayers
            .GroupBy(player => player.Position)
            .Select(group => new PositionDistributionDto(group.Key, group.Count()))
            .OrderByDescending(item => item.Count)
            .ThenBy(item => item.Position)
            .ToList();

        return new DashboardSummaryDto(
            activePlayers.Count,
            activeReports.Count,
            Average(activeReports.Select(report => report.PotentialScore)),
            CountPlayersToWatch(latestReportPerPlayer),
            latestReports,
            topPotentialPlayers,
            positionDistribution,
            BuildWarnings(activeReports, playersById),
            Average(activeReports.Select(report => report.TechnicalScore)),
            Average(activeReports.Select(report => report.PhysicalScore)),
            Average(activeReports.Select(report => report.TacticalScore)),
            Average(activeReports.Select(report => report.MentalScore)));
    }

    private static LatestScoutReportDto MapLatestReport(
        ScoutReport report,
        IReadOnlyDictionary<int, Player> playersById)
    {
        return new LatestScoutReportDto(
            report.Id,
            report.PlayerId,
            GetPlayerName(report.PlayerId, playersById),
            report.Title,
            report.PotentialScore,
            report.OverallScore,
            report.Recommendation,
            report.CreatedAt);
    }

    private static TopPotentialPlayerDto MapTopPotentialPlayer(
        ScoutReport report,
        IReadOnlyDictionary<int, Player> playersById)
    {
        playersById.TryGetValue(report.PlayerId, out var player);

        return new TopPotentialPlayerDto(
            report.PlayerId,
            GetPlayerName(report.PlayerId, playersById),
            player?.Position ?? "Unknown",
            report.PotentialScore,
            report.SuggestedScore ?? 0,
            report.Recommendation,
            report.Tags ?? string.Empty);
    }

    private static IReadOnlyList<string> BuildWarnings(
        IEnumerable<ScoutReport> reports,
        IReadOnlyDictionary<int, Player> playersById)
    {
        var warningKeywords = new[]
        {
            "savunma", "karar verme", "top kaybı", "riskli", "kondisyon", "yoruldu", "fiziksel düşüş"
        };

        return reports
            .OrderByDescending(report => report.CreatedAt)
            .Where(report => warningKeywords.Any(keyword =>
                Contains(report.ObservationText, keyword)
                || Contains(report.Weaknesses, keyword)
                || Contains(report.DevelopmentAdvice, keyword)))
            .Take(5)
            .Select(report => $"{GetPlayerName(report.PlayerId, playersById)}: {BuildWarningText(report)}")
            .ToList();
    }

    private static string BuildWarningText(ScoutReport report)
    {
        if (!string.IsNullOrWhiteSpace(report.Weaknesses))
        {
            return report.Weaknesses;
        }

        if (!string.IsNullOrWhiteSpace(report.DevelopmentAdvice))
        {
            return report.DevelopmentAdvice;
        }

        return "Gelişim alanı içeren son scout raporu";
    }

    private static int CountPlayersToWatch(IEnumerable<ScoutReport> latestReportPerPlayer)
    {
        return latestReportPerPlayer.Count(report =>
            report.PotentialScore >= 80
            || Contains(report.Recommendation, "izlenmeli")
            || Contains(report.Recommendation, "takip")
            || Contains(report.Tags, "hizli oyuncu")
            || Contains(report.Tags, "teknik oyuncu"));
    }

    private static string GetPlayerName(
        int playerId,
        IReadOnlyDictionary<int, Player> playersById)
    {
        return playersById.TryGetValue(playerId, out var player)
            ? $"{player.FirstName} {player.LastName}"
            : $"Player #{playerId}";
    }

    private static double Average(IEnumerable<int> values)
    {
        var list = values.ToList();
        return list.Count == 0 ? 0 : Math.Round(list.Average(), 1);
    }

    private static bool Contains(string? value, string keyword)
    {
        return value?.Contains(keyword, StringComparison.OrdinalIgnoreCase) == true;
    }
}
