namespace ZenBuilds.Models.Users;

using System.ComponentModel.DataAnnotations;
using ZenBuilds.Helpers;

public class RegisterRequest
{
    [Required(ErrorMessage = "Username is required.")]
    [RegularExpression(@"^[^\p{P}\p{Sm}]*$", ErrorMessage = "No special characters in username")]
    public string Username { get; set; }

    //[Required]
    //[EnumDataType(typeof(Race), ErrorMessage = "Allowed values: Terran, Protoss, Zerg")]
    //public Race Race { get; set; }
    [Required(ErrorMessage = "Pick a main race.")]
    public string ProfileImage { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords dont match")]
    public string ConfirmPassword { get; set; }


    //[Required]
    //[EmailAddress]
    //public string Email { get; set; }
}