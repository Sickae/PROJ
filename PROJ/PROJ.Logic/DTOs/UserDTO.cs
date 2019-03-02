using System;

namespace PROJ.Logic.DTOs
{
    public class UserDTO : DTOBase
    {
        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string PasswordHash { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public UserClaimDTO UserClaims { get; set; }
    }
}
