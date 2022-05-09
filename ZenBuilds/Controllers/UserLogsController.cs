using Microsoft.AspNetCore.Mvc;
using ZenBuilds.Authorization;
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
}
