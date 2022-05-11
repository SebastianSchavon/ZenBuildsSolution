using Microsoft.AspNetCore.Mvc;
using ZenBuilds.Authorization;
using ZenBuilds.Models.Builds;
using ZenBuilds.Services;

namespace ZenBuilds.Controllers;

public class BuildsController : BaseController
{
    private IBuildService _buildService;

    public BuildsController(IBuildService buildService)
    {
        _buildService = buildService;
    }

    [HttpPost("createBuild")]
    public IActionResult CreateBuild(CreateBuildRequest createBuildRequest)
    {
        try
        {
            _buildService.CreateBuild(GetAuthenticatedUserId(), createBuildRequest);
            return Ok(new { message = "Build posted" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("delete/{id}")]
    public IActionResult DeleteBuild(int id)
    {
        var buildCompositeKey = new BuildCompositeKey
        {
            UserId = GetAuthenticatedUserId(),
            Id = id
        };

        try
        {
            _buildService.DeleteBuild(buildCompositeKey);
            return Ok(new { message = "Build deleted" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("getAllBuilds")]
    public IActionResult GetAllBuilds()
    {
        var allBuilds = _buildService.GetAllBuilds();
        return Ok(allBuilds);
    }

    [HttpGet("getAllBuildsLatest")]
    public IActionResult GetAllBuildsLatest()
    {
        var allBuilds = _buildService.GetAllBuildsLatest();
        return Ok(allBuilds);
    }

    [HttpGet("getBuildsByUserId/{userId}")]
    public IActionResult GetBuildsByUserId(int userId)
    {
        var userFeed = _buildService.GetBuildsByUserId(userId);
        return Ok(userFeed);
    }

    [HttpGet("getBuildsByUserIdLatest/{userId}")]
    public IActionResult GetBuildsByUserIdLatest(int userId)
    {
        var userFeed = _buildService.GetBuildsByUserIdLatest(userId);
        return Ok(userFeed);
    }

    [HttpGet("GetAuthenticatedUserFollowingBuilds")]
    public IActionResult GetAuthenticatedUserFollowingBuilds()
    {
        var followingBuilds = _buildService.GetAuthenticatedUserFeed(GetAuthenticatedUserId());
        return Ok(followingBuilds);
    }

    [HttpGet("getAuthenticatedUserFollowingBuildsLatest")]
    public IActionResult GetAuthenticatedUserFollowingBuildsLatest()
    {
        var followingBuilds = _buildService.GetAuthenticatedUserFeedLatest(GetAuthenticatedUserId());
        return Ok(followingBuilds);
    }

    [HttpPatch("toggleBuildLike")]
    public IActionResult ToggleBuildLike(BuildCompositeKey buildId)
    {
        var toggleLikeRequest = new ToggleLikeRequest
        {
            Current_UserId = GetAuthenticatedUserId(),
            BuildId = buildId
        };

        try
        {
            _buildService.ToggleBuildLike(toggleLikeRequest);
            return Ok(new { message = "Like toggled" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("getBuildLikes")]
    public IActionResult GetBuildLikes(BuildCompositeKey buildCompositeKey)
    {
        try
        {
            var likedBy = _buildService.GetBuildLikes(buildCompositeKey);
            return Ok(likedBy);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("getBuildById")]
    public IActionResult GetBuildById(BuildCompositeKey buildCompositeKey)
    {
        try
        {
            var build = _buildService.GetBuildById(buildCompositeKey);
            return Ok(build);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
