﻿namespace PROJ.DataAccess.Entities
{
    public class ChecklistTask : Entity
    {
        public virtual string Name { get; set; }

        public virtual bool IsComplete { get; set; }
    }
}
