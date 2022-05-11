﻿using ZenBuilds.Entities;
using ZenBuilds.Helpers;

namespace ZenBuilds.Models.Builds;

public class GetBuildResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int LikesCount { get; set; }
    public DateTime Published { get; set; }
    public Race PlayerRace { get; set; }
    public Race OpponentRace { get; set; }
}