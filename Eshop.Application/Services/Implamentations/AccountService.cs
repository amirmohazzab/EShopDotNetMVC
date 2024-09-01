using Eshop.Application.Generators;
using Eshop.Application.Security;
using Eshop.Application.Services.Interfaces;
using Eshop.Domain.Enums.User;
using Eshop.Domain.Interfaces;
using Eshop.Domain.Models.User;
using Eshop.Domain.ViewModels.Account;
using Kavenegar.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Application.Services.Implamentations
{
    public class AccountService 
        (IUserRepository userRepository,
        ISmsSendersService smsSendersService) 
        : IAccountService
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

        public async Task<ForgotPasswordViewModel.ForgotPasswordResult> ForgotPasswordAsync(ForgotPasswordViewModel model)
        {
            User? user = await userRepository.GetUserByMobileAsync(model.Mobile);

            if (user == null)
                return ForgotPasswordViewModel.ForgotPasswordResult.MobileNotFound;

            string randomCode = CodeGenerator.GenerateCode();

            //var result = smsSendersService.SendSms(user.Mobile, $"کد تایید شما: {randomCode}");

            SendResult result = new SendResult();
            result.Status = 200;

            if (result.Status == 200)
            {
                user.ConfirmCode = randomCode;

                userRepository.Update(user);
                await userRepository.SaveAsync();

                return ForgotPasswordViewModel.ForgotPasswordResult.Success;
            }
            else
            {
                return ForgotPasswordViewModel.ForgotPasswordResult.Error;
            }
        }

        public async Task<ResetPasswordResult> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            User? user = await userRepository.GetByMobileAndConfirmCode(model.Mobile, model.ConfirmCode);

            if (user == null)
                return ResetPasswordResult.UserNotFound;

            string hashPassword = model.Password.EncodePasswordMd5();

            user.Password = hashPassword;

            user.ConfirmCode = null;

            userRepository.Update(user);

            await userRepository.SaveAsync();

            return ResetPasswordResult.Success;

        }
    }
}
