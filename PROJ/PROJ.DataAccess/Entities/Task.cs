using PROJ.Shared.Attributes;
using PROJ.Shared.Enums;
using System.Collections.Generic;

namespace PROJ.DataAccess.Entities
{
    public class Task : Entity
    {
        public virtual Comment Project { get; set; }

        public virtual string Name { get; set; }

        [Text]
        public virtual string Description { get; set; }

        public virtual IList<User> AssignedUsers { get; set; }

        public virtual Priority Priority { get; set; }

        public virtual IEnumerable<Checklist> Checklists { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; }

        public virtual TaskGroup TaskGroup { get; set; }

        public virtual bool IsCompleted { get; set; }
    }
}
