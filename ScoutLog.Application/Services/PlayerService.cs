using ScoutLog.Application.DTOs.Players;
using ScoutLog.Application.Interfaces.Repositories;
using ScoutLog.Application.Interfaces.Security;
using ScoutLog.Application.Interfaces.Services;
using ScoutLog.Domain.Entities;

namespace ScoutLog.Application.Services;

public class PlayerService(
    IPlayerRepository playerRepository,
    IScoutReportRepository scoutReportRepository,
    IPerformanceMetricRepository performanceMetricRepository,
    IRepository<Team> teamRepository,
    ICurrentUserContext currentUserContext) : IPlayerService
{
    public async Task<IReadOnlyList<PlayerDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var players = await playerRepository.GetAllAsync(cancellationToken);
        var clubId = currentUserContext.ClubId;

        return players
            .Where(player => player.ClubId == clubId && !IsPassive(player))
            .OrderBy(player => player.LastName)
            .ThenBy(player => player.FirstName)
            .Select(MapToDto)
            .ToList();
    }

    public async Task<IReadOnlyList<PlayerDto>> GetByTeamIdAsync(
        int teamId,
        CancellationToken cancellationToken = default)
    {
        if (teamId <= 0)
        {
            throw new ArgumentException("Team id must be greater than zero.", nameof(teamId));
        }

        var players = await playerRepository.GetByTeamIdAsync(teamId, cancellationToken);
        var clubId = currentUserContext.ClubId;

        return players
            .Where(player => player.ClubId == clubId && !IsPassive(player))
            .Select(MapToDto)
            .ToList();
    }

    public async Task<PlayerDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Player id must be greater than zero.", nameof(id));
        }

        var player = await playerRepository.GetByIdAsync(id, cancellationToken);
        return player is null || !CanAccess(player) || IsPassive(player) ? null : MapToDto(player);
    }

    public async Task<PlayerDto> CreateAsync(
        CreatePlayerDto request,
        CancellationToken cancellationToken = default)
    {
        ValidatePlayer(request);
        await EnsureTeamBelongsToCurrentClubAsync(request.TeamId, cancellationToken);

        var player = new Player
        {
            ClubId = currentUserContext.ClubId,
            TeamId = request.TeamId,
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            BirthDate = request.BirthDate,
            Position = request.Position.Trim(),
            PreferredFoot = request.PreferredFoot.Trim(),
            Height = request.Height,
            Weight = request.Weight,
            Nationality = request.Nationality.Trim(),
            PhotoUrl = string.IsNullOrWhiteSpace(request.PhotoUrl) ? null : request.PhotoUrl.Trim(),
            Status = request.Status.Trim()
        };

        await playerRepository.AddAsync(player, cancellationToken);
        await playerRepository.SaveChangesAsync(cancellationToken);

        return MapToDto(player);
    }

    public async Task<PlayerDto?> UpdateAsync(
        int id,
        UpdatePlayerDto request,
        CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Player id must be greater than zero.", nameof(id));
        }

        ValidatePlayer(request);

        var player = await playerRepository.GetByIdAsync(id, cancellationToken);
        if (player is null || !CanAccess(player))
        {
            return null;
        }

        await EnsureTeamBelongsToCurrentClubAsync(request.TeamId, cancellationToken);

        player.TeamId = request.TeamId;
        player.FirstName = request.FirstName.Trim();
        player.LastName = request.LastName.Trim();
        player.BirthDate = request.BirthDate;
        player.Position = request.Position.Trim();
        player.PreferredFoot = request.PreferredFoot.Trim();
        player.Height = request.Height;
        player.Weight = request.Weight;
        player.Nationality = request.Nationality.Trim();
        player.PhotoUrl = string.IsNullOrWhiteSpace(request.PhotoUrl) ? null : request.PhotoUrl.Trim();
        player.Status = request.Status.Trim();

        playerRepository.Update(player);
        await playerRepository.SaveChangesAsync(cancellationToken);

        return MapToDto(player);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Player id must be greater than zero.", nameof(id));
        }

        var player = await playerRepository.GetByIdAsync(id, cancellationToken);
        if (player is null || !CanAccess(player))
        {
            return false;
        }

        var scoutReports = await scoutReportRepository.GetByPlayerIdAsync(id, cancellationToken);
        foreach (var scoutReport in scoutReports)
        {
            scoutReportRepository.Delete(scoutReport);
        }

        var performanceMetrics = await performanceMetricRepository.GetByPlayerIdAsync(id, cancellationToken);
        foreach (var performanceMetric in performanceMetrics)
        {
            performanceMetricRepository.Delete(performanceMetric);
        }

        playerRepository.Delete(player);
        await playerRepository.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static PlayerDto MapToDto(Player player)
    {
        return new PlayerDto(
            player.Id,
            player.ClubId,
            player.TeamId,
            player.FirstName,
            player.LastName,
            player.BirthDate,
            player.Position,
            player.PreferredFoot,
            player.Height,
            player.Weight,
            player.Nationality,
            player.PhotoUrl,
            player.Status);
    }

    private static void ValidatePlayer(CreatePlayerDto request)
    {
        ValidateSharedPlayerFields(
            request.TeamId,
            request.FirstName,
            request.LastName,
            request.Position,
            request.PreferredFoot,
            request.Height,
            request.Weight,
            request.Nationality,
            request.Status);
    }

    private static void ValidatePlayer(UpdatePlayerDto request)
    {
        ValidateSharedPlayerFields(
            request.TeamId,
            request.FirstName,
            request.LastName,
            request.Position,
            request.PreferredFoot,
            request.Height,
            request.Weight,
            request.Nationality,
            request.Status);
    }

    private static void ValidateSharedPlayerFields(
        int? teamId,
        string firstName,
        string lastName,
        string position,
        string preferredFoot,
        int height,
        int weight,
        string nationality,
        string status)
    {
        if (teamId <= 0)
        {
            throw new ArgumentException("Team id must be greater than zero when provided.", nameof(teamId));
        }

        if (string.IsNullOrWhiteSpace(firstName)
            || string.IsNullOrWhiteSpace(lastName)
            || string.IsNullOrWhiteSpace(position)
            || string.IsNullOrWhiteSpace(preferredFoot)
            || string.IsNullOrWhiteSpace(nationality)
            || string.IsNullOrWhiteSpace(status))
        {
            throw new ArgumentException("Required player fields cannot be empty.");
        }

        if (height is < 120 or > 230)
        {
            throw new ArgumentException("Height must be between 120 and 230.");
        }

        if (weight is < 35 or > 130)
        {
            throw new ArgumentException("Weight must be between 35 and 130.");
        }
    }

    private bool CanAccess(Player player)
    {
        return player.ClubId == currentUserContext.ClubId;
    }

    private async Task EnsureTeamBelongsToCurrentClubAsync(
        int? teamId,
        CancellationToken cancellationToken)
    {
        if (!teamId.HasValue)
        {
            return;
        }

        var team = await teamRepository.GetByIdAsync(teamId.Value, cancellationToken);
        if (team is null || team.ClubId != currentUserContext.ClubId)
        {
            throw new InvalidOperationException("Team does not belong to the current user's club.");
        }
    }

    private static bool IsPassive(Player player)
    {
        return string.Equals(player.Status, "Passive", StringComparison.OrdinalIgnoreCase);
    }
}
