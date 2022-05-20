using System.ComponentModel.DataAnnotations;

namespace ZenBuilds.Models.Users;

public class UpdateRequest
{
    [Required]
    public string? Username { get; set; }

    public string? Description { get; set; }
    [Required]
    public string ProfileImage { get; set; }

    public string? OldPassword { get; set; }

    public string? NewPassword { get; set; }
    //public stri?ng Email { get; set; }

}
