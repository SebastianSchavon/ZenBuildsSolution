using System.ComponentModel.DataAnnotations;

namespace ZenBuilds.Models.Users;

public class UpdateRequest
{
    [Required]
    [MaxLength(10, ErrorMessage = "Max length: 10"), MinLength(3, ErrorMessage = "Min length: 3")]
    public string? Username { get; set; }
    
    [MaxLength(50, ErrorMessage = "Max length: 50")]
    public string? Description { get; set; }

    [Required]
    public string ProfileImage { get; set; }

    public string? OldPassword { get; set; }
    [MaxLength(12, ErrorMessage = "Max length: 12"), MinLength(4, ErrorMessage = "Min length: 4")]
    [DataType(DataType.Password)]
    public string? NewPassword { get; set; }
    

}
