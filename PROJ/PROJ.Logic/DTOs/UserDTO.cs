﻿using System;
using System.Collections.Generic;

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

        public IList<UserClaimDTO> UserClaims { get; set; }

        public IList<TeamDTO> Teams { get; set; }

        public TeamDTO ActiveTeam { get; set; }

        public IList<TaskDTO> AssignedTasks { get; set; }
    }
}
