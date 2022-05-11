using AutoMapper;
using ZenBuilds.Entities;
using ZenBuilds.Helpers;
using ZenBuilds.Models.Followers;

namespace ZenBuilds.Services;

/// <summary>
/// follower use composite key as primary key
///     referenced with the id of the user who is following, followed by the id of the user who is followed
/// </summary>
public interface IFollowerService
{
    void AddFollow(FollowCompositeKey followCompositeKey);
    void RemoveFollow(FollowCompositeKey followCompositeKey);

    IEnumerable<GetFollowerResponse> GetUserFollowers(int follower_UserId);
    IEnumerable<GetFollowerResponse> GetUserFollowing(int user_UserId);
    Follower GetFollower(FollowCompositeKey followCompositeKey);
    
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
    ///     does not need to be accepted by the user receiving the follow
    /// </summary>
    public void AddFollow(FollowCompositeKey followCompositeKey)
    {
        if (!_context.Users.Any(x => x.Id == followCompositeKey.Follower_UserId))
            throw new Exception("The User which you are trying to follow does not exist");

        if (_context.Followers.Any(x => x.User_UserId == followCompositeKey.User_UserId))
            throw new Exception("User is already followed");

        var follower = _mapper.Map<Follower>(followCompositeKey);

        follower.FollowDate = DateTime.Now;

        _context.Followers.Add(follower);
        _context.SaveChanges();
    }

    /// <summary>
    /// user which is the one following another user:
    ///     removes the follow
    /// </summary>
    public void RemoveFollow(FollowCompositeKey followCompositeKey)
    {
        var follower = GetFollower(followCompositeKey);

        _context.Followers.Remove(follower);
        _context.SaveChanges();
    }

    /// <summary>
    /// get a users follower list
    ///     by returning every follower where the Follower_UserId value equals the given users Id
    ///     
    /// represents all the users that follow the user
    /// </summary>
    public IEnumerable<GetFollowerResponse> GetUserFollowers(int follower_UserId)
    {
        var followers = _context.Followers.Where(x => x.Follower_UserId == follower_UserId)
            .Select(x => new GetFollowerResponse
            {
                Follower_UserId = x.User_User.Id,
                Username = x.User_User.Username,
                Description = x.User_User.Description,
                ZenPoints = x.User_User.ZenPoints,
                FollowDate = x.FollowDate

            }).ToList();

        return followers.OrderBy(x => x.FollowDate);

        //var followers = _context.Followers.Where(x => x.Follower_UserId == follower_UserId);
        //return followers;
    }

    /// <summary>
    /// get a users following list 
    ///     by returning every follower where the User_UserId value equals the given users Id
    ///     
    /// represents all the users the user is following
    /// </summary>
    public IEnumerable<GetFollowerResponse> GetUserFollowing(int user_UserId)
    {
        var following = _context.Followers.Where(x => x.User_UserId == user_UserId)
            .Select(x => new GetFollowerResponse
            {
                Follower_UserId = x.User_User.Id,
                Username = x.User_User.Username,
                Description = x.User_User.Description,
                ZenPoints = x.User_User.ZenPoints,
                FollowDate = x.FollowDate

            }).ToList();

        return following.OrderBy(x => x.FollowDate);
    }

    // helper method
    public Follower GetFollower(FollowCompositeKey followCompositeKey)
    {
        var follower = _context.Followers.Find(followCompositeKey.User_UserId, followCompositeKey.Follower_UserId);
        
        // will the exception be thrown all the way up to the followerController?
        if(follower == null)
            throw new KeyNotFoundException("Follower not found");

        return follower;
        
    }


}
