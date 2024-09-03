using Eshop.Application.Extension;
using Eshop.Application.Services.Interfaces;
using Eshop.Domain.Shared;
using Eshop.Domain.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Eshop.Web.Areas.UserPanel.Controllers
{
    public class UserController (IUserService userService) : UserPanelBaseController
    {
        public async Task<IActionResult> EditProfile()
        {
            EditUserProfileViewModel? model = await userService.GetProfileForEditAsync(User.GetUserId());

            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditUserProfileViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            model.UserId = User.GetUserId();

            EditUserProfileResult result = await userService.UpdateUserProfileAsync(model);

            switch (result)
            {
                case EditUserProfileResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.UpdateProfileSuccessfullyDone;
                    return RedirectToAction(nameof(EditProfile));

                case EditUserProfileResult.UserNotFound:
                    TempData[ErrorMessage] = ErrorMessages.UserNotFound;
                    return RedirectToAction(nameof(EditProfile));
            };

            return View(model);
        }
    }
}
