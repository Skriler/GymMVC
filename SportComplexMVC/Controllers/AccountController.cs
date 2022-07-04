using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SportComplexMVC.Models.Entities;
using SportComplexMVC.Models.DataDb;
using SportComplexMVC.Models.ViewModels;
using SportComplexMVC.Services.DAL;
using SportComplexMVC.Enums;
using SportComplexMVC.Services;

namespace SportComplexMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountDAL accountDAL;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationContext context)
        {
            accountDAL = new AccountDAL(userManager, signInManager, context);
        }

        [HttpGet]
        public async Task<ActionResult> RegisterAsync()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel()
            {
                Genders = await accountDAL.GetGenderListAsync(),
                BirthDate = DateTime.Now.AddYears(-30),
                MinBirthDate = DateTime.Now.AddYears(-70),
                MaxBirthDate = DateTime.Now.AddYears(-18)
            };

            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await accountDAL.RegisterAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            model.Genders = await accountDAL.GetGenderListAsync();

            return View(model);
        }

        [HttpGet]
        public ViewResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginAsync(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await accountDAL.LoginAsync(model);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrong username and/or password");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToActionResult> LogoutAsync()
        {
            await accountDAL.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> ProfileAsync()
        {
            ProfileViewModel profileViewModel;

            ViewData["IsLeaveButton"] = false;

            if (User.IsInRole(RoleEnum.Client.ToString()))
            {
                profileViewModel = await accountDAL.GetClientProfileViewModelAsync(User);

                if (DataChecker.CheckIsCurrentClientInGroup(await accountDAL.GetCurrentClientAsync(User)))
                    ViewData["IsLeaveButton"] = true;
            }
            else if (User.IsInRole(RoleEnum.Coach.ToString()))
            {
                profileViewModel = await accountDAL.GetCoachProfileViewModelAsync(User);
            }
            else
            {
                profileViewModel = await accountDAL.GetUserProfileViewModelAsync(User);
            }

            return View(profileViewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> EditAsync()
        {
            EditProfileViewModel editProfileViewModel = await accountDAL.GetEditUserProfileViewModelAsync(User);

            return View(editProfileViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditAsync(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await accountDAL.EditUserAsync(User, model);

            return RedirectToAction("Profile");
        }
    }
}
