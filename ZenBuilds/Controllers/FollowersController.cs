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
        var followRequest = new FollowRequest
        {
            User_UserId = GetAuthenticatedUserId(),
            Follower_UserId = follower_UserId
        };

        try
        {
            _followerService.AddFollow(followRequest);
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
        var followRequest = new FollowRequest
        {
            User_UserId = GetAuthenticatedUserId(),
            Follower_UserId = follower_UserId
        };

        try
        {
            _followerService.RemoveFollow(followRequest);
            return Ok(new { message = "Removed follow" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }

    }

    [HttpGet("followCheck/{follower_UserId}")]
    public IActionResult FollowCheck(int follower_UserId)
    {
        var followRequest = new FollowRequest
        {
            User_UserId = GetAuthenticatedUserId(),
            Follower_UserId = follower_UserId
        };

        var followCheck = _followerService.FollowCheck(followRequest);
        return Ok(followCheck);
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
