using CafeManager3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;

namespace CafeManager3.ViewModel
{
    /// <summary>
    /// Контекст для истории покупок
    /// </summary>
    public class HistoryContext:DbContext
    {
        /// <summary>
        /// Путь к БД
        /// </summary>
        private string path;
        /// <summary>
        /// Наша корзина
        /// </summary>
        private Cart cart;
        /// <summary>
        /// Аккаунт
        /// </summary>
        private Account account;
        public DbSet<History> History { get; set; }

        /// <summary>
        /// Контекст для истории покупок
        /// </summary>
        /// <param name="path">Путь к БД</param>
        /// <param name="cart">Корзина</param>
        /// <param name="account">Текущий пользователь</param>
        public HistoryContext(string path, Cart cart, Account account)
        {
            this.path = path;
            this.cart = cart;
            this.account = account;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={path}");
            optionsBuilder.UseLazyLoadingProxies();
        }

        /// <summary>
        /// Добавление истории в БД
        /// </summary>
        public void AddNewHistory()
        {
            //Проверка на гостя
            if (account.Id > 0)
            {
                History newHistory = new History();
                newHistory.Amount = cart.GetFullAmount();
                newHistory.Date = DateTime.Now.ToString();
                newHistory.AccountId = account.Id;

                History.Add(newHistory);
            }
        }
    }
}
