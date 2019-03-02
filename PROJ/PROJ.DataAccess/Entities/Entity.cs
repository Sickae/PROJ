using System;

namespace PROJ.DataAccess.Entities
{
    public abstract class Entity
    {
        public virtual int Id { get; set; }

        public virtual DateTime CreationDate { get; set; }

        public virtual DateTime ModificationDate { get; set; }

        protected Entity()
        {
            CreationDate = DateTime.UtcNow;
            ModificationDate = DateTime.UtcNow;
        }
    }
}
