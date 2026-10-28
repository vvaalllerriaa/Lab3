using Microsoft.AspNetCore.Mvc;
using Theatre.BLL.Services;
using Theatre.BLL.DTOs;

namespace Theatre.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PerformancesController : ControllerBase
{
    private readonly TheatreService _theatreService;

    public PerformancesController(TheatreService theatreService)
    {
        _theatreService = theatreService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllPerformances()
    {
        try
        {
            var performances = await _theatreService.GetAllPerformancesAsync();
            return Ok(performances);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
    [HttpPost("buy")]
    public async Task<IActionResult> BuyTicket([FromQuery] int performanceId, [FromQuery] int seatNumber)
    {
        try
        {
            var ticketDto = await _theatreService.BuyTicketAsync(performanceId, seatNumber);
            return Ok(new { message = "Квиток успішно куплено!", ticket = ticketDto });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    [HttpPost("return/{ticketId}")]
    public async Task<IActionResult> ReturnTicket(int ticketId)
    {
        try
        {
            var refundAmount = await _theatreService.ReturnTicketAsync(ticketId);
            return Ok(new { message = "Квиток успішно повернено.", refund = refundAmount });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}