using System.ComponentModel.DataAnnotations.Schema;
using ZenBuilds.Models.Builds;

namespace ZenBuilds.Entities;

public class Like
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public int BuildId { get; set; }
    public Build Build { get; set; }

    public string LikeDate { get; set; }
}
