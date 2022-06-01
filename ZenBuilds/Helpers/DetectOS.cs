namespace ZenBuilds.Helpers;

/// <summary>
///     Detect applications running operative system
/// </summary>
public static class DetectOS
{
    public static bool IsMacOs()
    {
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            return false;
        else
            return true;
    }
}
