using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PROJ.Logic.Identity.Managers;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
            [DisplayName("Username")]
            [Required(ErrorMessage = "Username cannot be left blank.")]
            public string Username { get; set; }

            [DisplayName("Password")]
            [Required(ErrorMessage = "Password cannot be left blank.")]
            public string Password { get; set; }

            [DisplayName("Remember me")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            await Task.Run(() => ReturnUrl = returnUrl);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = await _identityUserManager.FindByNameAsync(Input.Username);

                if (user == null)
                {
                    ModelState.AddModelError("", "Username or password is incorrect.");
                    return Page();
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, Input.Password, false);
                await _identityUserManager.UpdateSecurityStampAsync(user);

                if (result != null && result.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, Input.RememberMe);
                    _logger.LogInformation("User logged in.");

                    user.LastLoginDate = DateTime.UtcNow;
                    await _identityUserManager.UpdateAsync(user);

                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect.");
                }
            }

            return Page();
        }
    }
}