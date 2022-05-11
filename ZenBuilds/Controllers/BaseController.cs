using Microsoft.AspNetCore.Mvc;
using ZenBuilds.Authorization;
using ZenBuilds.Entities;

namespace ZenBuilds.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BaseController : ControllerBase
{
    /// <summary>
    /// get the authenticated user
    /// 
    /// use:
    ///     service methods where the authenticated user is needed
    /// </summary>

    [HttpGet("user")]
    public User GetAuthenticatedUser()
    {
        return (User)HttpContext.Items["User"];
    }
    

    [HttpGet("userId")]
    public int GetAuthenticatedUserId()
    {
        return ((GetAuthenticatedUser())!).Id;
    }
        
}
