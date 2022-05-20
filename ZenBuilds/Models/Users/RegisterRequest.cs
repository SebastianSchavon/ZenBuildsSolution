﻿namespace ZenBuilds.Models.Users;

using System.ComponentModel.DataAnnotations;
using ZenBuilds.Helpers;

public class RegisterRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    [EnumDataType(typeof(Race), ErrorMessage = "Allowed values: Terran, Protoss, Zerg")]
    public Race Race { get; set; }
    [Required]
    public string ProfileImage { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords dont match")]
    public string ConfirmPassword { get; set; }


    //[Required]
    //[EmailAddress]
    //public string Email { get; set; }
}