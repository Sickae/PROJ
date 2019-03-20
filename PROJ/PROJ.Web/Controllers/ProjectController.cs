using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;
using PROJ.Logic.UnitOfWork.Managers.Interfaces;
using PROJ.Logic.UnitOfWork.Repositories;
using PROJ.Web.Models;

namespace PROJ.Web.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly ProjectRepository _projectRepository;
        private readonly IProjectManager _projectManager;
        private readonly ITaskGroupManager _taskGroupManager;
        private readonly IAppContext _appContext;

        public ProjectController(ProjectRepository projectRepository, IProjectManager projectManager, ITaskGroupManager taskGroupManager, IAppContext appContext)
        {
            _projectRepository = projectRepository;
            _projectManager = projectManager;
            _taskGroupManager = taskGroupManager;
            _appContext = appContext;
        }

        public IActionResult Index()
        {
            FillViewBags();
            ViewBag.HasActiveTeam = _appContext.ActiveTeamId.HasValue && _appContext.ActiveTeamId.Value != 0;
            return View();
        }

        public IActionResult Create(int teamId, string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return Json(false);
            }

            var project = new ProjectDTO
            {
                Name = name,
                Team = new TeamDTO { Id = teamId }
            };
            _projectManager.Create(project);

            return Json(true);
        }

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

        public IActionResult AddNewGroup(int projectId, string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return Json(false);
            }

            var project = _projectRepository.Get(projectId);

            if (project == null)
            {
                return Json(false);
            }

            var group = new TaskGroupDTO
            {
                Name = name,
                Project = project
            };

            _taskGroupManager.Create(group);
            return Json(true);
        }

        public IActionResult Rename(int projectId, string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return Json(false);
            }

            var project = _projectRepository.Get(projectId);

            if (project == null)
            {
                return Json(false);
            }

            project.Name = name;
            _projectManager.Save(project);

            return Json(true);
        }

        public IActionResult Delete(int projectId)
        {
            var project = _projectRepository.Get(projectId);

            if (project == null)
            {
                return Json(new { success = false });
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
        }
    }
}