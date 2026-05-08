using ScoutLog.Application.DTOs.Teams;

namespace ScoutLog.Application.Interfaces.Services;

public interface ITeamService
{
    Task<IReadOnlyList<TeamDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TeamDto>> GetByClubIdAsync(int clubId, CancellationToken cancellationToken = default);
    Task<TeamDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<TeamDto> CreateAsync(CreateTeamDto request, CancellationToken cancellationToken = default);
    Task<TeamDto?> UpdateAsync(int id, UpdateTeamDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
