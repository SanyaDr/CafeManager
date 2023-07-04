using CafeManager3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;

namespace CafeManager3.ViewModel
{
    public class HistoryContext:DbContext
    {
        private string path;
        private Cart cart;
        private Account account;
        public DbSet<History> History { get; set; }

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
