namespace ZenBuilds.Helpers;

using AutoMapper;
using ZenBuilds.Entities;
using ZenBuilds.Models.UserLogs;
using ZenBuilds.Models.Users;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<LogAuthenticateRequest, UserLog>();
    }
    
}
