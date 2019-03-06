using Microsoft.AspNetCore.Mvc;
using PROJ.Logic.DTOs;
using PROJ.Logic.Identity.Managers;
using PROJ.Logic.UnitOfWork;
using PROJ.Logic.UnitOfWork.Repositories;
using System.Threading.Tasks;

namespace PROJ.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager _signInManager;
        private readonly CommentRepository commentRepository;
        private readonly CommentManager commentManager;
        private readonly UserManager userManager;

        public UserController(SignInManager signInManager, CommentRepository commentRepository, CommentManager commentManager, UserManager userManager)
        {
            _signInManager = signInManager;
            this.commentRepository = commentRepository;
            this.commentManager = commentManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}