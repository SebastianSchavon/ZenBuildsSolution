namespace ZenBuilds.Helpers;

using AutoMapper;
using ZenBuilds.Entities;
using ZenBuilds.Models.Builds;
using ZenBuilds.Models.Followers;
using ZenBuilds.Models.UserLogs;
using ZenBuilds.Models.Users;

/// <summary>
/// use to pass values between objects with common props
/// </summary>
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // specify object mappings
        CreateMap<LogAuthenticateRequest, UserLog>();
        CreateMap<User, AuthenticateResponse>();
        CreateMap<FollowRequest, Follower>();
        CreateMap<UpdateRequest, User>();
        CreateMap<CreateBuildRequest, Build>();
        
    }
    
}
