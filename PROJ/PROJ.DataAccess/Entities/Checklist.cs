using System.Collections.Generic;

namespace PROJ.DataAccess.Entities
{
    public class Checklist : Entity
    {
        public virtual string Name { get; set; }

        public virtual IEnumerable<ChecklistTask> ChecklistTasks { get; set; }

        public virtual bool IsComplete { get; set; }
    }
}
