using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;
using PROJ.Logic.UnitOfWork.Interfaces;
using PROJ.Logic.UnitOfWork.Managers.Interfaces;
using System.Collections.Generic;

namespace PROJ.Logic.UnitOfWork.Managers
{
    public class ProjectManager : ManagerBase<Project, ProjectDTO>, IProjectManager
    {
        public ProjectManager(IUnitOfWork unitOfWork, IAppContext appContext) : base(unitOfWork, appContext)
        { }

        public override int Create(ProjectDTO dto)
        {
            var user = new UserDTO { Id = _appContext.UserId.Value };

            dto.Owner = user;
            dto.Users = new List<UserDTO> { user };

            return base.Create(dto);
        }
    }
}
