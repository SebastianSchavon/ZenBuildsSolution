using System.ComponentModel.DataAnnotations;

namespace ZenBuilds.Models.Builds;

public class BuildCompositeKey
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public int BuildId { get; set; }
}
