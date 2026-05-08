using ScoutLog.Application.DTOs.Players;

namespace ScoutLog.Application.Interfaces.Services;

public interface IPlayerService
{
    Task<IReadOnlyList<PlayerDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PlayerDto>> GetByTeamIdAsync(int teamId, CancellationToken cancellationToken = default);
    Task<PlayerDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PlayerDto> CreateAsync(CreatePlayerDto request, CancellationToken cancellationToken = default);
    Task<PlayerDto?> UpdateAsync(int id, UpdatePlayerDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
