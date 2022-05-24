namespace ZenBuilds.Models.Users;

using System.ComponentModel.DataAnnotations;
using ZenBuilds.Helpers;

public class RegisterRequest
{
    /// <summary>
    ///     Regular expression:
    ///     ^[^\p{P}\p{Sm}]*$ no speical characters
    ///     [\r\n\t\f ] no whitespaces, newlines, tabs or anything like that
    /// </summary>
    [Required(ErrorMessage = "Username is required.")]
    [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Only letters, numbers and underscore allowed")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Pick a main race as profile image")]
    public string ProfileImage { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    /// <summary>
    ///     Compare and match Password with Confirm Password     
    /// </summary>
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords dont match")]
    public string ConfirmPassword { get; set; }

}