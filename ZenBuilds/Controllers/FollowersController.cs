using Microsoft.AspNetCore.Mvc;
using ZenBuilds.Authorization;
using ZenBuilds.Models.Followers;
using ZenBuilds.Services;

namespace ZenBuilds.Controllers;

public class FollowersController : BaseController
{
    private IFollowerService _followerService;

    public FollowersController(IFollowerService followerService)
    {
        _followerService = followerService;
    }

    [HttpPost("addFollow/{follower_UserId}")]
    public IActionResult AddFollow(int follower_UserId)
    {
        var followCompositeKey = new FollowCompositeKey
        {
            User_UserId = GetAuthenticatedUser().Id,
            Follower_UserId = follower_UserId
        };

        try
        {
            _followerService.AddFollow(followCompositeKey);
            return Ok(new { message = "Follow created" });
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

    }

    [HttpDelete("removeFollow/{follower_UserId}")]
    public IActionResult RemoveFollow(int follower_UserId)
    {
        var followCompositeKey = new FollowCompositeKey
        {
            User_UserId = GetAuthenticatedUser().Id,
            Follower_UserId = follower_UserId
        };

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

    [HttpGet("getUserFollowers/{follower_UserId}")]
    public IActionResult GetUserFollowers(int follower_UserId)
    {
        var userFollowers = _followerService.GetUserFollowers(follower_UserId);
        return Ok(userFollowers);

    }

    [HttpGet("getUserFollowing/{user_UserId}")]
    public IActionResult GetUserFollowing(int user_UserId)
    {
        var userFollowing = _followerService.GetUserFollowing(user_UserId);
        return Ok(userFollowing);

    }
}
