using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;

namespace PROJ.Logic.UnitOfWork
{
    public class CommentManager : UnitOfWork<Comment, CommentDTO>
    {
        public CommentManager(ISession session) : base(session)
        { }
    }
}
