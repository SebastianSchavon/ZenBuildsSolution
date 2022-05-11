namespace ZenBuilds.Models.Followers;

public class GetFollowerResponse
{
    public int Follower_UserId { get; set; }
    public string Username { get; set; }
    public int ZenPoints { get; set; }
    public string? Description { get; set; }
    public DateTime FollowDate { get; set; }
}
