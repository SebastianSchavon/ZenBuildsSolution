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
    void AddFollow(FollowRequest followRequest);
    void RemoveFollow(FollowRequest followRequest);

    IEnumerable<GetFollowerResponse> GetUserFollowers(int follower_UserId);
    IEnumerable<GetFollowerResponse> GetUserFollowing(int user_UserId);
    Follower GetFollower(FollowRequest followRequest);

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
    public void AddFollow(FollowRequest followRequest)
    {
        if (followRequest.User_UserId == followRequest.Follower_UserId)
            throw new Exception("Cant follow one self");

        if (!_context.Users.Any(x => x.Id == followRequest.Follower_UserId))
            throw new Exception("The User which you are trying to follow does not exist");

        if (_context.Followers.Any(x => x.User_UserId == followRequest.User_UserId) &&
            _context.Followers.Any(x => x.Follower_UserId == followRequest.Follower_UserId))
            throw new Exception("User is already followed");

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
        var follower = GetFollower(followRequest);

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
        var following = _context.Followers.Where(x => x.Follower_UserId == follower_UserId)
            .Select(follower => new GetFollowerResponse
            {
                Id = follower.User_User.Id,
                Username = follower.User_User.Username,
                Description = follower.User_User.Description,
                ZenPoints = follower.User_User.ZenPoints,
                FollowDate = follower.FollowDate

            });

        return following;

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
            .Select(follower => new GetFollowerResponse
            {
                Id = follower.Follower_User.Id,
                Username = follower.Follower_User.Username,
                Description = follower.Follower_User.Description,
                ZenPoints = follower.Follower_User.ZenPoints,
                FollowDate = follower.FollowDate

            });

        return following.OrderBy(x => x.FollowDate);
    }

    // helper method
    public Follower GetFollower(FollowRequest followRequest)
    {
        var follower = _context.Followers.Find(followRequest.User_UserId, followRequest.Follower_UserId);

        // will the exception be thrown all the way up to the followerController?
        if (follower == null)
            throw new KeyNotFoundException("Follower not found");

        return follower;

    }


}
