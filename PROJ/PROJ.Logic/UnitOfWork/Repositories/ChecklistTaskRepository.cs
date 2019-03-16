using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;

namespace PROJ.Logic.UnitOfWork.Repositories
{
    public class ChecklistTaskRepository : Repository<ChecklistTask, ChecklistTaskDTO>
    {
        public ChecklistTaskRepository(ISession session, IAppContext appContext) : base(session, appContext)
        { }
    }
}
