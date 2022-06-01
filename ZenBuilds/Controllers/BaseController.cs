using Microsoft.AspNetCore.Mvc;
using ZenBuilds.Authorization;
using ZenBuilds.Entities;

namespace ZenBuilds.Controllers;


/// <summary>
///     Derive all controller classes from this parent
///     Child classes inherit attributes
///     
///     Methods
///         Retrieve and return the authenticated user and user id from the http context
/// </summary>
[ApiController]
[Route("[controller]")]
[Authorize]
public class BaseController : ControllerBase
{
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
