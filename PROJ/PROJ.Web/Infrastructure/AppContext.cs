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
        private readonly Lazy<UserRepository> _userRepository;
        private readonly Lazy<TeamRepository> _teamRepository;
        private readonly Lazy<TaskRepository> _taskRepository;

        public int? UserId => int.TryParse(_identityUserManager.Value.GetUserId(_httpContextAccessor.HttpContext.User), out var id)
            ? id
            : (int?)null;

        public int? ActiveTeamId => UserId.HasValue
            ? _userRepository.Value.Get(UserId.Value).ActiveTeam?.Id
            : null;

        public UserDTO CurrentUser => _userRepository.Value.GetCurrentUser();

        public IList<ProjectDTO> Projects => _projectRepository.Value.GetProjectsForCurrentUser();

        public IList<TeamDTO> Teams => _teamRepository.Value.GetTeamsForCurrentUser();

        public IList<TaskDTO> AssignedTasks => _taskRepository.Value.GetTasksForCurrentUser();

        public AppContext(IHttpContextAccessor httpContextAccessor, Lazy<IdentityUserManager> identityUserManager, Lazy<ProjectRepository> projectRepository, Lazy<UserRepository> userRepository, Lazy<TeamRepository> teamRepository, Lazy<TaskRepository> taskRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityUserManager = identityUserManager;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _taskRepository = taskRepository;
        }
    }
}
