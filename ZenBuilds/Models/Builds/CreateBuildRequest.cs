using System.ComponentModel.DataAnnotations;
using ZenBuilds.Helpers;

namespace ZenBuilds.Models.Builds;

public class CreateBuildRequest
{
    [Required]
    [MaxLength(150, ErrorMessage = "Title cannot exceed more than 150 characters"), MinLength(6, ErrorMessage = "Title must be more than 6 characters")]
    public string Title { get; set; }

    [Required]
    [DataType(DataType.MultilineText), MaxLength(2500, ErrorMessage = "Build cannot exceed more than 2500 characters"), MinLength(8, ErrorMessage = "Build must be more than 8 characters")]
    public string Content { get; set; }

    [Required]
    [EnumDataType(typeof(Race), ErrorMessage = "Allowed values: Terran, Protoss, Zerg, Any")]
    public Race PlayerRace { get; set; }

    [Required]
    [EnumDataType(typeof(Race), ErrorMessage = "Allowed values: Terran, Protoss, Zerg, Any")]
    public Race OpponentRace { get; set; }
}
