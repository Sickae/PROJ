using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;

namespace PROJ.Logic.UnitOfWork.Repositories
{
    public class ChecklistRepository : Repository<Checklist, ChecklistDTO>
    {
        public ChecklistRepository(ISession session) : base(session)
        { }
    }
}
