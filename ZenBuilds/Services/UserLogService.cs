using AutoMapper;
using ZenBuilds.Entities;
using ZenBuilds.Helpers;
using ZenBuilds.Models.UserLogs;

namespace ZenBuilds.Services;

public interface IUserLogService
{
    void LogAuthentication(LogAuthenticateRequest logAuthenticateRequest);
    IEnumerable<UserLog> GetAllLogs();
    IEnumerable<UserLog> GetAuthenticatedUserLogs(int userId);
    IEnumerable<UserLog> GetSuccessfulAuthenticatedUserLogs(int userId);
    IEnumerable<UserLog> GetUnsuccessfulAuthenticatedUserLogs(int userId);

}

public class UserLogService : IUserLogService
{
    private DataContext _context;
    private readonly IMapper _mapper;

    public UserLogService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // execute in frontend depending on what authentication response is ?
    public void LogAuthentication(LogAuthenticateRequest logAuthenticateRequest)
    {
        var userLog = _mapper.Map<UserLog>(logAuthenticateRequest);

        // apply userId?
        userLog.User = GetUserByUsername(logAuthenticateRequest.Username);
        userLog.UserId = GetUserByUsername(logAuthenticateRequest.Username).Id;
        userLog.Date = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");

        _context.UserLogs.Add(userLog);
        _context.SaveChanges();
    }

    public IEnumerable<UserLog> GetAllLogs()
    {
        return _context.UserLogs.OrderBy(x => x.Date);
    }

    public IEnumerable<UserLog> GetAuthenticatedUserLogs(int userId)
    {
        return _context.UserLogs.Where(x => x.UserId == userId);
    }

    public IEnumerable<UserLog> GetSuccessfulAuthenticatedUserLogs(int userId)
    {
        return _context.UserLogs.Where(x => x.UserId == userId && x.AuthSuccessful == true);
    }

    public IEnumerable<UserLog> GetUnsuccessfulAuthenticatedUserLogs(int userId)
    {
        return _context.UserLogs.Where(x => x.UserId == userId && x.AuthSuccessful == false);
    }

    // helper method

    public User GetUserByUsername(string username)
    {
        var user = _context.Users.SingleOrDefault(x => x.Username == username);

        if (user == null)
            throw new KeyNotFoundException("User not found");    

        return user;
    }

}
