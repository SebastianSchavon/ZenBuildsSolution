using System.ComponentModel.DataAnnotations;
using ZenBuilds.Helpers;

namespace ZenBuilds.Models.Builds;

public class CreateBuildRequest
{
    [Required]
    [MaxLength(250, ErrorMessage = "Max length: 50"), MinLength(8, ErrorMessage = "Min length: 4")]
    public string Title { get; set; }

    [Required]
    [DataType(DataType.MultilineText), MaxLength(250, ErrorMessage = "Max length: 250"), MinLength(8, ErrorMessage = "Min length: 8")]
    public string Content { get; set; }

    [Required]
    [EnumDataType(typeof(Race), ErrorMessage = "Allowed values: Terran, Protoss, Zerg, Any")]
    public Race PlayerRace { get; set; }

    [Required]
    [EnumDataType(typeof(Race), ErrorMessage = "Allowed values: Terran, Protoss, Zerg, Any")]
    public Race OpponentRace { get; set; }
}
