using System.ComponentModel.DataAnnotations;

namespace ZenBuilds.Models.Builds;

public class ToggleLikeRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    [Required]
    public int BuildId { get; set; }
}
