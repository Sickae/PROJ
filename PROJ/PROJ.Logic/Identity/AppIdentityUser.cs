using Microsoft.AspNetCore.Identity;
using PROJ.DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace PROJ.Logic.Identity
{
    public class AppIdentityUser : IdentityUser<int>
    {
        public DateTime? LastLoginDate { get; set; }

        public IList<AppIdentityUserClaim> UserClaims { get; set; } = new List<AppIdentityUserClaim>();

        public Team ActiveTeam { get; set; }
    }
}
