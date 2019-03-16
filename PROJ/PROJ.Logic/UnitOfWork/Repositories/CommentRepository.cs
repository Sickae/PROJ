using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;

namespace PROJ.Logic.UnitOfWork.Repositories
{
    public class CommentRepository : Repository<Comment, CommentDTO>
    {
        public CommentRepository(ISession session, IAppContext appContext) : base(session, appContext)
        { }
    }
}
