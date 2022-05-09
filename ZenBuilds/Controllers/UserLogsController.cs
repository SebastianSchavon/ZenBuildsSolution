using Microsoft.AspNetCore.Mvc;
using ZenBuilds.Authorization;
using ZenBuilds.Models.UserLogs;
using ZenBuilds.Services;

namespace ZenBuilds.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserLogsController : ControllerBase
{
    private readonly IUserLogService _userLogService;

    public UserLogsController(IUserLogService userLogService)
    {
        _userLogService = userLogService;
    }

    [HttpPost("logAuthentication")]
    public IActionResult LogAuthentication(LogAuthenticateRequest logAuthenticateRequest)
    {
        try
        {
            _userLogService.LogAuthentication(logAuthenticateRequest);
            return Ok(new { message = "Authentication logged" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getAllLogs")]
    public IActionResult GetAllLogs()
    {
        var allLogs = _userLogService.GetAllLogs();
        return Ok(allLogs);
    }    

    [HttpGet("getAllLogsByUserId")]
    public IActionResult GetAllLogsByUserId(int userId)
    {
        var allLogsByUserId = _userLogService.GetAllLogsByUserId(userId);
        return Ok(allLogsByUserId);
    }    

    [HttpGet("getAllSuccessfulAuthenticationsByUserId")]
    public IActionResult GetAllSuccessfulAuthenticationsByUserId(int userId)
    {
        var allSuccessfulAuthenticationsByUserId = _userLogService.GetAllSuccessfulAuthenticationsByUserId(userId);
        return Ok(allSuccessfulAuthenticationsByUserId);
    }

    [HttpGet("getAllFailedAuthenticationsByUserId")]
    public IActionResult GetAllFailedAuthenticationsByUserId(int userId)
    {
        var allFailedAuthenticationsByUserId = _userLogService.GetAllFailedAuthenticationsByUserId(userId);
        return Ok(allFailedAuthenticationsByUserId);
    }
}
