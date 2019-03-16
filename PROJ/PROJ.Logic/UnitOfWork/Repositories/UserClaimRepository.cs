using AutoMapper;
using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;
using System.Collections.Generic;

namespace PROJ.Logic.UnitOfWork.Repositories
{
    public class UserClaimRepository : Repository<UserClaim, UserClaimDTO>
    {
        public UserClaimRepository(ISession session, IAppContext appContext) : base(session, appContext)
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
    }
}
