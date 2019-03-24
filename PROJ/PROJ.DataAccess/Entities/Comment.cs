using PROJ.Shared.Attributes;

namespace PROJ.DataAccess.Entities
{
    public class Comment : Entity
    {
        public virtual User Author { get; set; }

        [Text]
        public virtual string Text { get; set; }

        public virtual Task Task { get; set; }
    }
}
