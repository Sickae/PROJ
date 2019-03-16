using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;

namespace PROJ.Logic.UnitOfWork.Repositories
{
    public class TaskRepository : Repository<Task, TaskDTO>
    {
        public TaskRepository(ISession session, IAppContext appContext) : base(session, appContext)
        { }
    }
}
