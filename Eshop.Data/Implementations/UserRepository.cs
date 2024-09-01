using Eshop.Data.Context;
using Eshop.Domain.Interfaces;
using Eshop.Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Implementations
{
    public class UserRepository (EshopDbContext context) : IUserRepository
    {
        
    public async Task<bool> ExistMobileAsync(string mobile)
    {
        return await context.Users.AnyAsync(u => u.Mobile == mobile);
    }

        public async Task InsertAsync(User user)
        {
            await context.Users.AddAsync(user);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByMobileAndPasswordAsync(string mobile, string password)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Mobile == mobile && u.Password == password);
        }

        public async Task<User?> GetUserByMobileAsync(string mobile)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Mobile == mobile);
        }

        public async Task<User?> GetByMobileAndConfirmCode(string mobile, string confirmCode)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Mobile == mobile && u.ConfirmCode == confirmCode);
        }

        public void Update(User user)
        {
            context.Users.Update(user);
        }
    }
}
