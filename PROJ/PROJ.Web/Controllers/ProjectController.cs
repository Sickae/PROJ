using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;
using PROJ.Logic.UnitOfWork.Managers.Interfaces;
using PROJ.Logic.UnitOfWork.Repositories;
using PROJ.Web.Models;
using System.Linq;

namespace PROJ.Web.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly ProjectRepository _projectRepository;
        private readonly TaskRepository _taskRepository;
        private readonly IProjectManager _projectManager;
        private readonly ITaskGroupManager _taskGroupManager;
        private readonly IAppContext _appContext;

        public ProjectController(ProjectRepository projectRepository, IProjectManager projectManager, ITaskGroupManager taskGroupManager, TaskRepository taskRepository, IAppContext appContext)
        {
            _projectRepository = projectRepository;
            _projectManager = projectManager;
            _taskGroupManager = taskGroupManager;
            _taskRepository = taskRepository;
            _appContext = appContext;
        }

        public IActionResult Index()
        {
            FillViewBags();
            ViewBag.HasActiveTeam = _appContext.ActiveTeamId.HasValue && _appContext.ActiveTeamId.Value != 0;
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(int teamId, string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Project name cannot be empty."
                });
            }

            var project = new ProjectDTO
            {
                Name = name,
                Team = new TeamDTO { Id = teamId }
            };
            _projectManager.Create(project);

            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult Show(int id)
        {
            var project = _projectRepository.Get(id);
            FillViewBags();

            if (project == null)
            {
                return View("NotFound");
            }

            var vm = Mapper.Map<ProjectViewModel>(project);
            ViewBag.ActiveProjectId = vm.Id;

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddNewGroup(int projectId, string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Task Group name cannot be empty."
                });
            }

            var project = _projectRepository.Get(projectId);

            if (project == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Invalid Project."
                });
            }

            var group = new TaskGroupDTO
            {
                Name = name,
                Project = project
            };

            _taskGroupManager.Create(group);
            return Json(new { success = true });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Rename(int projectId, string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Project name cannot be empty."
                });
            }

            var project = _projectRepository.Get(projectId);

            if (project == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Invalid Project."
                });
            }

            project.Name = name;
            _projectManager.Save(project);

            return Json(new { success = true });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(int projectId)
        {
            var project = _projectRepository.Get(projectId);

            if (project == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Invalid Project"
                });
            }

            _projectManager.Delete(project);
            var redirectUrl = Url.Action(nameof(Index), "Project");

            return Json(new
            {
                success = true,
                redirectUrl
            });
        }

        private void FillViewBags()
        {
            ViewBag.ProjectsList = _projectRepository.GetProjectsForCurrentUser();
            ViewBag.AssignedTaskIds = _taskRepository.GetTasksForCurrentUser().Select(x => x.Id);
        }
    }
}