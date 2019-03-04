using Microsoft.AspNetCore.Mvc;
using PROJ.Logic.Managers;
using System.Threading.Tasks;

namespace PROJ.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager _signInManager;

        public UserController(SignInManager signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}