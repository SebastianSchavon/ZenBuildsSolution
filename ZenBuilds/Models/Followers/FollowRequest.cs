using System.ComponentModel.DataAnnotations;

namespace ZenBuilds.Models.Followers;

public class FollowRequest
{
    [Required]
    public int User_UserId { get; set; }
    [Required]
    public int Follower_UserId { get; set; }

}
