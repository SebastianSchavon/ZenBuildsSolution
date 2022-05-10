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

    [HttpGet("getAuthenticatedUserFeed")]
    public IActionResult GetAuthenticatedUserFeed()
    {
        var userFeed = _buildService.GetAuthenticatedUserFeed(GetAuthenticatedUserId());
        return Ok(userFeed);
    }

    [HttpGet("getBuildsByUserIdLatest")]
    public IActionResult GetBuildsByUserIdLatest()
    {
        var userFeed = _buildService.GetAuthenticatedUserFeedLatest(GetAuthenticatedUserId());
        return Ok(userFeed);
    }

    [HttpGet("getFollowingBuilds")]
    public IActionResult GetFollowingBuilds()
    {
        var followingBuilds = _buildService.GetFollowingBuilds(GetAuthenticatedUserId());
        return Ok(followingBuilds);
    }

    [HttpGet("getFollowingBuildsLatest")]
    public IActionResult GetFollowingBuildsLatest()
    {
        var followingBuilds = _buildService.GetFollowingBuildsLatest(GetAuthenticatedUserId());
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
