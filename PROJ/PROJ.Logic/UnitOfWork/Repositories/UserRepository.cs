using AutoMapper;
using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;
using System.Collections.Generic;

namespace PROJ.Logic.UnitOfWork.Repositories
{
    public class UserRepository : Repository<User, UserDTO>
    {
        public UserRepository(ISession session, IAppContext appContext) : base(session, appContext)
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

        public UserDTO FindByName(string username)
        {
            var normalizedUserName = username.ToUpper();
            var entity = _session.QueryOver<User>()
                .Where(x => x.NormalizedUserName == normalizedUserName)
                .SingleOrDefault();

            return Mapper.Map<UserDTO>(entity);
        }

        public UserDTO GetCurrentUser()
        {
            var entity = _session.Load<User>(_appContext.UserId.HasValue ? _appContext.UserId.Value : 0);

            return entity != null
                ? Mapper.Map<UserDTO>(entity)
                : null;
        }
    }
}
