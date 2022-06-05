using System;
namespace ZenBuilds.Models.UserLogs;

public class UserLogResponse
{
   
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Ip { get; set; }
    public bool AuthSuccessful { get; set; }

    public string Date { get; set; }
}

