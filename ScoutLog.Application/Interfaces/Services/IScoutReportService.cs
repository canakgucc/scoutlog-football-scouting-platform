using ScoutLog.Application.DTOs.ScoutReports;

namespace ScoutLog.Application.Interfaces.Services;

public interface IScoutReportService
{
    Task<IReadOnlyList<ScoutReportDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ScoutReportDto>> GetByPlayerIdAsync(int playerId, CancellationToken cancellationToken = default);
    Task<ScoutReportDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ScoutReportDto> CreateAsync(CreateScoutReportDto request, CancellationToken cancellationToken = default);
    Task<ScoutReportDto?> UpdateAsync(int id, UpdateScoutReportDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<AnalysisResultDto?> AnalyzeAsync(int id, CancellationToken cancellationToken = default);
}
