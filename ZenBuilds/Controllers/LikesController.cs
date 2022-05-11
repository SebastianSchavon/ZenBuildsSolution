using Microsoft.AspNetCore.Mvc;
using ZenBuilds.Models.Likes;
using ZenBuilds.Services;

namespace ZenBuilds.Controllers;

public class LikesController : BaseController
{
    private ILikeService _likeService;

    public LikesController(ILikeService likeService)
    {
        _likeService = likeService;
    }

    [HttpPost("toggleLike/{buildId}")]
    public IActionResult AddFollow(int buildId)
    {
        var likeCompositeKey = new LikeCompositeKey
        {
            UserId = GetAuthenticatedUserId(),
            BuildId = buildId
        };

        try
        {
            _likeService.ToggleLike(likeCompositeKey);
            return Ok(new { message = "Like toggled" });
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

    }

    [HttpGet("getBuildLikes/{buildId}")]
    public IActionResult GetBuildLikes(int buildId)
    {
        try
        {
            var likes = _likeService.GetBuildLikes(buildId);
            return Ok(likes);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

    }

    [HttpGet("getUserLikes/{userId}")]
    public IActionResult GetUserLikes(int userId)
    {
        try
        {
            var likes = _likeService.GetUserLikes(userId);
            return Ok(likes);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

    }
}
