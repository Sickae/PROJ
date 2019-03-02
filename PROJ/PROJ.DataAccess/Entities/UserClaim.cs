namespace PROJ.DataAccess.Entities
{
    public class UserClaim : Entity
    {
        public virtual User User { get; set; }

        public virtual string ClaimType { get; set; }

        public virtual string ClaimValue { get; set; }
    }
}
