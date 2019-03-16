using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;
using System.Linq;

namespace PROJ.Logic.UnitOfWork.Repositories
{
    public class TaskGroupRepository : Repository<TaskGroup, TaskGroupDTO>
    {
        public TaskGroupRepository(ISession session, IAppContext appContext) : base(session, appContext)
        { }

        public override TaskGroupDTO Get(int id)
        {
            return _appContext.Projects.SelectMany(x => x.TaskGroups).Any(x => x.Id == id)
                ? base.Get(id)
                : null;
        }
    }
}
