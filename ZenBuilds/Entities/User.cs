namespace ZenBuilds.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }

    // sum of build likes - not required
    public int ZenPoints { get; set; }
    // not required
    public string Description { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public DateTime RegisterDate { get; set; }

    // fluent api configuration needed because double collection reference?
    public List<Build> Builds { get; set; }
    public List<Build> FavoriteBuilds { get; set; }

    public List<Follower> Followers { get; set; }
    public List<Follower> Following { get; set; }




}
