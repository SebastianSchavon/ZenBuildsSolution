using Microsoft.AspNetCore.Mvc;
using ZenBuilds.Authorization;
using ZenBuilds.Models.UserLogs;
using ZenBuilds.Services;

namespace ZenBuilds.Controllers;

public class UserLogsController : BaseController
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

    [HttpGet("getAuthenticatedUserLogs")]
    public IActionResult GetAuthenticatedUserLogs()
    {
        var allLogsByUserId = _userLogService.GetAuthenticatedUserLogs(GetAuthenticatedUserId());
        return Ok(allLogsByUserId);
    }    

    [HttpGet("getSuccessfulAuthenticatedUserLogs")]
    public IActionResult GetSuccessfulAuthenticatedUserLogs()
    {
        var allSuccessfulAuthenticationsByUserId = _userLogService.GetSuccessfulAuthenticatedUserLogs(GetAuthenticatedUserId());
        return Ok(allSuccessfulAuthenticationsByUserId);
    }

    [HttpGet("getUnsuccessfulAuthenticatedUserLogs")]
    public IActionResult GetUnsuccessfulAuthenticatedUserLogs()
    {
        var allFailedAuthenticationsByUserId = _userLogService.GetUnsuccessfulAuthenticatedUserLogs(GetAuthenticatedUserId());
        return Ok(allFailedAuthenticationsByUserId);
    }

}
