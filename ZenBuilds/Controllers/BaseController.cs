using Microsoft.AspNetCore.Mvc;
using ZenBuilds.Authorization;
using ZenBuilds.Entities;

namespace ZenBuilds.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BaseController : ControllerBase
{
    [HttpGet("user")]
    public User GetAuthenticatedUser()
    => (User)HttpContext.Items["User"];

    [HttpGet("userId")]
    public int GetAuthenticatedUserId()
        => ((GetAuthenticatedUser())!).Id;
}
