namespace ZenBuilds.Authorization;

/// <summary>
///     Enables AllowAnonymous attribute on methods
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class AllowAnonymousAttribute : Attribute
{
}
