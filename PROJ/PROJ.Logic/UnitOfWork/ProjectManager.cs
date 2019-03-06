using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;

namespace PROJ.Logic.UnitOfWork
{
    public class ProjectManager : UnitOfWork<Project, ProjectDTO>
    {
        public ProjectManager(ISession session) : base(session)
        { }
    }
}
