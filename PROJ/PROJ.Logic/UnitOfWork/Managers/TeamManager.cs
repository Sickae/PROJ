using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;
using PROJ.Logic.UnitOfWork.Interfaces;
using PROJ.Logic.UnitOfWork.Managers.Interfaces;
using System.Collections.Generic;

namespace PROJ.Logic.UnitOfWork.Managers
{
    public class TeamManager : ManagerBase<Team, TeamDTO>, ITeamManager
    {
        public TeamManager(IUnitOfWork unitOfWork, IAppContext appContext) : base(unitOfWork, appContext)
        { }

        public override int Create(TeamDTO dto)
        {
            var user = new UserDTO { Id = _appContext.UserId.Value };

            dto.Owner = user;
            dto.Members = new List<UserDTO> { user };

            return base.Create(dto);
        }

        public override void Delete(TeamDTO dto)
        {
            _unitOfWork.UseTransaction(() =>
            {
                var entity = _session.Load<Team>(dto.Id);
                var currentUser = _session.Load<User>(_appContext.UserId.Value);
                if (currentUser.ActiveTeam.Id == dto.Id)
                {
                    currentUser.ActiveTeam = null;
                    _session.Save(currentUser);
                }

                _session.Delete(entity);
            });
        }
    }
}
