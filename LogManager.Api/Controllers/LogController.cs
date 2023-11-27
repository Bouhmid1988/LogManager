using LogManager.Core.Models;
using LogManager.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogManager.Api.Controllers;

[ApiController]
[Route("api/logs")]
public class LogController : Controller
{
    private readonly ILogMessageServices _logService;

    public LogController(ILogMessageServices logService)
    {
        _logService = logService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLogMessage([FromBody] List<LogMessageRequest> logMessages, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _logService.CreateLogMessage(logMessages, cancellationToken);
            return Ok("Log message saved successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }
}

