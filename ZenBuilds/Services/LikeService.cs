using AutoMapper;
using ZenBuilds.Helpers;
using ZenBuilds.Models.Likes;

namespace ZenBuilds.Services;

public interface ILikeService
{
    void AddLike(LikeCompositeKey likeCompositeKey);
    void RemoveLike(LikeCompositeKey likeCompositeKey);
    void GetBuildLikes(int buildId);
    void GetUserLikes(int userId);

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

    public void AddLike(LikeCompositeKey likeCompositeKey)
    {
        throw new NotImplementedException();
    }

    public void RemoveLike(LikeCompositeKey likeCompositeKey)
    {
        throw new NotImplementedException();
    }

    public void GetBuildLikes(int buildId)
    {
        throw new NotImplementedException();
    }

    public void GetUserLikes(int userId)
    {
        throw new NotImplementedException();
    }
}
