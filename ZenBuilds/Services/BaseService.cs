using AutoMapper;
using ZenBuilds.Entities;
using ZenBuilds.Helpers;
using ZenBuilds.Models.Builds;
using ZenBuilds.Models.Followers;
using ZenBuilds.Models.Likes;

namespace ZenBuilds.Services;

public interface IBaseService
{
    void UpdateZenPoints(int userId);
    void UpdateAllZenPoints();
    int UpdateBuildLikes(int buildId);

}

public class BaseService : IBaseService
{
    private DataContext _context;
    private readonly IMapper _mapper;


    public BaseService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }

    public void UpdateZenPoints(int userId)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == userId);
        var builds = _context.Builds.Where(x => x.UserId == userId);
        user.ZenPoints = builds.Sum(x => x.LikesCount);
        
        _context.SaveChanges();
        
    }

    public void UpdateAllZenPoints()
    {
        foreach (var user in _context.Users)
        {
            var builds = _context.Builds.Where(_x => _x.UserId == user.Id);
            user.ZenPoints = builds.Sum(x => x.LikesCount);
        }

        _context.SaveChanges();
    }

    public int UpdateBuildLikes(int buildId)
    {
        var build = _context.Builds.FirstOrDefault(x => x.Id == buildId);

        if (build == null)
            throw new Exception("Build not found");

        var likes = _context.Likes.Where(x => x.BuildId == buildId);

        build.LikesCount = likes.Count();
        
        _context.SaveChanges();
        UpdateAllZenPoints();
        return build.LikesCount;
    }


}
