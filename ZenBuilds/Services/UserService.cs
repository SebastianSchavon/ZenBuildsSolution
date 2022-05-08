namespace ZenBuilds.Services;

using AutoMapper;
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
    IEnumerable<User> GetAllUsers();
    User GetUserById(int id);
}

public class UserService : IUserService
{
    private DataContext _context;
    private IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    public UserService(DataContext context, IJwtUtils jwtUtils, IMapper mapper)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
    }

    /// <summary>
    /// control:
    ///     if the username is currently in database
    ///         bcrypt:
    ///             verifies given password with stored password
    ///             
    /// token:
    ///     generate and return a token which can be used as valid authentication for 3 days
    /// </summary>
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

    /// <summary>
    /// control:
    ///     if the username or email is already in database
    ///     
    /// bcrypt:
    ///     hash password before adding new user to the database
    /// </summary>
    public void Register(RegisterRequest request)
    {
        if (_context.Users.Any(x => x.Username == request.Username))
            throw new Exception("Username already taken");

        if (_context.Users.Any(x => x.Email == request.Email))
            throw new Exception("Email already taken");

        var user = _mapper.Map<User>(request);

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        _context.Users.Add(user);
        _context.SaveChanges();
    }

    /// <summary>
    /// if password is being updated: 
    ///     the old password needs to be verified
    ///     
    ///         if the old password is verified:
    ///             hash and store new password to user
    ///     
    /// if username or email is being changed: 
    ///     check if new username or email is already taken
    /// </summary>
    public void Update(int userId, UpdateRequest request)
    {
        var user = GetUserById(userId);

        if (!string.IsNullOrEmpty(request.NewPassword))
        {
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.OldPassword, user.PasswordHash))
                throw new Exception("Old password incorrect");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        }

        if (_context.Users.Any(x => x.Username == request.Username))
            throw new Exception("Username already taken");

        if (_context.Users.Any(x => x.Email == request.Email))
            throw new Exception("Email already taken");

        _mapper.Map(request, user);

        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = GetUserById(id);
        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _context.Users;
    }

    // helper method
    public User GetUserById(int id)
    {
        return _context.Users.Find(id);
    }


}
