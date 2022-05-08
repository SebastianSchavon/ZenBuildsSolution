using AutoMapper;
using ZenBuilds.Entities;
using ZenBuilds.Helpers;
using ZenBuilds.Models.Followers;

namespace ZenBuilds.Services;

public interface IFollowerService
{
    void AddFollow(FollowRequest followRequest);
    void RemoveFollow(FollowRequest followRequest);
    IEnumerable<Follower> GetUserFollowers(int follower_UserId);
    IEnumerable<Follower> GetUserFollowing(int user_UserId);
    Follower GetFollower(int user_UserId, int follower_UserId);
    
}

public class FollowerService : IFollowerService
{
    private DataContext _context;
    private readonly IMapper _mapper;

    public FollowerService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }

    /// <summary>
    /// user follows another user by creating a new follower
    ///     does not need to be accepted by the user being followed
    /// </summary>
    public void AddFollow(FollowRequest followRequest)
    {
        if (!_context.Users.Any(x => x.Id == followRequest.Follower_UserId))
            throw new Exception("The User which you are trying to follow does not exist");

        var follower = _mapper.Map<Follower>(followRequest);

        follower.FollowDate = DateTime.Now;

        _context.Followers.Add(follower);
        _context.SaveChanges();
    }

    /// <summary>
    /// user which is the one following another user:
    ///     removes the follow
    /// </summary>
    public void RemoveFollow(FollowRequest followRequest)
    {
        var follower = GetFollower(followRequest.User_UserId, followRequest.Follower_UserId);

        _context.Followers.Remove(follower);
        _context.SaveChanges();
    }
    
    /// <summary>
    /// get a users followers
    ///     by returning every follower where the Follower_UserId value equals the given users Id
    /// </summary>
    public IEnumerable<Follower> GetUserFollowers(int follower_UserId)
    {
        return _context.Followers.Where(x => x.Follower_UserId == follower_UserId);
    }

    /// <summary>
    /// get the users following list
    ///     by returning every follower where the User_UserId value equals the given users Id
    /// </summary>
    public IEnumerable<Follower> GetUserFollowing(int user_UserId)
    {
        return _context.Followers.Where(x => x.User_UserId == user_UserId);
    }

    public Follower GetFollower(int user_UserId, int follower_UserId)
    {
        var follower = _context.Followers.Find(user_UserId, follower_UserId);
        
        // will the exception be thrown all the way up to the followerController?
        if(follower == null)
            throw new Exception("Cant find following");

        return follower;
        
    }
}
