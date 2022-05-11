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
        // specify object mappings
        CreateMap<LogAuthenticateRequest, UserLog>();

        CreateMap<User, AuthenticateResponse>();

        CreateMap<User, GetUserResponse>();

        CreateMap<RegisterRequest, User>();
        CreateMap<AuthenticateRequest, User>();
        CreateMap<UpdateRequest, User>();

        CreateMap<User, GetLikeResponse>();

        CreateMap<Build, GetBuildResponse>();

        CreateMap<Like, LikeRequest>();

        //CreateMap<LikeCompositeKey, Like>()
        //    .ForMember(dest => dest.BuildId, opt => opt.MapFrom(src => src.Build_BuildId))
        //    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
            
            

        CreateMap<Follower, GetFollowerResponse>()
            .ForMember(dest => dest.FollowDate, opt => opt.MapFrom(src => src.FollowDate))
            .ForAllMembers(opt => opt.MapFrom(src => src.User_User));

        CreateMap<Follower, GetFollowerResponse>()
            .ForMember(dest => dest.FollowDate, opt => opt.MapFrom(src => src.FollowDate))
            .ForAllMembers(opt => opt.MapFrom(src => src.Follower_User));

        CreateMap<FollowRequest, Follower >();

        CreateMap<CreateBuildRequest, Build>();

    }

}
