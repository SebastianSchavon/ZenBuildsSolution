namespace ZenBuilds.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ZenBuilds.Entities;

/// <summary>
///     Enables Authorize attribute on methods and classes
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    /// <summary>
    ///     Configure Authorize attribute
    ///     
    ///         Return if method is decorated with AllowAnonymous attribute
    ///         Set result message to Unauthorized if context doesnt contain a user claim
    /// </summary>
    /// <param name="context"> Contains the HTTP context and metadata about the controller, endpoint, etc </param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {

        if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
            return;

        if ((User)context.HttpContext.Items["User"] == null)
            context.Result = new JsonResult(new { message = "Unatuhorized" }) { StatusCode = StatusCodes.Status401Unauthorized };

    }
}
