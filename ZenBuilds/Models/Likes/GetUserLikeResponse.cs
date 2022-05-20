namespace ZenBuilds.Models.Likes;

public class GetUserLikeResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string ProfileImage { get; set; }
    public string Content { get; set; }
    public int LikesCount { get; set; }
}

