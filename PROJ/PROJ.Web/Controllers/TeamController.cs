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
    public class TeamController : Controller
    {
        private readonly TeamRepository _teamRepository;
        private readonly UserRepository _userRepository;
        private readonly ITeamManager _teamManager;
        private readonly IUserManager _userManager;
        private readonly IAppContext _appContext;

        public TeamController(TeamRepository teamRepository, ITeamManager teamManager, UserRepository userRepository, IUserManager userManager, IAppContext appContext)
        {
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _teamManager = teamManager;
            _userManager = userManager;
            _appContext = appContext;
        }

        public IActionResult Index()
        {
            FillViewBags();
            return View();
        }

        public IActionResult Create(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Team name cannot be empty."
                });
            }

            var team = new TeamDTO { Name = name };
            _teamManager.Create(team);

            return Json(new { success = true });
        }

        public IActionResult Show(int id)
        {
            var team = _teamRepository.Get(id);
            FillViewBags();

            if (team == null)
            {
                return View("NotFound");
            }

            var vm = Mapper.Map<TeamViewModel>(team);
            ViewBag.ActiveTeamId = vm.Id;

            return View(vm);
        }

        public IActionResult Rename(int teamId, string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Team name cannot be empty."
                });
            }

            var team = _teamRepository.Get(teamId);

            if (team == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Invalid Team."
                });
            }

            team.Name = name;
            _teamManager.Save(team);

            return Json(new { success = true });
        }

        public IActionResult Delete(int teamId)
        {
            var team = _teamRepository.Get(teamId);

            if (team == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Invalid Team."
                });
            }

            _teamManager.Delete(team);
            var redirectUrl = Url.Action(nameof(Index), "Team");

            return Json(new
            {
                success = true,
                redirectUrl
            });
        }

        public IActionResult SetActive(int teamId)
        {
            var team = _teamRepository.Get(teamId);
            var user = _userRepository.GetCurrentUser();

            if (user == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "You are not logged in."
                });
            }

            if (team == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Invalid Team."
                });
            }

            user.ActiveTeam = new TeamDTO { Id = teamId };
            _userManager.Save(user);

            return Json(new { success = true });
        }

        public IActionResult AddMember(int teamId, string username)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Username cannot be empty."
                });
            }

            var team = _teamRepository.Get(teamId);
            var user = _userRepository.FindByName(username);

            if (team == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Invalid Team."
                });
            }

            if (user == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = $"There is no User called '{username}'."
                });
            }

            team.Members.Add(user);
            _teamManager.Save(team);

            return Json(new { success = true });
        }

        public IActionResult RemoveMember(int teamId, int userId)
        {
            var team = _teamRepository.Get(teamId);
            var user = _userRepository.Get(userId);

            if (team == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Invalid Team."
                });
            }

            if (user == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "This User is not part of your Team."
                });
            }

            var toRemove = team.Members.FirstOrDefault(x => x.Id == userId);

            if (toRemove == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "This User is not part of your Team."
                });
            }

            if (user.ActiveTeam.Id == teamId)
            {
                user.ActiveTeam = null;
                _userManager.Save(user);
            }

            team.Members.Remove(toRemove);
            _teamManager.Save(team);

            if (userId == _appContext.UserId.Value)
            {
                return Json(new
                {
                    success = true,
                    redirectUrl = Url.Action(nameof(TeamController.Index), "Team")
                });
            }
            else
            {
                return Json(new { success = true });
            }
        }

        private void FillViewBags()
        {
            ViewBag.TeamsList = _teamRepository.GetTeamsForCurrentUser();
        }
    }
}