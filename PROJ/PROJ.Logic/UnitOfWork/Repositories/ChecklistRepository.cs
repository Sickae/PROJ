using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;

namespace PROJ.Logic.UnitOfWork.Repositories
{
    public class ChecklistRepository : Repository<Checklist, ChecklistDTO>
    {
        public ChecklistRepository(ISession session, IAppContext appContext) : base(session, appContext)
        { }
    }
}
