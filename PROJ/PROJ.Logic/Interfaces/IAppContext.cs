using PROJ.Logic.DTOs;
using System.Collections.Generic;

namespace PROJ.Logic.Interfaces
{
    public interface IAppContext
    {
        int? UserId { get; }
        int? ActiveTeamId { get; }
        UserDTO CurrentUser { get; }
        IList<ProjectDTO> Projects { get; }
        IList<TeamDTO> Teams { get; }
        IList<TaskDTO> AssignedTasks { get; }
    }
}
