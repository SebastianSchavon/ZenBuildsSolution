using System.ComponentModel.DataAnnotations;

namespace ZenBuilds.Models.Builds;

public class ToggleLikeRequest
{
    [Required]
    public BuildCompositeKey BuildId { get; set; }

    [Required]
    public int Current_UserId { get; set; }
}
