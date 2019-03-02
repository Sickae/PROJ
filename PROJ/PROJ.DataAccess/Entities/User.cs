using PROJ.Shared.Attributes;
using System;

namespace PROJ.DataAccess.Entities
{
    public class User : Entity
    {
        [NotNull]
        public virtual string UserName { get; set; }

        public virtual string NormalizedUserName { get; set; }

        [NotNull]
        public virtual string PasswordHash { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual int AccessFailedCount { get; set; }

        //public virtual DateTimeOffset? LockoutEndDate { get; set; }

        public virtual bool TwoFactorEnabled { get; set; }

        public virtual DateTime? LastLoginDate { get; set; }

        public virtual UserClaim UserClaims { get; set; }
    }
}
