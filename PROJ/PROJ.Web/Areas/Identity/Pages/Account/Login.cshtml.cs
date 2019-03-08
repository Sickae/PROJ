using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PROJ.Logic.Identity.Managers;
using System;
using System.Threading.Tasks;

namespace PROJ.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager _signInManager;
        private readonly IdentityUserManager _identityUserManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager signInManager, IdentityUserManager userManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
            _identityUserManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            public string Username { get; set; }

            public string Password { get; set; }

            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ReturnUrl = returnUrl;
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = await _identityUserManager.FindByNameAsync(Input.Username);
                var result = await _signInManager.CheckPasswordSignInAsync(user, Input.Password, false);
                await _identityUserManager.UpdateSecurityStampAsync(user);

                if (result != null && result.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, true);
                    _logger.LogInformation("User logged in.");

                    user.LastLoginDate = DateTime.UtcNow;
                    await _identityUserManager.UpdateAsync(user);

                    return LocalRedirect(returnUrl);
                }
            }

            return Page();
        }
    }
}