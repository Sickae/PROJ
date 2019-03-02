using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Managers.Interfaces;

namespace PROJ.Logic.Managers
{
    public class UserClaimManager : ManagerBase<UserClaim, UserClaimDTO>, IUserClaimManager
    {
        public UserClaimManager(ISession session) : base(session)
        { }
    }
}
