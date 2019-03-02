using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using System.Collections.Generic;

namespace PROJ.Logic.Managers.Interfaces
{
    public interface IUserManager : IManagerBase<User, UserDTO>
    {
        IList<User> GetByClaim(string claimType, string claimValue);
    }
}
