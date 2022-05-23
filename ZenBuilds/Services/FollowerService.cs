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
    bool FollowCheck(FollowRequest followRequest);

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

    public void AddFollow(FollowRequest followRequest)
    {
        if (followRequest.User_UserId == followRequest.Follower_UserId)
            throw new Exception("Cant follow one self");

        if (!_context.Users.Any(x => x.Id == followRequest.Follower_UserId))
            throw new Exception("The User which you are trying to follow does not exist");

        if (_context.Followers.Any(x => x.User_UserId == followRequest.User_UserId && x.Follower_UserId == followRequest.Follower_UserId))
            throw new Exception("User is already followed");

        var follower = _mapper.Map<Follower>(followRequest);

        follower.FollowDate = DateTime.Now.ToString("yyyy-MM-dd");

        _context.Followers.Add(follower);
        _context.SaveChanges();
    }
    public void RemoveFollow(FollowRequest followRequest)
    {
        var follower = GetFollower(followRequest);

        _context.Followers.Remove(follower);
        _context.SaveChanges();
    }

    public bool FollowCheck(FollowRequest followRequest)
    {
        if(_context.Followers.Any(x => x.User_UserId == followRequest.User_UserId && x.Follower_UserId == followRequest.Follower_UserId))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public IEnumerable<GetFollowerResponse> GetUserFollowers(int follower_UserId)
    {
        // this needs a automapper
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

    public IEnumerable<GetFollowerResponse> GetUserFollowing(int user_UserId)
    {
        // this needs a automapper
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

        if (follower == null)
            throw new KeyNotFoundException("Follower not found");

        return follower;

    }

}
