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
        CreateMap<User, GetBuildUserResponse>();
        CreateMap<Build, GetBuildResponse>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(source => source.User));


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
        // ignore null values passed in update request
        //CreateMap<UpdateRequest, User>()
        //    .ForAllMembers(x => x.Condition(
        //        (src, dest, prop) =>
        //        {
        //            // ignore null & empty string properties
        //            if (prop == null) return false;
        //            if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

        //            return true;
        //        }
        //    ));

        CreateMap<Like, GetBuildLikeResponse>();
        CreateMap<Follower, GetFollowerResponse>();
        CreateMap<Build, GetBuildLikeResponse>();
        CreateMap<User, GetAuthenticatedUserResponse>();
        CreateMap<User, GetUserResponse>()
            .ForMember(dest => dest.Builds, opt => opt.MapFrom(source => source.Builds.ToList()));

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
