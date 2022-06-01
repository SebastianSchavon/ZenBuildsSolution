namespace ZenBuilds.Authorization;

using ZenBuilds.Helpers;
using ZenBuilds.Services;

public class JwtMiddleware
{
    // a function that can process an HTTP request.
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    ///     Retrieve token from request header
    ///     Retrieve user id from token and get user by id from db 
    ///     Set user to context items
    /// </summary>
    /// <param name="context"> Contains the request </param>
    /// <param name="userService"> GetUserById method </param>
    /// <param name="jwtUtils"> ValidateToken method </param>
    /// <returns></returns>
    public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = jwtUtils.ValidateToken(token);
        if (userId != null)
        {
            context.Items["User"] = userService.GetUserById(userId.Value);
        }

        await _next(context);
    }
}
