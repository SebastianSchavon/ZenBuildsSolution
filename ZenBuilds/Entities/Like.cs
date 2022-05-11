using System.ComponentModel.DataAnnotations.Schema;

namespace ZenBuilds.Entities;

public class Like
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int BuildId { get; set; }
    public Build Build { get; set; }

    [Column(TypeName = "SmallDateTime ")]
    public DateTime LikeDate { get; set; }
}
