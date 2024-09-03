using Eshop.Domain.Models.User;
using Eshop.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByMobileAsync(string mobile);

        Task<EditUserProfileViewModel> GetProfileForEditAsync(int UserId);

        Task<EditUserProfileResult> UpdateUserProfileAsync(EditUserProfileViewModel model);
    }
}
