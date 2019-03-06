using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;

namespace PROJ.Logic.UnitOfWork
{
    public class UserManager : UnitOfWork<User, UserDTO>
    {
        public UserManager(ISession session) : base(session)
        { }
    }
}
