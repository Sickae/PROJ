using System.Collections.Generic;

namespace PROJ.Logic.DTOs
{
    public class TaskGroupDTO : DTOBase
    {
        public string Name { get; set; }

        public IList<TaskDTO> Tasks { get; set; }

        public ProjectDTO Project { get; set; }
    }
}
