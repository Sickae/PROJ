using System.Collections.Generic;

namespace PROJ.Logic.DTOs
{
    public class TeamDTO : DTOBase
    {
        public string Name { get; set; }

        public IList<ProjectDTO> Projects { get; set; }

        public IList<UserDTO> Members { get; set; }

        public UserDTO Owner { get; set; }
    }
}
