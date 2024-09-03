using Eshop.Application.Services.Interfaces;
using Eshop.Domain.Interfaces;
using Eshop.Domain.Models.User;
using Eshop.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Application.Services.Implamentations
{
    public class UserService (IUserRepository userRepository) : IUserService
    {
        public async Task<User?> GetByMobileAsync(string mobile)
        {
            return await userRepository.GetUserByMobileAsync(mobile);
        }

        public async Task<EditUserProfileViewModel> GetProfileForEditAsync(int UserId)
        {
            User? user = await userRepository.GetByIdAsync(UserId);

            if (user == null)
                return null;

            return new EditUserProfileViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

        }

        public async Task<EditUserProfileResult> UpdateUserProfileAsync(EditUserProfileViewModel model)
        {
            if (!model.UserId.HasValue)
                return EditUserProfileResult.UserNotFound;

            User? user = await userRepository.GetByIdAsync(model.UserId.Value);

            if (user == null)
                return EditUserProfileResult.UserNotFound;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;

            userRepository.Update(user);
            await userRepository.SaveAsync();

            return EditUserProfileResult.Success;
        }
    }
}
