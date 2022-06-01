using System.ComponentModel.DataAnnotations;

namespace ZenBuilds.Models.Users;

public class UpdateRequest
{
    [Required(ErrorMessage = "Username is required."), RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Only letters, numbers and underscore allowed")]
    [MaxLength(12, ErrorMessage = "Username cannot exceed more than 12 characters"), MinLength(3, ErrorMessage = "Username must be more than 3 characters")]
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
