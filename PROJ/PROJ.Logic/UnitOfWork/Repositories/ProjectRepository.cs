using AutoMapper;
using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PROJ.Logic.UnitOfWork.Repositories
{
    public class ProjectRepository : Repository<Project, ProjectDTO>
    {
        public ProjectRepository(ISession session, IAppContext appContext) : base(session, appContext)
        { }

        public override ProjectDTO Get(int id)
        {
            return _appContext.Projects.Any(x => x.Id == id)
                ? base.Get(id)
                : null;
        }

        public IList<ProjectDTO> GetProjectsForCurrentUser()
        {
            var entities = _session.Load<User>(_appContext.UserId.Value).ActiveTeam?.Projects ?? new List<Project>();
            return Mapper.Map<IList<ProjectDTO>>(entities);
        }
    }
}
