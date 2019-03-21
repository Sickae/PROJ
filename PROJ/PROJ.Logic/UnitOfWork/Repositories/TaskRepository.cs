using AutoMapper;
using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;
using System.Collections.Generic;

namespace PROJ.Logic.UnitOfWork.Repositories
{
    public class TaskRepository : Repository<Task, TaskDTO>
    {
        public TaskRepository(ISession session, IAppContext appContext) : base(session, appContext)
        { }

        public IList<TaskDTO> GetTasksForCurrentUser()
        {
            var entities = _session.Load<User>(_appContext.UserId.Value).AssignedTasks ?? new List<Task>();
            return Mapper.Map<IList<TaskDTO>>(entities);
        }
    }
}
