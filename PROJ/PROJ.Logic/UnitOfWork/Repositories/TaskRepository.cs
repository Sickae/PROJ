using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;

namespace PROJ.Logic.UnitOfWork.Repositories
{
    public class TaskRepository : Repository<Task, TaskDTO>
    {
        public TaskRepository(ISession session) : base(session)
        { }
    }
}
