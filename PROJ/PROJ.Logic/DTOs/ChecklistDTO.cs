using System.Collections.Generic;

namespace PROJ.Logic.DTOs
{
    public class ChecklistDTO : DTOBase
    {
        public string Name { get; set; }

        public IList<ChecklistTaskDTO> ChecklistTasks { get; set; }

        public bool IsComplete { get; set; }
    }
}
