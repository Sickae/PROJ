using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace PROJ.Logic.Identity.Managers
{
    public class SignInManager : SignInManager<AppIdentityUser>
    {
        public SignInManager(IdentityUserManager identityUserManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<AppIdentityUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<AppIdentityUser>> logger,
            IAuthenticationSchemeProvider schemes)
            : base(identityUserManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        { }
    }
}
