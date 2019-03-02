using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PROJ.Logic.Managers
{
    public class UserManager : ManagerBase<User, UserDTO>, IUserManager
    {
        public UserManager(ISession session) : base(session)
        { }

        public IList<User> GetByClaim(string claimType, string claimValue)
        {   
            UserClaim userClaimAlias = null;
            return _session.QueryOver<User>()
                .JoinAlias(x => x.UserClaims, () => userClaimAlias)
                .Where(() => userClaimAlias.ClaimType == claimType && userClaimAlias.ClaimValue == claimValue)
                .List();
        }
    }
}
