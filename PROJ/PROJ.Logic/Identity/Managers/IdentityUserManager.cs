using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PROJ.Logic.Identity;
using System;
using System.Collections.Generic;
using System.Threading;

namespace PROJ.Logic.Identity.Managers
{
    public class IdentityUserManager : UserManager<AppIdentityUser>
    {
        private readonly CancellationToken _cancellationToken;

        public IdentityUserManager(IUserStore<AppIdentityUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<AppIdentityUser> passwordHasher,
            IEnumerable<IUserValidator<AppIdentityUser>> userValidators,
            IEnumerable<IPasswordValidator<AppIdentityUser>> passwordValidators,
            ILookupNormalizer keyNormalizer, AppIdentityErrorDescriber errors,
            IServiceProvider services, ILogger<UserManager<AppIdentityUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _cancellationToken = services?.GetService<IHttpContextAccessor>()?.HttpContext?.RequestAborted ?? CancellationToken.None;
        }

        protected override CancellationToken CancellationToken => _cancellationToken;
    }
}
