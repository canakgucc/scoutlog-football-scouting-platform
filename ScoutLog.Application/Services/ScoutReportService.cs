using ScoutLog.Application.DTOs.ScoutReports;
using ScoutLog.Application.Interfaces.Repositories;
using ScoutLog.Application.Interfaces.Security;
using ScoutLog.Application.Interfaces.Services;
using ScoutLog.Domain.Entities;

namespace ScoutLog.Application.Services;

public class ScoutReportService(
    IScoutReportRepository scoutReportRepository,
    IPlayerRepository playerRepository,
    IUserRepository userRepository,
    IReportAnalysisService reportAnalysisService,
    ICurrentUserContext currentUserContext) : IScoutReportService
{
    public async Task<IReadOnlyList<ScoutReportDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var reports = await scoutReportRepository.GetAllAsync(cancellationToken);
        var activePlayerIds = await GetActivePlayerIdsAsync(cancellationToken);

        return reports
            .Where(report => activePlayerIds.Contains(report.PlayerId))
            .OrderByDescending(report => report.CreatedAt)
            .Select(MapToDto)
            .ToList();
    }

    public async Task<IReadOnlyList<ScoutReportDto>> GetByPlayerIdAsync(
        int playerId,
        CancellationToken cancellationToken = default)
    {
        if (playerId <= 0)
        {
            throw new ArgumentException("Player id must be greater than zero.", nameof(playerId));
        }

        var player = await playerRepository.GetByIdAsync(playerId, cancellationToken);
        if (player is null || !CanAccess(player) || IsPassive(player))
        {
            return [];
        }

        var reports = await scoutReportRepository.GetByPlayerIdAsync(playerId, cancellationToken);
        return reports.Select(MapToDto).ToList();
    }

    public async Task<ScoutReportDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Scout report id must be greater than zero.", nameof(id));
        }

        var report = await scoutReportRepository.GetByIdAsync(id, cancellationToken);
        if (report is null)
        {
            return null;
        }

        var player = await playerRepository.GetByIdAsync(report.PlayerId, cancellationToken);
        return player is null || !CanAccess(player) || IsPassive(player) ? null : MapToDto(report);
    }

    public async Task<ScoutReportDto> CreateAsync(
        CreateScoutReportDto request,
        CancellationToken cancellationToken = default)
    {
        ValidateScoutReport(request);

        await EnsurePlayerExistsAsync(request.PlayerId, cancellationToken);
        await EnsureScoutExistsAsync(currentUserContext.UserId, cancellationToken);

        var analysis = reportAnalysisService.Analyze(request.ObservationText);
        var report = new ScoutReport
        {
            PlayerId = request.PlayerId,
            ScoutId = currentUserContext.UserId,
            Title = request.Title.Trim(),
            ObservationText = request.ObservationText.Trim(),
            TechnicalScore = request.TechnicalScore,
            PhysicalScore = request.PhysicalScore,
            TacticalScore = request.TacticalScore,
            MentalScore = request.MentalScore,
            PotentialScore = request.PotentialScore,
            OverallScore = CalculateOverallScore(
                request.TechnicalScore,
                request.PhysicalScore,
                request.TacticalScore,
                request.MentalScore,
                request.PotentialScore),
            Recommendation = request.Recommendation.Trim(),
            AnalysisSummary = analysis.Summary,
            Strengths = JoinAnalysisValues(analysis.Strengths),
            Weaknesses = JoinAnalysisValues(analysis.Weaknesses),
            SuggestedActions = analysis.DevelopmentAdvice,
            SuggestedScore = analysis.SuggestedScore,
            Tags = JoinAnalysisValues(analysis.Tags),
            DevelopmentAdvice = analysis.DevelopmentAdvice,
            CreatedAt = DateTime.UtcNow
        };

        await scoutReportRepository.AddAsync(report, cancellationToken);
        await scoutReportRepository.SaveChangesAsync(cancellationToken);

        return MapToDto(report);
    }

    public async Task<ScoutReportDto?> UpdateAsync(
        int id,
        UpdateScoutReportDto request,
        CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Scout report id must be greater than zero.", nameof(id));
        }

        ValidateScoutReport(request);

        var report = await scoutReportRepository.GetByIdAsync(id, cancellationToken);
        if (report is null)
        {
            return null;
        }

        var player = await playerRepository.GetByIdAsync(report.PlayerId, cancellationToken);
        if (player is null || !CanAccess(player) || IsPassive(player))
        {
            return null;
        }

        var analysis = reportAnalysisService.Analyze(request.ObservationText);

        report.Title = request.Title.Trim();
        report.ObservationText = request.ObservationText.Trim();
        report.TechnicalScore = request.TechnicalScore;
        report.PhysicalScore = request.PhysicalScore;
        report.TacticalScore = request.TacticalScore;
        report.MentalScore = request.MentalScore;
        report.PotentialScore = request.PotentialScore;
        report.OverallScore = CalculateOverallScore(
            request.TechnicalScore,
            request.PhysicalScore,
            request.TacticalScore,
            request.MentalScore,
            request.PotentialScore);
        report.Recommendation = request.Recommendation.Trim();
        report.AnalysisSummary = analysis.Summary;
        report.Strengths = JoinAnalysisValues(analysis.Strengths);
        report.Weaknesses = JoinAnalysisValues(analysis.Weaknesses);
        report.SuggestedActions = analysis.DevelopmentAdvice;
        report.SuggestedScore = analysis.SuggestedScore;
        report.Tags = JoinAnalysisValues(analysis.Tags);
        report.DevelopmentAdvice = analysis.DevelopmentAdvice;

        scoutReportRepository.Update(report);
        await scoutReportRepository.SaveChangesAsync(cancellationToken);

        return MapToDto(report);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Scout report id must be greater than zero.", nameof(id));
        }

        var report = await scoutReportRepository.GetByIdAsync(id, cancellationToken);
        if (report is null)
        {
            return false;
        }

        var player = await playerRepository.GetByIdAsync(report.PlayerId, cancellationToken);
        if (player is null || !CanAccess(player))
        {
            return false;
        }

        scoutReportRepository.Delete(report);
        await scoutReportRepository.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<AnalysisResultDto?> AnalyzeAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Scout report id must be greater than zero.", nameof(id));
        }

        var report = await scoutReportRepository.GetByIdAsync(id, cancellationToken);
        if (report is null)
        {
            return null;
        }

        var player = await playerRepository.GetByIdAsync(report.PlayerId, cancellationToken);
        if (player is null || !CanAccess(player) || IsPassive(player))
        {
            return null;
        }

        var analysis = reportAnalysisService.Analyze(report.ObservationText);

        report.AnalysisSummary = analysis.Summary;
        report.Strengths = JoinAnalysisValues(analysis.Strengths);
        report.Weaknesses = JoinAnalysisValues(analysis.Weaknesses);
        report.SuggestedActions = analysis.DevelopmentAdvice;
        report.SuggestedScore = analysis.SuggestedScore;
        report.Tags = JoinAnalysisValues(analysis.Tags);
        report.DevelopmentAdvice = analysis.DevelopmentAdvice;

        scoutReportRepository.Update(report);
        await scoutReportRepository.SaveChangesAsync(cancellationToken);

        return new AnalysisResultDto(
            report.Id,
            analysis.Summary,
            analysis.Strengths,
            analysis.Weaknesses,
            analysis.SuggestedScore,
            analysis.Tags,
            analysis.DevelopmentAdvice);
    }

    private async Task EnsurePlayerExistsAsync(int playerId, CancellationToken cancellationToken)
    {
        var player = await playerRepository.GetByIdAsync(playerId, cancellationToken);
        if (player is null)
        {
            throw new InvalidOperationException($"Player with id {playerId} does not exist.");
        }

        if (!CanAccess(player) || IsPassive(player))
        {
            throw new InvalidOperationException($"Player with id {playerId} is not available.");
        }
    }

    private async Task<HashSet<int>> GetActivePlayerIdsAsync(CancellationToken cancellationToken)
    {
        var players = await playerRepository.GetAllAsync(cancellationToken);

        return players
            .Where(player => CanAccess(player) && !IsPassive(player))
            .Select(player => player.Id)
            .ToHashSet();
    }

    private bool CanAccess(Player player)
    {
        return player.ClubId == currentUserContext.ClubId;
    }

    private static bool IsPassive(Player player)
    {
        return string.Equals(player.Status, "Passive", StringComparison.OrdinalIgnoreCase);
    }

    private async Task EnsureScoutExistsAsync(int scoutId, CancellationToken cancellationToken)
    {
        var scout = await userRepository.GetByIdAsync(scoutId, cancellationToken);
        if (scout is null)
        {
            throw new InvalidOperationException($"Scout user with id {scoutId} does not exist.");
        }

        if (scout.ClubId != currentUserContext.ClubId)
        {
            throw new InvalidOperationException($"Scout user with id {scoutId} is not available.");
        }
    }

    private static ScoutReportDto MapToDto(ScoutReport report)
    {
        return new ScoutReportDto(
            report.Id,
            report.PlayerId,
            report.ScoutId,
            report.Title,
            report.ObservationText,
            report.TechnicalScore,
            report.PhysicalScore,
            report.TacticalScore,
            report.MentalScore,
            report.PotentialScore,
            report.OverallScore,
            report.Recommendation,
            report.AnalysisSummary,
            report.Strengths,
            report.Weaknesses,
            report.SuggestedActions,
            report.SuggestedScore,
            report.Tags,
            report.DevelopmentAdvice,
            report.CreatedAt);
    }

    private static void ValidateScoutReport(CreateScoutReportDto request)
    {
        if (request.PlayerId <= 0)
        {
            throw new ArgumentException("Player id must be greater than zero.", nameof(request));
        }

        ValidateSharedScoutReportFields(
            request.Title,
            request.ObservationText,
            request.TechnicalScore,
            request.PhysicalScore,
            request.TacticalScore,
            request.MentalScore,
            request.PotentialScore,
            request.Recommendation);
    }

    private static void ValidateScoutReport(UpdateScoutReportDto request)
    {
        ValidateSharedScoutReportFields(
            request.Title,
            request.ObservationText,
            request.TechnicalScore,
            request.PhysicalScore,
            request.TacticalScore,
            request.MentalScore,
            request.PotentialScore,
            request.Recommendation);
    }

    private static void ValidateSharedScoutReportFields(
        string title,
        string observationText,
        int technicalScore,
        int physicalScore,
        int tacticalScore,
        int mentalScore,
        int potentialScore,
        string recommendation)
    {
        if (string.IsNullOrWhiteSpace(title)
            || string.IsNullOrWhiteSpace(observationText)
            || string.IsNullOrWhiteSpace(recommendation))
        {
            throw new ArgumentException("Required scout report fields cannot be empty.");
        }

        if (observationText.Trim().Length < 20)
        {
            throw new ArgumentException("Observation text must be at least 20 characters.");
        }

        foreach (var score in new[] { technicalScore, physicalScore, tacticalScore, mentalScore, potentialScore })
        {
            if (score is < 0 or > 100)
            {
                throw new ArgumentException("Scores must be between 0 and 100.");
            }
        }
    }

    private static int CalculateOverallScore(
        int technicalScore,
        int physicalScore,
        int tacticalScore,
        int mentalScore,
        int potentialScore)
    {
        return (int)Math.Round(
            (technicalScore + physicalScore + tacticalScore + mentalScore + potentialScore) / 5.0,
            MidpointRounding.AwayFromZero);
    }

    private static string JoinAnalysisValues(IEnumerable<string> values)
    {
        return string.Join("; ", values);
    }
}
