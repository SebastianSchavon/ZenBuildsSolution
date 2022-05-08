namespace ZenBuilds.Models.Users;

public class UpdateRequest
{
    public string Username { get; set; }
    public string Description { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string Email { get; set; }

}
