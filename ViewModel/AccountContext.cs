using CafeManager3.Models;
using Microsoft.EntityFrameworkCore;

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

        public void CreateNewAccount(string name, string number)
        {
            Account newUser = new Account();
            newUser.mobileNumber = number;
            newUser.Name = name;
            Account.Add(newUser);
            SaveChanges();
        }

    }
}
