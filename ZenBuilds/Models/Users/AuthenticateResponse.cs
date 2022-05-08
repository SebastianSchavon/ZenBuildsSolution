namespace ZenBuilds.Models.Users;

using ZenBuilds.Entities;
using ZenBuilds.Helpers;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string Username { get; set; }
    public Race Race { get; set; }
    public string Token { get; set; }
}
