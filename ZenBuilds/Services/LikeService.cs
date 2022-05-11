using AutoMapper;
using ZenBuilds.Entities;
using ZenBuilds.Helpers;
using ZenBuilds.Models.Likes;

namespace ZenBuilds.Services;

public interface ILikeService
{
    void ToggleLike(LikeCompositeKey likeCompositeKey);
    IEnumerable<GetLikeResponse> GetBuildLikes(int buildId);
    IEnumerable<GetLikeResponse> GetUserLikes(int userId);

}

public class LikeService : ILikeService
{
    private DataContext _context;
    private readonly IMapper _mapper;

    public LikeService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }

    public void ToggleLike(LikeCompositeKey likeCompositeKey)
    {
        var likes = _context.Likes;
        var like = _mapper.Map<Like>(likeCompositeKey);

        if (!_context.Builds.Any(x => x.Id == likeCompositeKey.BuildId))
            throw new Exception("No Build found");

        if (likes.Any(x => x.BuildId == likeCompositeKey.BuildId) &&
            likes.Any(x => x.UserId == likeCompositeKey.UserId))
            likes.Remove(like);
        else
        {
            like.LikeDate = DateTime.Now;
            likes.Add(like);
        }
            
    }

    public IEnumerable<GetLikeResponse> GetBuildLikes(int buildId)
    {
        var likes = _context.Likes.Where(x => x.BuildId == buildId)
            .Select(user => _mapper.Map<GetLikeResponse>(user.User.Username));

        return likes;
    }

    public IEnumerable<GetLikeResponse> GetUserLikes(int userId)
    {
        var likes = _context.Likes.Where(x => x.UserId == userId)
            .Select(user => _mapper.Map<GetLikeResponse>(user.User.Username));

        return likes;
    }
}
