using System.ComponentModel.DataAnnotations.Schema;

namespace ZenBuilds.Entities;

public class Follower
{
    public int User_UserId { get; set; }
    public User User_User { get; set; }

    public int Follower_UserId { get; set; }
    public User Follower_User { get; set; }

    public string FollowDate { get; set; }
}
