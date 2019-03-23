using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PROJ.Logic.DTOs
{
    public class DTOBase
    {
        public int Id { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy. MM. dd. HH:mm:ss}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Modified")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy. MM. dd. HH:mm:ss}")]
        public DateTime ModificationDate { get; set; }

        protected DTOBase()
        {
            CreationDate = DateTime.UtcNow;
            ModificationDate = DateTime.UtcNow;
        }
    }
}
