namespace PROJ.Logic.DTOs
{
    public class UserClaimDTO : DTOBase
    {
        public virtual UserDTO User { get; set; }

        public virtual string ClaimType { get; set; }

        public virtual string ClaimValue { get; set; }
    }
}
