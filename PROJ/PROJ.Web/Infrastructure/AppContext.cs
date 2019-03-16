using Microsoft.AspNetCore.Http;
using PROJ.Logic.DTOs;
using PROJ.Logic.Identity.Managers;
using PROJ.Logic.Interfaces;
using PROJ.Logic.UnitOfWork.Repositories;
using System;
using System.Collections.Generic;

namespace PROJ.Web.Infrastructure
{
    public class AppContext : IAppContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Lazy<IdentityUserManager> _identityUserManager;
        private readonly Lazy<ProjectRepository> _projectRepository;

        public int? UserId => int.TryParse(_identityUserManager.Value.GetUserId(_httpContextAccessor.HttpContext.User), out var id)
            ? id
            : (int?)null;

        public IList<ProjectDTO> Projects => _projectRepository.Value.GetProjectsForCurrentUser();

        public AppContext(IHttpContextAccessor httpContextAccessor, Lazy<IdentityUserManager> identityUserManager, Lazy<ProjectRepository> projectRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityUserManager = identityUserManager;
            _projectRepository = projectRepository;
        }
    }
}
