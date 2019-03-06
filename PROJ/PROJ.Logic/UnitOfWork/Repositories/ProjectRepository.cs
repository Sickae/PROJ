using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;

namespace PROJ.Logic.UnitOfWork.Repositories
{
    public class ProjectRepository : Repository<Project, ProjectDTO>
    {
        public ProjectRepository(ISession session) : base(session)
        { }
    }
}
