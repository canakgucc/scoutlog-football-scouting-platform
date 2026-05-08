using ScoutLog.Application.DTOs.Dashboard;

namespace ScoutLog.Application.Interfaces.Services;

public interface IDashboardService
{
    Task<DashboardSummaryDto> GetSummaryAsync(CancellationToken cancellationToken = default);
}
