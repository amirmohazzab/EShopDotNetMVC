using Eshop.Application.Services.Interfaces;
using Eshop.Domain.Interfaces;
using Eshop.Domain.Models.User;
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
    }
}
