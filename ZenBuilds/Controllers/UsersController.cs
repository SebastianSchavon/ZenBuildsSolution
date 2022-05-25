using Microsoft.AspNetCore.Mvc;
using ZenBuilds.Authorization;
using ZenBuilds.Models.Users;
using ZenBuilds.Services;

namespace ZenBuilds.Controllers;

public class UsersController : BaseController
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest authenticateRequest)
    {
        try
        {
            var response = _userService.Authenticate(authenticateRequest);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest registerRequest)
    {
        try
        {
            _userService.Register(registerRequest);
            return Ok(new { message = "Register success" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("update")]
    public IActionResult Update(UpdateRequest updateRequest)
    {
        try
        {
            _userService.Update(GetAuthenticatedUserId(), updateRequest);
            return Ok(new { message = "Profile updated" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("delete")]
    public IActionResult Delete()
    {
        try
        {
            _userService.Delete(GetAuthenticatedUserId());
            return Ok(new { message = "User deleted" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getAllUsers")]
    public IActionResult GetAllUsers()
    {
        var allUsers = _userService.GetAllUsers();
        return Ok(allUsers);
    }
    [HttpGet("getTop20Users")]
    public IActionResult GetTop20Users()
    {
        var allUsers = _userService.GetTop20Users();
        return Ok(allUsers);
    }

    [HttpGet("getUserByUsername/{username}")]
    public IActionResult GetUserByUsername(string username)
    {
        try
        {
            var user = _userService.GetUserByUsername(username);
            return Ok(user);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }

    }
    [HttpGet("getUserByUserId/{userId}")]
    public IActionResult GetUserByUsername(int userId)
    {
        try
        {
            var user = _userService.GetUserByUserId(userId);
            return Ok(user);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }

    }

    [HttpGet("getAuthenticatedUser")]
    public IActionResult GetUserByUsername()
    {
        try
        {
            var user = _userService.GetAuthenticatedUser(GetAuthenticatedUserId());
            return Ok(user);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }

    }

}
