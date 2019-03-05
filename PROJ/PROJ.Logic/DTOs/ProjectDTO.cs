using System.Collections.Generic;

namespace PROJ.Logic.DTOs
{
    public class ProjectDTO : DTOBase
    {
        public string Name { get; set; }

        public IList<TaskGroupDTO> TaskGroups { get; set; }
    }
}
