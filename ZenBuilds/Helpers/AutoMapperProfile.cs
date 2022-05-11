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

        CreateMap<User, GetUserResponse>();

        CreateMap<RegisterRequest, User>();
        CreateMap<AuthenticateRequest, User>();
        CreateMap<UpdateRequest, User>();

        CreateMap<Build, GetBuildResponse>();

        CreateMap<Follower, GetFollowerResponse>()
            .ForMember(dest => dest.FollowDate, opt => opt.MapFrom(src => src.FollowDate))
            .ForAllMembers(opt => opt.MapFrom(src => src.User_User));

        CreateMap<Follower, GetFollowerResponse>()
            .ForMember(dest => dest.FollowDate, opt => opt.MapFrom(src => src.FollowDate))
            .ForAllMembers(opt => opt.MapFrom(src => src.Follower_User));

        ////CreateMap<Follower, GetFollowerResponse>()
        ////    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Follower_User.Id))
        ////    .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Follower_User.Username))
        ////    .ForMember(dest => dest.ZenPoints, opt => opt.MapFrom(src => src.Follower_User.ZenPoints))
        ////    .ForMember(dest => dest.FollowDate, opt => opt.MapFrom(src => src.FollowDate));



        //// reverse this?
        //CreateMap<Follower, GetFollowerResponse>()
        //    .ForMember(f => f.FollowDate, u => u.MapFrom(s => s.FollowDate))
        //    .ForMember(f => f.Id, u => u.MapFrom(s => s.Follower_User.Id))
        //    .ForMember(f => f.Username, u => u.MapFrom(s => s.Follower_User.Username))
        //    .ForMember(f => f.ZenPoints, u => u.MapFrom(s => s.Follower_User.ZenPoints))
        //    .ForMember(f => f.Description, u => u.MapFrom(s => s.Follower_User.Description));

        //CreateMap<Follower, GetFollowerResponse>()
        //    .ForMember(f => f.FollowDate, u => u.MapFrom(s => s.FollowDate))
        //    .ForMember(f => f.Id, u => u.MapFrom(s => s.User_User.Id))
        //    .ForMember(f => f.Username, u => u.MapFrom(s => s.User_User.Username))
        //    .ForMember(f => f.ZenPoints, u => u.MapFrom(s => s.User_User.ZenPoints))
        //    .ForMember(f => f.Description, u => u.MapFrom(s => s.User_User.Description));




        CreateMap<FollowCompositeKey, Follower>();

        CreateMap<CreateBuildRequest, Build>();

    }

}
