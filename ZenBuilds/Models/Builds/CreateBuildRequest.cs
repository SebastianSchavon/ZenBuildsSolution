using ZenBuilds.Helpers;

namespace ZenBuilds.Models.Builds;

public class CreateBuildRequest
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Race PlayerRace { get; set; }
    public Race OpponentRace { get; set; }
}
