using AutoMapper;
using ZenBuilds.Entities;
using ZenBuilds.Helpers;
using ZenBuilds.Models.Builds;

namespace ZenBuilds.Services;

public interface IBuildService
{
    void CreateBuild(CreateBuildRequest createBuildRequest);
    void DeleteBuild(BuildCompositeKey buildCompositeKey);
    void ToggleBuildLike(ToggleLikeRequest toggleLikeRequest);

    IEnumerable<Build> GetAllBuilds();
    IEnumerable<Build> GetAllBuildsLatest();
    IEnumerable<Build> GetBuildsByUserId(int userId);
    IEnumerable<Build> GetBuildsByUserIdLatest(int userId);
    IEnumerable<Build> GetFollowingBuilds(int userId);
    IEnumerable<Build> GetFollowingBuildsLatest(int userId);

    Build GetBuildById(BuildCompositeKey buildCompositeKey);
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

        _context.Builds.Add(build);
        _context.SaveChanges();
    }

    public void DeleteBuild(BuildCompositeKey buildCompositeKey)
    {
        _context.Builds.Remove(GetBuildById(buildCompositeKey));
        _context.SaveChanges();
    }

    /// <summary>
    /// toggle like on build:
    ///     if user exists in liked list of users
    ///         remove user from list
    ///     if user does not exist in liked list of users
    ///         add user to list
    ///         
    /// likescount property equals liked list of users count
    /// 
    /// update zenpoints of user who recieved like
    /// </summary>
    public void ToggleBuildLike(ToggleLikeRequest toggleLikeRequest)
    {
        var currentUser = _userService.GetUserById(toggleLikeRequest.Current_UserId);
        var build = GetBuildById(toggleLikeRequest.BuildId);

        if (!build.Likes.Contains(currentUser))
            build.Likes.Add(currentUser);
        else
            build.Likes.Remove(currentUser);

        build.LikesCount = build.Likes.Count;

        _context.SaveChanges();

        _userService.UpdateZenPoints(toggleLikeRequest.BuildId.UserId);
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

    public Build GetBuildById(BuildCompositeKey buildCompositeKey)
    {
        var build = _context.Builds.Find(buildCompositeKey.UserId, buildCompositeKey.Id);

        if (build == null)
            throw new KeyNotFoundException("Build not found");

        return build;

    }

}
