using ScoutLog.Application.DTOs.PerformanceMetrics;

namespace ScoutLog.Application.Interfaces.Services;

public interface IPerformanceMetricService
{
    Task<IReadOnlyList<PerformanceMetricDto>> GetByPlayerIdAsync(int playerId, CancellationToken cancellationToken = default);
    Task<PerformanceMetricDto> CreateAsync(CreatePerformanceMetricDto request, CancellationToken cancellationToken = default);
}
