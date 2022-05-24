using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    GetBuildResponse GetBuildResponseById(int buildId);
    Build GetBuildById(int buildId);
}

public class BuildService : IBuildService
{
    private DataContext _context;
    private readonly IMapper _mapper;
    private IBaseService _baseService;

    public BuildService(DataContext context, IMapper mapper, IBaseService baseService)
    {
        _context = context;
        _mapper = mapper;
        _baseService = baseService;
    }

    public void CreateBuild(int userId, CreateBuildRequest createBuildRequest)
    {
        createBuildRequest.Content = StringManagement.WhitespaceRemoval(createBuildRequest.Content);

        var build = _mapper.Map<Build>(createBuildRequest);

        build.UserId = userId;
        build.Published = DateTime.Now.ToString("yyyy-MM-dd");

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

    public IEnumerable<GetBuildResponse> GetAllBuilds()
    {
        var builds = _context.Builds.Include(x => x.User).Select(build => _mapper.Map<GetBuildResponse>(build)).ToList();

        return builds.OrderByDescending(x => x.LikesCount);
    }

    public IEnumerable<GetBuildResponse> GetAllBuildsLatest()
    {
        var builds = _context.Builds.Select(build => _mapper.Map<GetBuildResponse>(build));

        return builds.OrderByDescending(x => x.Published);
    }

    public IEnumerable<GetBuildResponse> GetBuildsByUserId(int userId)
    {
        var builds = _context.Builds
            .Where(x => x.UserId == userId)
            .Include(x => x.User)
            .Select(build => _mapper.Map<GetBuildResponse>(build)).ToList();

        return builds.OrderByDescending(x => x.LikesCount);
    }

    public IEnumerable<GetBuildResponse> GetBuildsByUserIdLatest(int userId)
    {
        var builds = _context.Builds.Where(x => x.UserId == userId).Select(build => _mapper.Map<GetBuildResponse>(build));

        return builds.OrderByDescending(x => x.Published);
    }

    // change foreach loops to automapper or lambda??
    public IEnumerable<GetBuildResponse> GetAuthenticatedUserFeed(int userId)
    {
        var builds = new List<GetBuildResponse>();

        foreach (var follower in _context.Followers.Where(x => x.User_UserId == userId))
        {
            foreach (var build in GetBuildsByUserId(follower.Follower_UserId))
            {
                builds.Add(build);
            }
        }

        return builds.OrderByDescending(x => x.LikesCount);
    }

    public IEnumerable<GetBuildResponse> GetAuthenticatedUserFeedLatest(int userId)
    {
        var builds = new List<GetBuildResponse>();

        foreach (var follower in _context.Followers.Where(x => x.User_UserId == userId))
        {
            foreach (var build in GetAuthenticatedUserFeed(follower.Follower_UserId))
            {
                builds.Add(build);
            }
        }

        return builds.OrderByDescending(x => x.Published);
    }

    public GetBuildResponse GetBuildResponseById(int buildId)
    {
        

        var build = _context.Builds.Include(x => x.User).FirstOrDefault(x => x.Id == buildId);
            

            //.Select(build => _mapper.Map<GetBuildResponse>(build));

        var buildResponse = _mapper.Map<GetBuildResponse>(build);

        return buildResponse;

    }


    // helper method
    public Build GetBuildById(int buildId)
    {
        var build = _context.Builds.Find(buildId);

        if (build == null)
            throw new KeyNotFoundException("Build not found");

        return build;

    }

}
