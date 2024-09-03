using Eshop.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Application.Extension
{
    public static class UserExtensions
    {
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            string? userId = claimsPrincipal.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrWhiteSpace(userId))
                return int.Parse(userId);

            else return default;
        }
    }
}
