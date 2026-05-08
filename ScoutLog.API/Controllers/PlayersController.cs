using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoutLog.Application.DTOs.Players;
using ScoutLog.Application.Interfaces.Services;

namespace ScoutLog.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PlayersController(
    IPlayerService playerService,
    ILogger<PlayersController> logger) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PlayerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<PlayerDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var players = await playerService.GetAllAsync(cancellationToken);
            return Ok(players);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while getting players.");
            return Problem("An unexpected error occurred while getting players.");
        }
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PlayerDto>> GetById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest("Player id must be greater than zero.");
        }

        try
        {
            var player = await playerService.GetByIdAsync(id, cancellationToken);

            return player is null
                ? NotFound($"Player with id {id} was not found.")
                : Ok(player);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while getting player {PlayerId}.", id);
            return Problem("An unexpected error occurred while getting the player.");
        }
    }

    [HttpGet("team/{teamId:int}")]
    [ProducesResponseType(typeof(IReadOnlyList<PlayerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<PlayerDto>>> GetByTeamId(
        int teamId,
        CancellationToken cancellationToken)
    {
        if (teamId <= 0)
        {
            return BadRequest("Team id must be greater than zero.");
        }

        try
        {
            var players = await playerService.GetByTeamIdAsync(teamId, cancellationToken);
            return Ok(players);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while getting players for team {TeamId}.", teamId);
            return Problem("An unexpected error occurred while getting team players.");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PlayerDto>> Create(
        [FromBody] CreatePlayerDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            var player = await playerService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = player.Id }, player);
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
            logger.LogError(ex, "An unexpected error occurred while creating a player.");
            return Problem("An unexpected error occurred while creating the player.");
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PlayerDto>> Update(
        int id,
        [FromBody] UpdatePlayerDto request,
        CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest("Player id must be greater than zero.");
        }

        try
        {
            var player = await playerService.UpdateAsync(id, request, cancellationToken);

            return player is null
                ? NotFound($"Player with id {id} was not found.")
                : Ok(player);
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
            logger.LogError(ex, "An unexpected error occurred while updating player {PlayerId}.", id);
            return Problem("An unexpected error occurred while updating the player.");
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
            return BadRequest("Player id must be greater than zero.");
        }

        try
        {
            var deleted = await playerService.DeleteAsync(id, cancellationToken);

            return deleted
                ? NoContent()
                : NotFound($"Player with id {id} was not found.");
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
            logger.LogError(ex, "An unexpected error occurred while deleting player {PlayerId}.", id);
            return Problem("An unexpected error occurred while deleting the player.");
        }
    }
}
