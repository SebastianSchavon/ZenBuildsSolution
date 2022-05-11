using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ZenBuilds.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public int ZenPoints { get; set; }
    public string? Description { get; set; }
    [JsonIgnore]
    public string PasswordHash { get; set; }
    //public string Email { get; set; }
    [Column(TypeName = "SmallDateTime ")]
    public DateTime RegisterDate { get; set; }
    public List<Build> Builds { get; set; }
    public List<Like> LikedBuilds { get; set; }
    public List<UserLog> UserLogs { get; set; }
    
    public List<Follower> Followers { get; set; }
    public List<Follower> Following { get; set; }




}
