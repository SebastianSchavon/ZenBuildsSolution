namespace ZenBuilds.Helpers;

using AutoMapper;
using ZenBuilds.Entities;
using ZenBuilds.Models.Builds;
using ZenBuilds.Models.Followers;
using ZenBuilds.Models.Likes;
using ZenBuilds.Models.UserLogs;
using ZenBuilds.Models.Users;

/// <summary>
///     Use to copy property values between objects
/// </summary>
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // build
        CreateMap<Build, GetBuildLikeResponse>();
        CreateMap<Build, GetFollowerResponse>();
        CreateMap<Build, GetUserLikeResponse>();
        CreateMap<Build, GetBuildResponse>();
        //CreateMap<Build, GetBuildResponse>()
        //    .ForMember(dest => dest.User, opt => opt.MapFrom(source => source.User));

        CreateMap<CreateBuildRequest, Build>();


        // userlog
        CreateMap<LogAuthenticateRequest, UserLog>();


        // user
        CreateMap<User, AuthenticateResponse>();
        CreateMap<User, GetBuildLikeResponse>();
        CreateMap<User, GetFollowerResponse>();
        CreateMap<User, GetFollowerUserResponse>();
        CreateMap<User, GetAuthenticatedUserResponse>();
        CreateMap<User, GetUserResponse>();
        CreateMap<User, GetBuildUserResponse>();

        CreateMap<RegisterRequest, User>();
        CreateMap<UpdateRequest, User>();
        CreateMap<AuthenticateRequest, User>();


        // follower
        CreateMap<Follower, GetFollowerResponse>();
        CreateMap<Follower, GetFollowingResponse>();
        CreateMap<Follower, GetAuthenticatedUserResponse>();
        CreateMap<GetBuildResponse, Follower>();


        CreateMap<FollowRequest, Follower>();


        // like
        CreateMap<Like, GetBuildLikeResponse>();
        CreateMap<LikeRequest, Like>();
    }

}
