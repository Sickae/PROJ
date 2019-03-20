using PROJ.Shared.Attributes;
using System.Collections.Generic;

namespace PROJ.DataAccess.Entities
{
    public class Team : Entity
    {
        public virtual string Name { get; set; }

        public virtual IList<Project> Projects { get; set; }

        public virtual IList<User> Members { get; set; }

        [NotNull]
        public virtual User Owner { get; set; }
    }
}
