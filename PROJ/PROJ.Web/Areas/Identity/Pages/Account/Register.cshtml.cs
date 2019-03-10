using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PROJ.Logic.Identity;
using PROJ.Logic.Identity.Managers;
using PROJ.Web.Controllers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PROJ.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager _signInManager;
        private readonly IdentityUserManager _identityUserManager;
        private readonly ILogger<LoginModel> _logger;

        public RegisterModel(SignInManager signInManager, IdentityUserManager userManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
            _identityUserManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [DisplayName("Username")]
            [Required(ErrorMessage = "Username cannot be left blank.")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Password cannot be left blank.")]
            public string Password { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            await Task.Run(() => { });
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = new AppIdentityUser { UserName = Input.Username };
                var result = await _identityUserManager.CreateAsync(user, Input.Password);
                if (result != null && result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, true);
                    return RedirectToAction(nameof(HomeController.Dashboard), "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return Page();
        }
    }
}