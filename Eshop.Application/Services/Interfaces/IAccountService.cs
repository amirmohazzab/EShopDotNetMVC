using Eshop.Domain.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Eshop.Domain.ViewModels.Account.LoginViewModel;
using static Eshop.Domain.ViewModels.Account.RegisterViewModel;

namespace Eshop.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterResult> RegisterAsync(RegisterViewModel model);

        Task<LoginResult> LoginAsync(LoginViewModel model);
    }
}
