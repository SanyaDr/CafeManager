using CafeManager3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManager3.ViewModel
{
    /// <summary>
    /// Контекст аккаунтов
    /// </summary>
    public class AccountContext: DbContext
    {
        private string path;
        public DbSet<Account> Account { get; set; }
        public AccountContext(string path)
        {
            this.path = path;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={path}");
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
