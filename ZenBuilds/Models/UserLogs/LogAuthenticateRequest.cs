namespace ZenBuilds.Models.UserLogs;

public class LogAuthenticateRequest
{
    public int UserId { get; set; }
    public string Ip { get; set; }
    public bool AuthSuccessful { get; set; }
    public DateTime Date { get; set; }

}
