namespace ZenBuilds.Helpers;

using AutoMapper;
using ZenBuilds.Entities;
using ZenBuilds.Models.Builds;
using ZenBuilds.Models.Followers;
using ZenBuilds.Models.Likes;
using ZenBuilds.Models.UserLogs;
using ZenBuilds.Models.Users;

/// <summary>
/// use to pass values between objects with common props
/// </summary>
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // build
        CreateMap<CreateBuildRequest, Build>();
        CreateMap<Build, GetBuildResponse>();

        // follower
        CreateMap<FollowRequest, Follower>();

        // like
        CreateMap<LikeRequest, Like>();
        CreateMap<User, GetBuildLikeResponse>();
        CreateMap<Build, GetUserLikeResponse>();

        // userLog
        CreateMap<LogAuthenticateRequest, UserLog>();

        // user
        CreateMap<User, AuthenticateResponse>();
        CreateMap<RegisterRequest, User>();
        CreateMap<UpdateRequest, User>();
        CreateMap<User, GetUserResponse>();

        // specify object mappings
        

        

        

        
        CreateMap<AuthenticateRequest, User>();
       
        //CreateMap<LikeCompositeKey, Like>()
        //    .ForMember(dest => dest.BuildId, opt => opt.MapFrom(src => src.Build_BuildId))
        //    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
            
            

        CreateMap<Follower, GetFollowerResponse>()
            .ForMember(dest => dest.FollowDate, opt => opt.MapFrom(src => src.FollowDate))
            .ForAllMembers(opt => opt.MapFrom(src => src.User_User));

        CreateMap<Follower, GetFollowerResponse>()
            .ForMember(dest => dest.FollowDate, opt => opt.MapFrom(src => src.FollowDate))
            .ForAllMembers(opt => opt.MapFrom(src => src.Follower_User));

        

        

    }

}
