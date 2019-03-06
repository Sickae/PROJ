﻿using PROJ.Shared.Enums;
using System.Collections.Generic;

namespace PROJ.Logic.DTOs
{
    public class TaskDTO : DTOBase
    {
        public ProjectDTO Project { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public UserDTO AssignedUser { get; set; }

        public Priority Priority { get; set; }

        public IList<ChecklistDTO> Checklists { get; set; }

        public IList<CommentDTO> Comments { get; set; }
    }
}