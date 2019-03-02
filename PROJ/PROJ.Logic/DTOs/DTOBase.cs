using System;

namespace PROJ.Logic.DTOs
{
    public class DTOBase
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        protected DTOBase()
        {
            CreationDate = DateTime.UtcNow;
            ModificationDate = DateTime.UtcNow;
        }
    }
}
