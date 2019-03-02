using Microsoft.AspNetCore.Identity;
using PROJ.DataAccess.Entities;
using PROJ.Logic.Authorization;
using System.Security.Claims;

namespace PROJ.Logic.Identity
{
    public class AppIdentityUserClaim : IdentityUserClaim<int>
    {
        public AppIdentityUserClaim()
        { }

        public AppIdentityUserClaim(User user, RoleConstants.Role role)
        {
            UserId = user.Id;
            ClaimType = ClaimTypes.Role;
            ClaimValue = role.ToString();
        }
    }
}
