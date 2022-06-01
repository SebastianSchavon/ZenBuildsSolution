namespace ZenBuilds.Services;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZenBuilds.Authorization;
using ZenBuilds.Entities;
using ZenBuilds.Helpers;
using ZenBuilds.Models.Users;

public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest request);
    void Register(RegisterRequest request);
    void Update(int userId, UpdateRequest request);
    void Delete(int userId);
    IEnumerable<GetUserResponse> GetAllUsers();
    IEnumerable<GetUserResponse> GetTop20Users();
    GetUserResponse GetUserByUsername(string username);
    GetAuthenticatedUserResponse GetAuthenticatedUser(int userId);
    GetUserResponse GetUserByUserId(int userId);
    User GetUserById(int id);
}

public class UserService : IUserService
{
    private DataContext _context;
    private IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;
    IBaseService _baseService;

    public UserService(DataContext context, IJwtUtils jwtUtils, IMapper mapper, IBaseService baseService)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
        _baseService = baseService;
    }
    public AuthenticateResponse Authenticate(AuthenticateRequest request)
    {
        if (!_context.Users.Any(x => x.Username == request.Username))
            throw new Exception("Username incorrect");

        var user = _context.Users.SingleOrDefault(x => x.Username == request.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new Exception("Password incorrect");

        var response = _mapper.Map<AuthenticateResponse>(user);

        response.Token = _jwtUtils.GenerateToken(user);

        return response;
    }

    public void Register(RegisterRequest request)
    {
        if (_context.Users.Any(x => x.Username == request.Username))
            throw new Exception("Username already taken");

        var user = _mapper.Map<User>(request);

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        user.RegisterDate = DateTime.Now.ToString("yyyy-MM-dd");

        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void Update(int userId, UpdateRequest request)
    {
        var user = GetUserById(userId);

        if (!string.IsNullOrEmpty(request.NewPassword))
        {
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.OldPassword, user.PasswordHash))
                throw new Exception("Old password incorrect");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        }

        if (_context.Users.Any(x => x.Username == request.Username) && user.Username != request.Username)
            throw new Exception("Username already taken");

        _mapper.Map(request, user);

        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void Delete(int userId)
    {
        var user = GetUserById(userId);
        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    public IEnumerable<GetUserResponse> GetAllUsers()
    {
        var users = _context.Users.Select(user => _mapper.Map<GetUserResponse>(user)).ToList();

        return users.OrderBy(x => x.ZenPoints);
    }

    public IEnumerable<GetUserResponse> GetTop20Users()
    {
        var users = _context.Users.Include(x => x.Builds).Select(user => _mapper.Map<GetUserResponse>(user)).Take(20).ToList();

        return users.OrderByDescending(x => x.ZenPoints);
    }

    public GetUserResponse GetUserByUsername(string username)
    {
        var user = _context.Users.SingleOrDefault(x => x.Username == username);

        if (user == null)
            throw new KeyNotFoundException("User not found");

        var userResponse = _mapper.Map<GetUserResponse>(user);

        return userResponse;
    }

    public GetUserResponse GetUserByUserId(int userId)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == userId);

        var userResponse = _mapper.Map<GetUserResponse>(user);

        return userResponse;
    }

    public GetAuthenticatedUserResponse GetAuthenticatedUser(int userId)
    {
        var user = _context.Users.SingleOrDefault(x => x.Id == userId);

        var user2 = _mapper.Map<GetAuthenticatedUserResponse>(user);

        return user2;
    }

    // helper methods
    public User GetUserById(int id)
    {
        var user = _context.Users.SingleOrDefault(x => x.Id == id);
        if (user == null)
            throw new KeyNotFoundException("User not found");
        return user;
    }


}
