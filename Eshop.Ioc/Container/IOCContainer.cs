using Eshop.Application.Services.Implamentations;
using Eshop.Application.Services.Interfaces;
using Eshop.Data.Implementations;
using Eshop.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Ioc.Container
{
    public static class IOCContainer
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            #region Services

                services.AddScoped<IUserService, UserService>();

            #endregion

            #region Repositories

                services.AddScoped<IUserRepository, UserRepository>();

            #endregion
        }
    }
}
