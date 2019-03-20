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
                return Json(false);
            }

            var team = new TeamDTO { Name = name };
            _teamManager.Create(team);

            return Json(true);
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
                return Json(false);
            }

            var team = _teamRepository.Get(teamId);

            if (team == null)
            {
                return Json(false);
            }

            team.Name = name;
            _teamManager.Save(team);

            return Json(true);
        }

        public IActionResult Delete(int teamId)
        {
            var team = _teamRepository.Get(teamId);

            if (team == null)
            {
                return Json(new { success = false });
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

            if (team == null || user == null)
            {
                return Json(false);
            }

            user.ActiveTeam = new TeamDTO { Id = teamId };
            _userManager.Save(user);

            return Json(true);
        }

        public IActionResult AddMember(int teamId, string username)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
            {
                return Json(false);
            }

            var team = _teamRepository.Get(teamId);
            var user = _userRepository.FindByName(username);

            if (team == null || user == null)
            {
                return Json(false);
            }

            team.Members.Add(user);
            _teamManager.Save(team);

            return Json(true);
        }

        public IActionResult RemoveMember(int teamId, int userId)
        {
            var team = _teamRepository.Get(teamId);
            var user = _userRepository.Get(userId);

            if (team == null || user == null)
            {
                return Json(new { success = false });
            }

            var toRemove = team.Members.FirstOrDefault(x => x.Id == userId);

            if (toRemove == null)
            {
                return Json(new { success = false });
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