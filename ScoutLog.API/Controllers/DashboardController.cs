using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoutLog.Application.DTOs.Dashboard;
using ScoutLog.Application.Interfaces.Services;

namespace ScoutLog.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class DashboardController(
    IDashboardService dashboardService,
    ILogger<DashboardController> logger) : ControllerBase
{
    [HttpGet("summary")]
    [ProducesResponseType(typeof(DashboardSummaryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<DashboardSummaryDto>> GetSummary(CancellationToken cancellationToken)
    {
        try
        {
            var summary = await dashboardService.GetSummaryAsync(cancellationToken);
            return Ok(summary);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while getting dashboard summary.");
            return Problem("An unexpected error occurred while getting dashboard summary.");
        }
    }
}
