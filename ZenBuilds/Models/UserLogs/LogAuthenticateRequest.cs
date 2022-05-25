namespace ZenBuilds.Models.UserLogs;

public class LogAuthenticateRequest
{
    public string Ip { get; set; }
    public string Username { get; set; }
    public bool AuthSuccessful { get; set; }
    

}
