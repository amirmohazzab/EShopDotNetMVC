using Eshop.Application.Security;
using Eshop.Application.Services.Interfaces;
using Eshop.Domain.Enums.User;
using Eshop.Domain.Interfaces;
using Eshop.Domain.Models.User;
using Eshop.Domain.ViewModels.Account;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Application.Services.Implamentations
{
    public class AccountService (IUserRepository userRepository) : IAccountService
    {

        public async Task<RegisterViewModel.RegisterResult> RegisterAsync(RegisterViewModel model)
        {
            if (await userRepository.ExistMobileAsync(model.Mobile))
                return RegisterViewModel.RegisterResult.MobileDuplicated;

            string hashPassword = model.Password.EncodePasswordMd5();

            User user = new()
            {
                CreateDate = DateTime.Now,
                FirstName = null,
                LastName = null,
                Mobile = model.Mobile,
                Password = hashPassword,
                Email = null,
                Status = UserStatus.Active
            };

            await userRepository.InsertAsync(user);
            await userRepository.SaveAsync();

            return RegisterViewModel.RegisterResult.Success;
        }

        public async Task<LoginViewModel.LoginResult> LoginAsync(LoginViewModel model)
        {
            string hashPassword = model.Password.EncodePasswordMd5();
            User? user = await userRepository.GetUserByMobileAndPasswordAsync(model.Mobile, hashPassword);

            if (user == null)
                return LoginViewModel.LoginResult.UserNotFound;

            if (user.Status == UserStatus.Banned)
                return LoginViewModel.LoginResult.UserBanned;

            return LoginViewModel.LoginResult.Success;
        }

    }
}
