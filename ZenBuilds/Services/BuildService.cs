using AutoMapper;
using ZenBuilds.Entities;
using ZenBuilds.Helpers;
using ZenBuilds.Models.Builds;

namespace ZenBuilds.Services;

/// <summary>
/// builds use composite key as primary key
///     referenced with the id of the user who created the build, followed by the database auto incremented id
/// </summary>
public interface IBuildService
{
    void CreateBuild(CreateBuildRequest createBuildRequest);
    void DeleteBuild(int userId, int id);
    void LikeBuild(int userId, int id);
    void RemoveLike(int userId, int id);

    IEnumerable<Build> GetAllBuilds();
    IEnumerable<Build> GetAllBuildsLatest();
    IEnumerable<Build> GetFollowingBuilds(List<Follower> following);
    IEnumerable<Build> GetFollowingBuildsLatest(List<Follower> following);
    IEnumerable<Build> GetBuildsByUserId(int userId);

    Build GetPostById(int userId, int id);
}

public class BuildService : IBuildService
{
    private DataContext _context;
    private readonly IMapper _mapper;

    public BuildService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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

    public IEnumerable<Build> GetAllBuilds()
    {
        return _context.Builds.OrderBy(x => x.Likes);
    }

    public IEnumerable<Build> GetAllBuildsLatest()
    {
        return _context.Builds.OrderBy(x => x.Published);
    }

    public IEnumerable<Build> GetBuildsByUserId(int userId)
    {
        return _context.Builds.Where(x => x.UserId == userId).OrderBy(x => x.Likes);
    }

    public IEnumerable<Build> GetBuildsByUserIdLatest(int userId)
    {
        return _context.Builds.Where(x => x.UserId == userId).OrderBy(x => x.Published);
    }

    public IEnumerable<Build> GetFollowingBuilds(List<Follower> following)
    {
        var builds = new List<Build>();

        foreach(var follower in following)
        {
            foreach(var build in GetBuildsByUserId(follower.Follower_UserId))
            {
                builds.Add(build);
            }
        }

        return builds;
    }

    public void LikeBuild(int userId, int id)
    {
        GetPostById(userId, id).Likes++;
        _context.SaveChanges();
    }

    public void RemoveLike(int userId, int id)
    {
        GetPostById(userId, id).Likes--;
        _context.SaveChanges();
    }

    public Build GetPostById(int userId, int id)
    {
        var build = _context.Builds.Find(id, userId);

        if (build == null)
            throw new KeyNotFoundException("Build not found");

        return build;

    }
}
