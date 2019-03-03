using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using System.Collections.Generic;

namespace PROJ.Logic.Managers.Interfaces
{
    public interface IUserManager : IManagerBase<User, UserDTO>, IDeletableManager
    {
        IList<UserDTO> GetByClaim(string claimType, string claimValue);
        UserDTO FindByName(string normalizedUserName);
    }
}
