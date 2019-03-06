using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;

namespace PROJ.Logic.UnitOfWork
{
    public class ChecklistManager : UnitOfWork<Checklist, ChecklistDTO>
    {
        public ChecklistManager(ISession session) : base(session)
        { }
    }
}
