using ZenBuilds.Entities;
using ZenBuilds.Models.Builds;

namespace ZenBuilds.Models.Users;

public class GetUserResponse
{
    public int Id { get; set; }
    public string Username { get; set; }
    public int ZenPoints { get; set; }
    public string? Description { get; set; }
    public string ProfileImage { get; set; }
    public DateTime RegisterDate { get; set; }
    public List<GetBuildResponse> Builds { get; set; }

}
