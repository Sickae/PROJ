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
        private readonly IAppContext _appContext;

        public ProjectController(ProjectRepository projectRepository, IProjectManager projectManager, IAppContext appContext)
        {
            _projectRepository = projectRepository;
            _projectManager = projectManager;
            _appContext = appContext;
        }

        public IActionResult Index()
        {
            FillViewBags();
            return View();
        }

        public IActionResult Create(string name)
        {
            var project = new ProjectDTO { Name = name };
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

        private void FillViewBags()
        {
            ViewBag.ProjectsList = _projectRepository.GetProjectsForCurrentUser();
        }
    }
}