using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;

namespace PROJ.Logic.UnitOfWork.Repositories
{
    public class ChecklistTaskRepository : Repository<ChecklistTask, ChecklistTaskDTO>
    {
        public ChecklistTaskRepository(ISession session) : base(session)
        { }
    }
}
