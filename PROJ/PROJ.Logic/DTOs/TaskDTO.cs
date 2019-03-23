using PROJ.Shared.Enums;
using System.Collections.Generic;
using System.ComponentModel;

namespace PROJ.Logic.DTOs
{
    public class TaskDTO : DTOBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IList<UserDTO> AssignedUsers { get; set; }

        [DisplayName("Priority")]
        public Priority Priority { get; set; }

        public IList<ChecklistDTO> Checklists { get; set; }

        public IList<CommentDTO> Comments { get; set; }

        public TaskGroupDTO TaskGroup { get; set; }

        [DisplayName("Completed")]
        public bool IsCompleted { get; set; }

        [DisplayName("Estimated hours")]
        public int? EstimatedHours { get; set; }    
    }
}
