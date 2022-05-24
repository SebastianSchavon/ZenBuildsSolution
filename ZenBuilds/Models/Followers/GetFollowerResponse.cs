﻿using ZenBuilds.Models.Likes;
using ZenBuilds.Models.Users;

namespace ZenBuilds.Models.Followers;

public class GetFollowerResponse
{
    public int User_UserId { get; set; }
    public GetFollowerUserResponse User_User { get; set; }

    public int Follower_UserId { get; set; }
    public GetFollowerUserResponse Follower_User { get; set; }

    public string FollowDate { get; set; }

}
