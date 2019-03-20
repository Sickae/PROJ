using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.UnitOfWork.Interfaces;

namespace PROJ.Logic.UnitOfWork.Managers.Interfaces
{
    public interface ITeamManager : IManager<Team, TeamDTO>
    { }
}
