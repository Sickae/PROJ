using Microsoft.AspNetCore.Http;
using PROJ.Logic.Identity.Managers;
using PROJ.Logic.Interfaces;
using System;

namespace PROJ.Web.Infrastructure
{
    public class AppContext : IAppContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Lazy<IdentityUserManager> _identityUserManager;

        public int? UserId => int.TryParse(_identityUserManager.Value.GetUserId(_httpContextAccessor.HttpContext.User), out var id)
            ? id
            : (int?)null;

        public AppContext(IHttpContextAccessor httpContextAccessor, Lazy<IdentityUserManager> identityUserManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityUserManager = identityUserManager;
        }
    }
}
