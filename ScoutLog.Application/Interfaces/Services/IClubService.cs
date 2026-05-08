using ScoutLog.Application.DTOs.Clubs;

namespace ScoutLog.Application.Interfaces.Services;

public interface IClubService
{
    Task<IReadOnlyList<ClubDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ClubDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ClubDto> CreateAsync(CreateClubDto request, CancellationToken cancellationToken = default);
    Task<ClubDto?> UpdateAsync(int id, UpdateClubDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
