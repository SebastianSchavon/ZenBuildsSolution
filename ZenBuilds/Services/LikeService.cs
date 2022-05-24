using AutoMapper;
using ZenBuilds.Entities;
using ZenBuilds.Helpers;
using ZenBuilds.Models.Builds;
using ZenBuilds.Models.Likes;

namespace ZenBuilds.Services;

public interface ILikeService
{
    int ToggleLike(LikeRequest likeRequest);
    IEnumerable<GetBuildLikeResponse> GetBuildLikes(int buildId);
    IEnumerable<GetUserLikeResponse> GetUserLikes(int userId);

}

public class LikeService : ILikeService
{
    private DataContext _context;
    private readonly IMapper _mapper;
    private IBaseService _baseService;

    public LikeService(DataContext context, IMapper mapper, IBaseService baseService)
    {
        _context = context;
        _mapper = mapper;
        _baseService = baseService;

    }

    // split into three methods?
    public int ToggleLike(LikeRequest likeRequest)
    {
        if (!_context.Builds.Any(x => x.Id == likeRequest.BuildId))
            throw new Exception("No Build found");

        if(_context.Likes.Any(x => x.BuildId == likeRequest.BuildId && x.UserId == likeRequest.UserId))
        {
            var like = _context.Likes.SingleOrDefault(x => x.BuildId == likeRequest.BuildId && x.UserId == likeRequest.UserId);
            _context.Likes.Remove(like);
        }
        else
        {
            var like = _mapper.Map<Like>(likeRequest);

            like.LikeDate = DateTime.Now.ToString("yyyy-MM-dd");
            _context.Likes.Add(like);   
        }

        _context.SaveChanges();


        
        
        return _baseService.UpdateBuildLikes(likeRequest.BuildId);
    }

    public IEnumerable<GetBuildLikeResponse> GetBuildLikes(int buildId)
    {
        var likes = _context.Likes.Where(x => x.BuildId == buildId)
            .Select(user => _mapper.Map<GetBuildLikeResponse>(user.User));

        return likes;
    }

    public IEnumerable<GetUserLikeResponse> GetUserLikes(int userId)
    {
        var likes = _context.Likes.Where(x => x.UserId == userId)
            .Select(user => _mapper.Map<GetUserLikeResponse>(user.Build));

        return likes;
    }
}
