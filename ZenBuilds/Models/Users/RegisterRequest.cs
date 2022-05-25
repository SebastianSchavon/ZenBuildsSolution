namespace ZenBuilds.Models.Users;

using System.ComponentModel.DataAnnotations;
using ZenBuilds.Helpers;

public class RegisterRequest
{
    [Required(ErrorMessage = "Username is required."), RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Only letters, numbers and underscore allowed")]
    [MaxLength(12, ErrorMessage = "Username cannot exceed more than 12 characters"), MinLength(3, ErrorMessage = "Username must be more than 3 characters")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Pick a main race as profile image")]
    public string ProfileImage { get; set; }

    [MaxLength(12, ErrorMessage = "Password cannote exceed more than 12 characters"), MinLength(4, ErrorMessage = "Password must be more than 4 characters")]
    [DataType(DataType.Password), Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords dont match")]
    public string ConfirmPassword { get; set; }

}