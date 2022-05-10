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

    // test: try to get an exception thrown and see difference between with or without trycatch
    [HttpPost("createBuild")]
    public IActionResult CreateBuild(CreateBuildRequest createBuildRequest)
    {
        try
        {
            _buildService.CreateBuild(createBuildRequest);
            return Ok(new { message = "Register success" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("delete")]
    public IActionResult DeleteBuild(BuildCompositeKey buildCompositeKey)
    {
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

    [HttpGet("getBuildsByUserId")]
    public IActionResult GetBuildsByUserId(int userId)
    {
        var buildsByUserId = _buildService.GetBuildsByUserId(userId);
        return Ok(buildsByUserId);
    }

    [HttpGet("getBuildsByUserIdLatest")]
    public IActionResult GetBuildsByUserIdLatest(int userId)
    {
        var buildsByUserId = _buildService.GetBuildsByUserIdLatest(userId);
        return Ok(buildsByUserId);
    }

    [HttpGet("getFollowingBuilds")]
    public IActionResult GetFollowingBuilds(int userId)
    {
        var buildsByUserId = _buildService.GetFollowingBuilds(userId);
        return Ok(buildsByUserId);
    }

    [HttpGet("getFollowingBuildsLatest")]
    public IActionResult GetFollowingBuildsLatest(int userId)
    {
        var buildsByUserId = _buildService.GetFollowingBuildsLatest(userId);
        return Ok(buildsByUserId);
    }

    [HttpPatch("toggleBuildLike")]
    public IActionResult ToggleBuildLike(ToggleLikeRequest toggleLikeRequest)
    {
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

    [HttpPatch("getBuildById")]
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
