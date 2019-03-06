using AutoMapper;
using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using System.Collections.Generic;

namespace PROJ.Logic.UnitOfWork.Repositories
{
    public class UserRepository : Repository<User, UserDTO>
    {
        public UserRepository(ISession session) : base(session)
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

        public UserDTO FindByName(string normalizedUserName)
        {
            var entity = _session.QueryOver<User>()
                .Where(x => x.NormalizedUserName == normalizedUserName)
                .SingleOrDefault();

            return Mapper.Map<UserDTO>(entity);
        }
    }
}
