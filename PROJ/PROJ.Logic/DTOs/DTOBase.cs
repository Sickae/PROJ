using System;
using System.ComponentModel;

namespace PROJ.Logic.DTOs
{
    public class DTOBase
    {
        public int Id { get; set; }

        [DisplayName("Created")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Modified")]
        public DateTime ModificationDate { get; set; }

        protected DTOBase()
        {
            CreationDate = DateTime.UtcNow;
            ModificationDate = DateTime.UtcNow;
        }
    }
}
