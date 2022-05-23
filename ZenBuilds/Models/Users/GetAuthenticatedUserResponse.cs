using ZenBuilds.Models.Builds;
using ZenBuilds.Models.Followers;
using ZenBuilds.Models.Likes;

namespace ZenBuilds.Models.Users;

public class GetAuthenticatedUserResponse
{
    public int Id { get; set; }
    public string Username { get; set; }
    public int ZenPoints { get; set; }
    public string? Description { get; set; }
    public string ProfileImage { get; set; }
    public string RegisterDate { get; set; }

}
