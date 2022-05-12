using AutoMapper;
using ZenBuilds.Entities;
using ZenBuilds.Helpers;
using ZenBuilds.Models.Builds;
using ZenBuilds.Models.Likes;

namespace ZenBuilds.Services;

public interface ILikeService
{
    void ToggleLike(LikeRequest likeRequest);
    IEnumerable<GetLikeResponse> GetBuildLikes(int buildId);
    IEnumerable<GetLikeResponse> GetUserLikes(int userId);

}

public class LikeService : ILikeService
{
    private DataContext _context;
    private readonly IMapper _mapper;
    private IBuildService _buildService;

    public LikeService(DataContext context, IMapper mapper, IBuildService buildService)
    {
        _context = context;
        _mapper = mapper;
        _buildService = buildService;

    }

    public void ToggleLike(LikeRequest likeRequest)
    {
        var like = _mapper.Map<Like>(likeRequest);

        

        if (!_context.Builds.Any(x => x.Id == likeRequest.BuildId))
            throw new Exception("No Build found");

        if(_context.Likes.Any(x => x.BuildId == likeRequest.BuildId && x.UserId == likeRequest.UserId))
        {
            _context.Likes.Remove(like);
        }
        else
        {
            like.LikeDate = DateTime.Now;

            _context.Likes.Add(like);   
        }


        _context.SaveChanges();
    }

    public IEnumerable<GetLikeResponse> GetBuildLikes(int buildId)
    {
        var likes = _context.Likes.Where(x => x.BuildId == buildId)
            .Select(user => _mapper.Map<GetLikeResponse>(user.User));

        return likes;
    }

    public IEnumerable<GetLikeResponse> GetUserLikes(int userId)
    {
        var likes = _context.Likes.Where(x => x.UserId == userId)
            .Select(user => _mapper.Map<GetLikeResponse>(user.User));

        return likes;
    }
}
