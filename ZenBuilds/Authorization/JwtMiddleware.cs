namespace ZenBuilds.Authorization;

using ZenBuilds.Helpers;
using ZenBuilds.Services;

public class JwtMiddleware
{
    // a function that can process an HTTP request.
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        // next middleware?
        _next = next;
    }

    /// <summary>
    /// 
    /// </summary>
    public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = jwtUtils.ValidateToken(token);
        if (userId != null)
        {
            // attach user to context on successful jwt validation
            context.Items["User"] = userService.GetUserById(userId.Value);
        }

        await _next(context);
    }
}
