using Eshop.Application.Services.Interfaces;
using Eshop.Domain.Models.User;
using Eshop.Domain.Shared;
using Eshop.Domain.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace Eshop.Web.Controllers
{
    public class AccountController 
        (IAccountService accountService,
        IUserService userService) 
        : SiteBaseController
    {

        #region Actions

        #region Register

        [HttpGet("/register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            #region Validations

            if(!ModelState.IsValid)
                return View(model);

            #endregion

            RegisterViewModel.RegisterResult result = await accountService.RegisterAsync(model);

            switch (result)
            {
                case RegisterViewModel.RegisterResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.RegisterSuccessfullyDone;
                    return RedirectToAction("Login");
                
                case RegisterViewModel.RegisterResult.MobileDuplicated:
                    TempData[ErrorMessage] = ErrorMessages.MobileDuplicated;
                    break;

            }

            return View(model);
        }

        #endregion

        #region Login

        [HttpGet("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            #region Validations

            if (!ModelState.IsValid)
                return View(model);

            #endregion

            LoginViewModel.LoginResult result = await accountService.LoginAsync(model);

            switch (result)
            {
                case LoginViewModel.LoginResult.Success:
                    User user = await userService.GetByMobileAsync(model.Mobile);

                    if (user == null)
                    {
                        TempData[ErrorMessage] = ErrorMessages.UserNotFound;
                        return View(model);
                    }

                    #region Login

                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.MobilePhone, model.Mobile),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    };

                    ClaimsIdentity claimsIdentity = 
                        new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(claimsPrincipal, properties);

                    #endregion
                    TempData[SuccessMessage] = "خوش آمدید";
                    return Redirect("/");

                    

                case LoginViewModel.LoginResult.UserNotFound:
                    TempData[ErrorMessage] = ErrorMessages.UserNotFound;
                    break;

                case LoginViewModel.LoginResult.UserBanned:
                    TempData[ErrorMessage] = ErrorMessages.UserBanned;
                    break;

                case LoginViewModel.LoginResult.UserIsNotActive:
                    TempData[ErrorMessage] = ErrorMessages.UserIsNotActive;
                    break;

            }
            return View(model);
        }

        #endregion

        #endregion

    }
}
