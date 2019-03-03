using System.Collections.Generic;
using AutoMapper;
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

        public IList<UserClaimDTO> GetByUserId(int userId)
        {
            User userAlias = null;
            var entities = _session.QueryOver<UserClaim>()
                .JoinAlias(x => x.User, () => userAlias)
                .Where(() => userAlias.Id == userId)
                .List();

            return Mapper.Map<IList<UserClaimDTO>>(entities);
        }

        public IList<UserClaimDTO> GetSpecificClaimByUserId(int userId, string claimType, string claimValue)
        {
            User userAlias = null;
            var entities = _session.QueryOver<UserClaim>()
                .JoinAlias(x => x.User, () => userAlias)
                .Where(x => userAlias.Id == userId && x.ClaimType == claimType && x.ClaimValue == claimValue)
                .List();

            return Mapper.Map<IList<UserClaimDTO>>(entities);
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            InTransaction(() =>
            {
                _session.Delete(id);
            });
        }
    }
}
