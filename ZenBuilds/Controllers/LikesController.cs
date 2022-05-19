﻿using Microsoft.AspNetCore.Mvc;
using ZenBuilds.Models.Builds;
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

    // change to put?
    [HttpPut("toggleLike/{id}")]
    public IActionResult ToggleLike(int id)
    {
        var likeRequest = new LikeRequest
        {
            BuildId = id,
            UserId = GetAuthenticatedUserId()
        };

        try
        {
            _likeService.ToggleLike(likeRequest);
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
