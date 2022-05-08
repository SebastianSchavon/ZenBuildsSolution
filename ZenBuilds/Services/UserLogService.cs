using AutoMapper;
using ZenBuilds.Entities;
using ZenBuilds.Helpers;
using ZenBuilds.Models.UserLogs;

namespace ZenBuilds.Services;

public interface IUserLogService
{
    void LogAuthentication (LogAuthenticateRequest logAuthenticateRequest);
    IEnumerable<UserLog> GetAllLogs();
    IEnumerable<UserLog> GetAllLogsByUserId(int userId);
    IEnumerable<UserLog> GetAllSuccessfulAuthenticationsByUserId(int userId);
    IEnumerable<UserLog> GetAllFailedAuthenticationsByUserId(int userId);

}

/// <summary>
/// logs and displays all authentication tries when a valid username is being used
/// properties of userId, datetime, ip-adress and if the authentication was succesful or not is beging stored
/// </summary>
public class UserLogService : IUserLogService
{
    private DataContext _context;
    private readonly IMapper _mapper;

    public UserLogService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void LogAuthentication(LogAuthenticateRequest logAuthenticateRequest)
    {
        var userLog = _mapper.Map<UserLog>(logAuthenticateRequest);

        _context.UserLogs.Add(userLog);
        _context.SaveChanges();
    }

    public IEnumerable<UserLog> GetAllLogs()
    {
        return _context.UserLogs;
    }

    public IEnumerable<UserLog> GetAllLogsByUserId(int userId)
    {
        return _context.UserLogs.Where(x => x.UserId == userId);
    }

    public IEnumerable<UserLog> GetAllSuccessfulAuthenticationsByUserId(int userId)
    {
        return _context.UserLogs.Where(x => x.UserId == userId && x.AuthSuccessful == true);
    }

    public IEnumerable<UserLog> GetAllFailedAuthenticationsByUserId(int userId)
    {
        return _context.UserLogs.Where(x => x.UserId == userId && x.AuthSuccessful == false);
    }

}
