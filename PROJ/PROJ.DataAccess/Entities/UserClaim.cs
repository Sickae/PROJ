using PROJ.Shared.Attributes;

namespace PROJ.DataAccess.Entities
{
    [DeletableEntity]
    public class UserClaim : Entity
    {
        public virtual User User { get; set; }

        public virtual string ClaimType { get; set; }

        public virtual string ClaimValue { get; set; }
    }
}
