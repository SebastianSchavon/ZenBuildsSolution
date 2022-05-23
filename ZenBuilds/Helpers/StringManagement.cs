using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace ZenBuilds.Helpers;

public static class StringManagement
{
    public static string WhitespaceRemoval(string content)
    {

        //var textLines = content.Split('\n');
        //foreach (var line in textLines)
        //{
        //    Regex.Replace(line, @"\s+", "  ");
        //}
        
        Regex.Replace(content, @"\n+", "\n");
        Regex.Replace(content, @"\s+", "");

        return Regex.Replace(content, @"\t+", " ");




    }
}


