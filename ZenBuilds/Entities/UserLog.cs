using System.ComponentModel.DataAnnotations.Schema;

namespace ZenBuilds.Entities;

public class UserLog
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public string Ip { get; set; }
    public bool AuthSuccessful { get; set; }
    [Column(TypeName = "SmallDateTime ")]
    public DateTime Date { get; set; }
}
