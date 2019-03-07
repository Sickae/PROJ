using Microsoft.AspNetCore.Mvc;
using PROJ.Logic.Identity.Managers;
using PROJ.Logic.UnitOfWork.Managers.Interfaces;
using PROJ.Logic.UnitOfWork.Repositories;
using System.Threading.Tasks;

namespace PROJ.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager _signInManager;
        private readonly UserRepository _userRepository;
        private readonly IUserManager _userManager;

        public UserController(SignInManager signInManager, UserRepository userRepository, IUserManager userManager)
        {
            _signInManager = signInManager;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}