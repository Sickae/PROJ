using AutoMapper;
using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PROJ.Logic.UnitOfWork.Repositories
{
    public class TeamRepository : Repository<Team, TeamDTO>
    {
        public TeamRepository(ISession session, IAppContext appContext) : base(session, appContext)
        { }

        public override TeamDTO Get(int id)
        {
            return _appContext.Teams.Any(x => x.Id == id)
                ? base.Get(id)
                : null;
        }

        public IList<TeamDTO> GetTeamsForCurrentUser()
        {
            var entities = _session.Load<User>(_appContext.UserId.Value).Teams;
            return Mapper.Map<IList<TeamDTO>>(entities);
        }
    }
}
