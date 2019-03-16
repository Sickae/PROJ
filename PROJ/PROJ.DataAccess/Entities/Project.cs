﻿using System.Collections.Generic;

namespace PROJ.DataAccess.Entities
{
    public class Project : Entity
    {
        public virtual string Name { get; set; }

        public virtual IList<TaskGroup> TaskGroups { get; set; }

        public virtual User Owner { get; set; }

        public virtual IList<User> Users { get; set; }
    }
}
