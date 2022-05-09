using Microsoft.AspNetCore.Mvc;
using ZenBuilds.Authorization;
using ZenBuilds.Models.Builds;
using ZenBuilds.Services;

namespace ZenBuilds.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BuildsController : ControllerBase
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
    public IActionResult DeleteBuild(int userId, int id)
    {
        try
        {
            _buildService.DeleteBuild(userId, id);
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

    [HttpPatch("likeBuild")]
    public IActionResult LikeBuild(int userId, int id)
    {
        try
        {
            _buildService.LikeBuild(userId, id);
            return Ok(new { message = "Build recieved like" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPatch("removeLike")]
    public IActionResult RemoveLike(int userId, int id)
    {
        try
        {
            _buildService.RemoveLike(userId, id);
            return Ok(new { message = "Like removed" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPatch("getPostById")]
    public IActionResult Update(int userId, int id)
    {
        try
        {
            var build = _buildService.GetPostById(userId, id);
            return Ok(build);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
