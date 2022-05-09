using Microsoft.AspNetCore.Mvc;
using ZenBuilds.Authorization;
using ZenBuilds.Services;

namespace ZenBuilds.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class FollowersController : ControllerBase
{
    private IFollowerService _followerService;
    
    public FollowersController(IFollowerService followerService)
    {
        _followerService = followerService;
    }
}
