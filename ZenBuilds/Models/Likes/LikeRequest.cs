using System.ComponentModel.DataAnnotations;

namespace ZenBuilds.Models.Likes;

public class LikeRequest
{
    [Required]
    public int BuildId { get; set; }
    [Required]
    public int UserId { get; set; }
}
