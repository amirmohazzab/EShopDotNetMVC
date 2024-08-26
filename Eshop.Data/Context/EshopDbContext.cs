using Eshop.Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Context
{
    public class EshopDbContext : DbContext
    {

        #region Constructor

        public EshopDbContext(DbContextOptions<EshopDbContext> options) : base(options)
        {

        }
        #endregion

        #region DbSet 

        #region User

        public DbSet<User> Users { get; set; }

        #endregion

        #endregion
    }
}
