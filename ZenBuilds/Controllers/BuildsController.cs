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
        try
        {
            _buildService.DeleteBuild(GetAuthenticatedUserId(), id);
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

    [HttpGet("getAuthenticatedUserFeed")]
    public IActionResult GetAuthenticatedUserFeed()
    {
        var followingBuilds = _buildService.GetAuthenticatedUserFeed(GetAuthenticatedUserId());
        return Ok(followingBuilds);
    }

    [HttpGet("getAuthenticatedUserFeedLatest")]
    public IActionResult GetAuthenticatedUserFeedLatest()
    {
        var followingBuilds = _buildService.GetAuthenticatedUserFeedLatest(GetAuthenticatedUserId());
        return Ok(followingBuilds);
    }

    [HttpGet("getBuildById/{buildId}")]
    public IActionResult GetBuildById(int buildId)
    {
        try
        {
            var build = _buildService.GetBuildResponseById(buildId);
            return Ok(build);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
