using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PROJ.Logic.Identity;
using PROJ.Logic.Managers;
using PROJ.Web.Controllers;
using System.Threading.Tasks;

namespace PROJ.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager _signInManager;
        private readonly IdentityUserManager _userManager;
        private readonly ILogger<LoginModel> _logger;

        public RegisterModel(SignInManager signInManager, IdentityUserManager userManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            public string Username { get; set; }

            public string Password { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = new AppIdentityUser { UserName = Input.Username };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result != null && result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, true);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }

            return Page();
        }
    }
}