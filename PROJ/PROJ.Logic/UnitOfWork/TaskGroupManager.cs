using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;

namespace PROJ.Logic.UnitOfWork
{
    public class TaskGroupManager : UnitOfWork<TaskGroup, TaskGroupDTO>
    {
        public TaskGroupManager(ISession session) : base(session)
        { }
    }
}
