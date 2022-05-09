using Microsoft.AspNetCore.Mvc;
using ZenBuilds.Authorization;
using ZenBuilds.Models.Users;
using ZenBuilds.Services;

namespace ZenBuilds.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpGet("authenticate")]
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
    [HttpGet("register")]
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
    public IActionResult Update(int userId, UpdateRequest updateRequest)
    {
        try
        {
            _userService.Update(userId, updateRequest);
            return Ok(new { message = "Register success" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("delete")]
    public IActionResult Delete(int userId)
    {
        try
        {
            _userService.Delete(userId);
            return Ok(new { message = "User deleted" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // so... do I not need to try catch this?
    [HttpGet("getAllUsers")]
    public IActionResult GetAllUsers()
    {
        var allUsers = _userService.GetAllUsers();
        return Ok(allUsers);
    }

    [HttpGet("getUserByUsername")]
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

}
