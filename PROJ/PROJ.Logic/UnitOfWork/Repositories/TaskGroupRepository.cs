using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;

namespace PROJ.Logic.UnitOfWork.Repositories
{
    public class TaskGroupRepository : Repository<TaskGroup, TaskGroupDTO>
    {
        public TaskGroupRepository(ISession session, IAppContext appContext) : base(session, appContext)
        { }
    }
}
