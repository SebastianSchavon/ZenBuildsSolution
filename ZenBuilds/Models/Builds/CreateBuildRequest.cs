using System.ComponentModel.DataAnnotations;
using ZenBuilds.Helpers;

namespace ZenBuilds.Models.Builds;

public class CreateBuildRequest
{
    [Required]
    [MaxLength(250, ErrorMessage = "Title cannot exceed more than 50 characters"), MinLength(8, ErrorMessage = "Title must be more than 4 characters")]
    public string Title { get; set; }

    [Required]
    [DataType(DataType.MultilineText), MaxLength(250, ErrorMessage = "Build cannot exceed more than 250 characters"), MinLength(8, ErrorMessage = "Build must be more than 8 characters")]
    public string Content { get; set; }

    [Required]
    [EnumDataType(typeof(Race), ErrorMessage = "Allowed values: Terran, Protoss, Zerg, Any")]
    public Race PlayerRace { get; set; }

    [Required]
    [EnumDataType(typeof(Race), ErrorMessage = "Allowed values: Terran, Protoss, Zerg, Any")]
    public Race OpponentRace { get; set; }
}
