using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace ZenBuilds.Helpers;

public interface IStringManagement
{
    public string WhitespaceRemoval(string content);

}

/// <summary>
///     Remove unnecessary whitespace from string
///     Practical use when build content is copied from spawningtool website
/// </summary>
public class StringManagement : IStringManagement
{
    public string WhitespaceRemoval(string content)
    {

        Regex.Replace(content, @"\n +", "\n");
        Regex.Replace(content, @"\n+", "\n");
        Regex.Replace(content, @"\s+", "");

        return Regex.Replace(content, @"\t+", " ");

    }
}


