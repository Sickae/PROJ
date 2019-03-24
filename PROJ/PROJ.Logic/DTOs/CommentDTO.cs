namespace PROJ.Logic.DTOs
{
    public class CommentDTO : DTOBase
    {
        public UserDTO Author { get; set; }

        public string Text { get; set; }

        public TaskDTO Task { get; set; }
    }
}
