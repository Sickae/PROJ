using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using System.Collections.Generic;

namespace PROJ.Logic.Managers.Interfaces
{
    public interface IUserClaimManager : IManagerBase<UserClaim, UserClaimDTO>, IDeletableManager
    {
        IList<UserClaimDTO> GetByUserId(int userId);
        IList<UserClaimDTO> GetSpecificClaimByUserId(int userId, string claimType, string claimValue);
    }
}
