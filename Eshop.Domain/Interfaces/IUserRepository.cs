using Eshop.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistMobileAsync(string Mobile);

        Task InsertAsync(User user);

        Task SaveAsync();

        Task<User?> GetUserByMobileAndPasswordAsync(string mobile, string password);

        Task<User?> GetUserByMobileAsync(string mobile);

        Task<User?> GetByMobileAndConfirmCode(string mobile, string confirmCode);

        void Update(User user);
    }
}
