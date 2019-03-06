using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;

namespace PROJ.Logic.UnitOfWork
{
    public class ChecklistTaskManager : UnitOfWork<ChecklistTask, ChecklistTaskDTO>
    {
        public ChecklistTaskManager(ISession session) : base(session)
        { }
    }
}
