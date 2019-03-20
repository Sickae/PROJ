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
            CreateMap<AppIdentityUser, UserDTO>().ReverseMap();
            CreateMap<UserClaim, AppIdentityUserClaim>().ReverseMap();
            CreateMap<AppIdentityUserClaim, UserClaimDTO>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserClaim, UserClaimDTO>().ReverseMap();
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<TaskGroup, TaskGroupDTO>().ReverseMap();
            CreateMap<Task, TaskDTO>().ReverseMap();
            CreateMap<Checklist, ChecklistDTO>().ReverseMap();
            CreateMap<ChecklistTask, ChecklistTaskDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<Team, TeamDTO>().ReverseMap();
        }
    }
}
