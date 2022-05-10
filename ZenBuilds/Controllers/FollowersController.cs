using Microsoft.AspNetCore.Mvc;
using ZenBuilds.Authorization;
using ZenBuilds.Models.Followers;
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

    [HttpPost("addFollow")]
    public IActionResult AddFollow(FollowCompositeKey followCompositeKey)
    {
        try
        {
            _followerService.AddFollow(followCompositeKey);
            return Ok(new { message = "Follow created" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }

    }

    [HttpPost("removeFollow")]
    public IActionResult RemoveFollow(FollowCompositeKey followCompositeKey)
    {
        try
        {
            _followerService.RemoveFollow(followCompositeKey);
            return Ok(new { message = "Removed follow" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }

    }

    [HttpGet("getUserFollowers")]
    public IActionResult GetUserFollowers(int follower_UserId)
    {
        var userFollowers = _followerService.GetUserFollowers(follower_UserId);
        return Ok(userFollowers);

    }

    [HttpGet("getUserFollowing")]
    public IActionResult GetUserFollowing(int user_UserId)
    {
        var userFollowing = _followerService.GetUserFollowing(user_UserId);
        return Ok(userFollowing);

    }
}
