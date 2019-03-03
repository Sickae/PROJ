using Microsoft.AspNetCore.Identity;
using PROJ.Logic.Authorization;
using System.Security.Claims;

namespace PROJ.Logic.Identity
{
    public class AppIdentityUserClaim : IdentityUserClaim<int>
    {
        public AppIdentityUserClaim()
        { }

        public AppIdentityUserClaim(AppIdentityUser identityUser, RoleConstants.Role role)
        {
            UserId = identityUser.Id;
            ClaimType = ClaimTypes.Role;
            ClaimValue = role.ToString();
        }
    }
}
