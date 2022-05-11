using System.ComponentModel.DataAnnotations.Schema;
using ZenBuilds.Helpers;

namespace ZenBuilds.Entities;

public class Build
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int LikesCount { get; set; }

    [Column(TypeName = "SmallDateTime ")]
    public DateTime Published { get; set; }
    public Race PlayerRace { get; set; }
    public Race OpponentRace { get; set; }
}
