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
    [DataType(DataType.Password)]
    public string? NewPassword { get; set; }
    

}
