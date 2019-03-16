using System.Collections.Generic;

namespace PROJ.DataAccess.Entities
{
    public class TaskGroup : Entity
    {
        public virtual string Name { get; set; }

        public virtual IEnumerable<Task> Tasks { get; set; }

        public virtual Project Project { get; set; }
    }
}
