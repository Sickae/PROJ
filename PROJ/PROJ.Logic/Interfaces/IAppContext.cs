using PROJ.Logic.DTOs;
using System.Collections.Generic;

namespace PROJ.Logic.Interfaces
{
    public interface IAppContext
    {
        int? UserId { get; }
        IList<ProjectDTO> Projects { get; }
    }
}
