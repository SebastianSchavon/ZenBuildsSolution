namespace ZenBuilds.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ZenBuilds.Entities;

// specify valid attribute targets
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // if endpoint is decorated with AllowAnonymous attribute
        if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
            return;


        if ((User)context.HttpContext.Items["User"] == null)
            context.Result = new JsonResult(new { message = "Unatuhorized" }) { StatusCode = StatusCodes.Status401Unauthorized };

    }
}
