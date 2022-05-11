using AutoMapper;
using ZenBuilds.Entities;
using ZenBuilds.Helpers;
using ZenBuilds.Models.Builds;
using ZenBuilds.Models.Users;

namespace ZenBuilds.Services;

public interface IBuildService
{
    void CreateBuild(int userId, CreateBuildRequest createBuildRequest);
    void DeleteBuild(int userId, int id);

    IEnumerable<GetBuildResponse> GetAllBuilds();
    IEnumerable<GetBuildResponse> GetAllBuildsLatest();
    IEnumerable<GetBuildResponse> GetBuildsByUserId(int userId);
    IEnumerable<GetBuildResponse> GetBuildsByUserIdLatest(int userId);
    IEnumerable<GetBuildResponse> GetAuthenticatedUserFeed(int userId);
    IEnumerable<GetBuildResponse> GetAuthenticatedUserFeedLatest(int userId);

    Build GetBuildById(int buildId);
}

public class BuildService : IBuildService
{
    private DataContext _context;
    private readonly IMapper _mapper;
    private IFollowerService _followerService;
    private IUserService _userService;

    public BuildService(DataContext context, IMapper mapper, 
        IFollowerService followerService, IUserService userService)
    {
        _context = context;
        _mapper = mapper;
        _followerService = followerService;
        _userService = userService;
    }

    public void CreateBuild(int userId, CreateBuildRequest createBuildRequest)
    {
        var build = _mapper.Map<Build>(createBuildRequest);
        
        build.UserId = userId;
        build.Published = DateTime.Now.Date;

        _context.Builds.Add(build);
        _context.SaveChanges();
    }

    public void DeleteBuild(int userId, int id)
    {
        var build = _context.Builds.SingleOrDefault(x => x.UserId == userId && x.Id == id);

        if (build == null)
            throw new Exception("Build not found");
        
        _context.Builds.Remove(build);
        _context.SaveChanges();
    }

    /// <summary>
    /// returns the global feed: 
    ///     global feed:
    ///         all builds by every user
    ///         
    /// order:
    ///     builds with most likes on top
    /// </summary>
    public IEnumerable<GetBuildResponse> GetAllBuilds()
    {
        var builds = _context.Builds.Select(build => _mapper.Map<GetBuildResponse>(build));
        return builds;
    }

    /// <summary>
    /// returns the global feed: 
    ///     global feed:
    ///         all builds by every user
    ///         
    /// order:
    ///     builds published latest on top
    /// </summary>
    public IEnumerable<GetBuildResponse> GetAllBuildsLatest()
    {
        var builds = _context.Builds.Select(build => _mapper.Map<GetBuildResponse>(build));

        return builds.OrderBy(x => x.Published);
    }

    /// <summary>
    /// returns every build posted by a singel user: 
    ///         
    /// order:
    ///     builds with most likes on top
    /// </summary>
    public IEnumerable<GetBuildResponse> GetBuildsByUserId(int userId)
    {
        var builds = _context.Builds.Where(x => x.UserId == userId).Select(build => _mapper.Map<GetBuildResponse>(build));

        return builds.OrderBy(x => x.LikesCount);
    }

    /// <summary>
    /// returns every build posted by a singel user: 
    ///         
    /// order:
    ///     builds published latest on top
    /// </summary>
    public IEnumerable<GetBuildResponse> GetBuildsByUserIdLatest(int userId)
    {
        var builds = _context.Builds.Where(x => x.UserId == userId).Select(build => _mapper.Map<GetBuildResponse>(build));

        return builds.OrderBy(x => x.Published);
    }

    /// <summary>
    /// returns the private feed: 
    ///     private feed:
    ///         all builds posted by users the current user is following
    ///         
    /// order:
    ///     builds with most likes on top
    /// </summary>
    public IEnumerable<GetBuildResponse> GetAuthenticatedUserFeed(int userId)
    {
        var builds = new List<GetBuildResponse>();

        foreach(var follower in _followerService.GetUserFollowing(userId))
        {
            foreach(var build in GetBuildsByUserId(follower.Id))
            {
                builds.Add(build);
            }
        }

        return builds.OrderBy(x => x.LikesCount);
    }

    /// <summary>
    /// returns the private feed: 
    ///     private feed:
    ///         all builds posted by users the current user is following
    ///         
    /// order:
    ///     builds published latest on top
    /// </summary>
    public IEnumerable<GetBuildResponse> GetAuthenticatedUserFeedLatest(int userId)
    {
        var builds = new List<GetBuildResponse>();

        foreach (var follower in _followerService.GetUserFollowing(userId))
        {
            foreach (var build in GetAuthenticatedUserFeed(follower.Id))
            {
                builds.Add(build);
            }
        }

        return builds.OrderBy(x => x.Published);
    }

    public Build GetBuildById(int buildId)
    {
        var build = _context.Builds.Find(buildId);

        if (build == null)
            throw new KeyNotFoundException("Build not found");

        return build;

    }

}
