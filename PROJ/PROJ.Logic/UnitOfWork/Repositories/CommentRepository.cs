using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;

namespace PROJ.Logic.UnitOfWork.Repositories
{
    public class CommentRepository : Repository<Comment, CommentDTO>
    {
        public CommentRepository(ISession session) : base(session)
        { }
    }
}
