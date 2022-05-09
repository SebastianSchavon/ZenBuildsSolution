using AutoMapper;
using ZenBuilds.Entities;
using ZenBuilds.Helpers;
using ZenBuilds.Models.Builds;

namespace ZenBuilds.Services;

/// <summary>
/// builds use composite key as primary key
///     referenced with the id of the user who created the build, followed by the database auto incremented id
///     
/// get builds come with two options:
///     get the builds in order of most likes (will be app standard)
///     get the builds in order of published date
/// </summary>
public interface IBuildService
{
    void CreateBuild(CreateBuildRequest createBuildRequest);
    void DeleteBuild(int userId, int id);
    void LikeBuild(int userId, int id);
    void RemoveLike(int userId, int id);

    IEnumerable<Build> GetAllBuilds();
    IEnumerable<Build> GetAllBuildsLatest();
    IEnumerable<Build> GetBuildsByUserId(int userId);
    IEnumerable<Build> GetBuildsByUserIdLatest(int userId);
    IEnumerable<Build> GetFollowingBuilds(int userId);
    IEnumerable<Build> GetFollowingBuildsLatest(int userId);

    Build GetPostById(int userId, int id);
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

    public void CreateBuild(CreateBuildRequest createBuildRequest)
    {
        var build = _mapper.Map<Build>(createBuildRequest);

        build.Published = DateTime.Now;
        build.Likes = 0;

        _context.Builds.Add(build);
        _context.SaveChanges();
    }

    public void DeleteBuild(int userId, int id)
    {
        _context.Builds.Remove(GetPostById(userId, id));
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
    public IEnumerable<Build> GetAllBuilds()
    {
        return _context.Builds.OrderBy(x => x.Likes);
    }

    /// <summary>
    /// returns the global feed: 
    ///     global feed:
    ///         all builds by every user
    ///         
    /// order:
    ///     builds published latest on top
    /// </summary>
    public IEnumerable<Build> GetAllBuildsLatest()
    {
        return _context.Builds.OrderBy(x => x.Published);
    }

    /// <summary>
    /// returns every build posted by a singel user: 
    ///         
    /// order:
    ///     builds with most likes on top
    /// </summary>
    public IEnumerable<Build> GetBuildsByUserId(int userId)
    {
        return _context.Builds.Where(x => x.UserId == userId).OrderBy(x => x.Likes);
    }

    /// <summary>
    /// returns every build posted by a singel user: 
    ///         
    /// order:
    ///     builds published latest on top
    /// </summary>
    public IEnumerable<Build> GetBuildsByUserIdLatest(int userId)
    {
        return _context.Builds.Where(x => x.UserId == userId).OrderBy(x => x.Published);
    }

    /// <summary>
    /// returns the private feed: 
    ///     private feed:
    ///         all builds posted by users the current user is following
    ///         
    /// order:
    ///     builds with most likes on top
    /// </summary>
    public IEnumerable<Build> GetFollowingBuilds(int userId)
    {
        var builds = new List<Build>();

        foreach(var follower in _followerService.GetUserFollowing(userId))
        {
            foreach(var build in GetBuildsByUserId(follower.Follower_UserId))
            {
                builds.Add(build);
            }
        }

        return builds.OrderBy(x => x.Likes);
    }

    /// <summary>
    /// returns the private feed: 
    ///     private feed:
    ///         all builds posted by users the current user is following
    ///         
    /// order:
    ///     builds published latest on top
    /// </summary>
    public IEnumerable<Build> GetFollowingBuildsLatest(int userId)
    {
        var builds = new List<Build>();

        foreach (var follower in _followerService.GetUserFollowing(userId))
        {
            foreach (var build in GetBuildsByUserId(follower.Follower_UserId))
            {
                builds.Add(build);
            }
        }

        return builds.OrderBy(x => x.Published);
    }

    /// <summary>
    /// like build
    /// </summary>
    public void LikeBuild(int userId, int id)
    {
        GetPostById(userId, id).Likes++;
        _context.SaveChanges();

        _userService.UpdateZenPoints(userId);
    }

    public void RemoveLike(int userId, int id)
    {
        GetPostById(userId, id).Likes--;
        _context.SaveChanges();

        _userService.UpdateZenPoints(userId);
    }

    public Build GetPostById(int userId, int id)
    {
        var build = _context.Builds.Find(id, userId);

        if (build == null)
            throw new KeyNotFoundException("Build not found");

        return build;

    }


}
