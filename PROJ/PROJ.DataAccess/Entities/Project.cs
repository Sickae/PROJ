using System.Collections.Generic;

namespace PROJ.DataAccess.Entities
{
    public class Project : Entity
    {
        public virtual string Name { get; set; }

        public virtual IEnumerable<TaskGroup> TaskGroups { get; set; }
    }
}
