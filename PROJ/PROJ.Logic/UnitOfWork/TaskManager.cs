using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;

namespace PROJ.Logic.UnitOfWork
{
    public class TaskManager : UnitOfWork<Task, TaskDTO>
    {
        public TaskManager(ISession session) : base(session)
        { }
    }
}
