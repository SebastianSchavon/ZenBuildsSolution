using System.ComponentModel.DataAnnotations;
using ZenBuilds.Helpers;

namespace ZenBuilds.Models.Builds;

public class CreateBuildRequest
{
    [Required]
    public string Title { get; set; }

    [Required]
    [DataType(DataType.MultilineText)]
    public string Content { get; set; }

    [Required]
    [EnumDataType(typeof(Race), ErrorMessage = "Allowed values: Terran, Protoss, Zerg, Any")]
    public Race PlayerRace { get; set; }

    [Required]
    [EnumDataType(typeof(Race), ErrorMessage = "Allowed values: Terran, Protoss, Zerg, Any")]
    public Race OpponentRace { get; set; }
}
