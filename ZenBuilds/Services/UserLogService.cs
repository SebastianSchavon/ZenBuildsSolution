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
        throw new NotImplementedException();
    }

    public IEnumerable<UserLog> GetAllSuccessfulAuthenticationsByUserId(int userId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UserLog> GetAllFailedAuthenticationsByUserId(int userId)
    {
        throw new NotImplementedException();
    }

}
