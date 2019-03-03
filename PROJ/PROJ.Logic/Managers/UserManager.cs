using AutoMapper;
using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PROJ.Logic.Managers
{
    public class UserManager : ManagerBase<User, UserDTO>, IUserManager
    {
        public UserManager(ISession session) : base(session)
        { }

        public IList<UserDTO> GetByClaim(string claimType, string claimValue)
        {   
            UserClaim userClaimAlias = null;
            var entities = _session.QueryOver<User>()
                .JoinAlias(x => x.UserClaims, () => userClaimAlias)
                .Where(() => userClaimAlias.ClaimType == claimType && userClaimAlias.ClaimValue == claimValue)
                .List();

            return Mapper.Map<IList<UserDTO>>(entities);
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            InTransaction(() =>
            {
                _session.Delete(entity);
            });
        }

        public UserDTO FindByName(string normalizedUserName)
        {
            var entity = _session.QueryOver<User>().Where(x => x.NormalizedUserName == normalizedUserName).List().SingleOrDefault();

            return Mapper.Map<UserDTO>(entity);
        }
    }
}
