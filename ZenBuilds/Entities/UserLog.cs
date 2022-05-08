﻿namespace ZenBuilds.Entities;

public class UserLog
{
    // create composite key of username and id?
    public int Id { get; set; }
    public string Username { get; set; }
    public string Ip { get; set; }
    public bool AuthSuccessful { get; set; }
    public DateTime Date { get; set; }
}
