namespace ZenBuilds.Models.Users;

using System.ComponentModel.DataAnnotations;
using ZenBuilds.Helpers;

public class RegisterRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public Race Race { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Email { get; set; }
}