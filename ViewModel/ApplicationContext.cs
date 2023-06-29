using CafeManager3.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeManager3.ViewModel
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Account> users { set; get; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*
                приложение будет использовать базу данных 
                с именем "Users.db". 
                Причем не важно, что сейчас такой 
                базы данных не существует - она будет 
                создана автоматически.
            */
            optionsBuilder.UseSqlite("Data Source=Users.db");
        }
    }
}
