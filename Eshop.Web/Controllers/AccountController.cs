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
            if (User.Identity.IsAuthenticated)
                return Redirect("/");

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
            if (User.Identity.IsAuthenticated)
                return Redirect("/");

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

        #region Logout

        [HttpGet("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Login));
        }

        #endregion

        #region Forgot Password

        [HttpGet("/forgot-password")]
        public IActionResult ForgotPassword()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/");

            return View();
        }

        [HttpPost("/forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            #region Validations

            if (!ModelState.IsValid)
                return View(model);

            #endregion

            ForgotPasswordViewModel.ForgotPasswordResult result = await accountService.ForgotPasswordAsync(model);

            switch (result)
            {
                case ForgotPasswordViewModel.ForgotPasswordResult.Success:
                    TempData["Mobile"] = model.Mobile;
                    TempData[SuccessMessage] = SuccessMessages.ForgotPasswordSuccessfullyDone;
                    return RedirectToAction(nameof(ResetPassword));

                case ForgotPasswordViewModel.ForgotPasswordResult.MobileNotFound:
                    TempData[ErrorMessage] = ErrorMessages.UserNotFound;
                    return RedirectToAction(nameof(ForgotPassword));

                case ForgotPasswordViewModel.ForgotPasswordResult.Error:
                    TempData[ErrorMessage] = ErrorMessages.OperationFailed;
                    return RedirectToAction(nameof(ForgotPassword));
            }

            return View(model);
        }

        #endregion

        #region Reset Password

        [HttpGet("/reset-password")]
        public IActionResult ResetPassword()
        {
            string mobile = TempData["Mobile"]?.ToString();

            if (string.IsNullOrEmpty(mobile))
                return NotFound();

            return View(new ResetPasswordViewModel() { Mobile = mobile });
        }

        [HttpPost("/reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            #region Validations

            if (!ModelState.IsValid)
                return View(model);

            #endregion

            ResetPasswordResult result = await accountService.ResetPasswordAsync(model);

            switch (result)
            {
                case ResetPasswordResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.ResetPasswordSuccessfullyDone;
                    return RedirectToAction(nameof(Login));

                case ResetPasswordResult.UserNotFound:
                    TempData[ErrorMessage] = ErrorMessages.UserNotFound;
                    return RedirectToAction(nameof(ForgotPassword));
            }


            return View(model);
        }


        #endregion 

        #endregion

    }
}
