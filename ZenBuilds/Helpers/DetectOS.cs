namespace ZenBuilds.Helpers;

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
