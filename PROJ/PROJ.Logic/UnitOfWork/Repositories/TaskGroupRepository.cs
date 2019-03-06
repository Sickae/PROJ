using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;

namespace PROJ.Logic.UnitOfWork.Repositories
{
    public class TaskGroupRepository : Repository<TaskGroup, TaskGroupDTO>
    {
        public TaskGroupRepository(ISession session) : base(session)
        { }
    }
}
