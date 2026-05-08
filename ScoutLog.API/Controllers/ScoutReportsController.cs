using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoutLog.Application.DTOs.ScoutReports;
using ScoutLog.Application.Interfaces.Services;

namespace ScoutLog.API.Controllers;

[ApiController]
[Authorize]
[Route("api/scout-reports")]
public class ScoutReportsController(
    IScoutReportService scoutReportService,
    ILogger<ScoutReportsController> logger) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ScoutReportDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<ScoutReportDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var reports = await scoutReportService.GetAllAsync(cancellationToken);
            return Ok(reports);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while getting scout reports.");
            return Problem("An unexpected error occurred while getting scout reports.");
        }
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ScoutReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ScoutReportDto>> GetById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest("Scout report id must be greater than zero.");
        }

        try
        {
            var report = await scoutReportService.GetByIdAsync(id, cancellationToken);

            return report is null
                ? NotFound($"Scout report with id {id} was not found.")
                : Ok(report);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while getting scout report {ScoutReportId}.", id);
            return Problem("An unexpected error occurred while getting the scout report.");
        }
    }

    [HttpGet("player/{playerId:int}")]
    [ProducesResponseType(typeof(IReadOnlyList<ScoutReportDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<ScoutReportDto>>> GetByPlayerId(
        int playerId,
        CancellationToken cancellationToken)
    {
        if (playerId <= 0)
        {
            return BadRequest("Player id must be greater than zero.");
        }

        try
        {
            var reports = await scoutReportService.GetByPlayerIdAsync(playerId, cancellationToken);
            return Ok(reports);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while getting scout reports for player {PlayerId}.", playerId);
            return Problem("An unexpected error occurred while getting player scout reports.");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(ScoutReportDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ScoutReportDto>> Create(
        [FromBody] CreateScoutReportDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            var report = await scoutReportService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = report.Id }, report);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while creating a scout report.");
            return Problem("An unexpected error occurred while creating the scout report.");
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ScoutReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ScoutReportDto>> Update(
        int id,
        [FromBody] UpdateScoutReportDto request,
        CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest("Scout report id must be greater than zero.");
        }

        try
        {
            var report = await scoutReportService.UpdateAsync(id, request, cancellationToken);

            return report is null
                ? NotFound($"Scout report with id {id} was not found.")
                : Ok(report);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while updating scout report {ScoutReportId}.", id);
            return Problem("An unexpected error occurred while updating the scout report.");
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest("Scout report id must be greater than zero.");
        }

        try
        {
            var deleted = await scoutReportService.DeleteAsync(id, cancellationToken);

            return deleted
                ? NoContent()
                : NotFound($"Scout report with id {id} was not found.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while deleting scout report {ScoutReportId}.", id);
            return Problem("An unexpected error occurred while deleting the scout report.");
        }
    }

    [HttpPost("{id:int}/analyze")]
    [ProducesResponseType(typeof(AnalysisResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AnalysisResultDto>> Analyze(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest("Scout report id must be greater than zero.");
        }

        try
        {
            var result = await scoutReportService.AnalyzeAsync(id, cancellationToken);

            return result is null
                ? NotFound($"Scout report with id {id} was not found.")
                : Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while analyzing scout report {ScoutReportId}.", id);
            return Problem("An unexpected error occurred while analyzing the scout report.");
        }
    }
}
