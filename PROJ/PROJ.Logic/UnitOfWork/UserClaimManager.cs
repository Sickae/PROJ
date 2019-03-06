using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;

namespace PROJ.Logic.UnitOfWork
{
    public class UserClaimManager : UnitOfWork<UserClaim, UserClaimDTO>
    {
        public UserClaimManager(ISession session) : base(session)
        { }
    }
}
