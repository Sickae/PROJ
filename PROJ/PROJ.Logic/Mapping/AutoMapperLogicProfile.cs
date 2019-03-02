using AutoMapper;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Identity;

namespace PROJ.Logic.Mapping
{
    public class AutoMapperLogicProfile : Profile
    {
        public AutoMapperLogicProfile()
        {
            CreateMap<User, AppIdentityUser>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<AppIdentityUser, UserDTO>().ReverseMap();
            CreateMap<UserClaim, AppIdentityUserClaim>().ReverseMap();
            CreateMap<UserClaim, UserClaimDTO>();
        }
    }
}
