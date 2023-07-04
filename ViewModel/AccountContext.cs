using CafeManager3.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeManager3.ViewModel
{
    /// <summary>
    /// Контекст аккаунтов
    /// </summary>
    public class AccountContext: DbContext
    {
        /// <summary>
        /// Путь к БД
        /// </summary>
        private string path;
        public DbSet<Account> Account { get; set; }
        /// <summary>
        /// Контекст аккаунтов
        /// </summary>
        /// <param name="path">Путь к БД</param>
        public AccountContext(string path)
        {
            this.path = path;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={path}");
            optionsBuilder.UseLazyLoadingProxies();
        }

        /// <summary>
        /// Создание нового аккаута
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <param name="number">Номер телефона</param>
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
