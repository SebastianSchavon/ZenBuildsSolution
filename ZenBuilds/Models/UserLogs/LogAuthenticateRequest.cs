namespace ZenBuilds.Models.UserLogs;

public class LogAuthenticateRequest
{
    public string Ip { get; set; }
    public bool AuthSuccessful { get; set; }
    public string Date { get; set; }

}
