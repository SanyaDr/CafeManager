using CafeManager3.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace CafeManager3.ViewModel
{
    public class MenuItemsContext : DbContext
    {
        private string path;
        public DbSet<MenuItem> menuItem { get; set; }
        public MenuItemsContext(string path, FoodTypesContext foodTypesContext)
        {
            this.path = path;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={path}; ");
            optionsBuilder.UseLazyLoadingProxies();
        }

        public List<MenuItem> ShowByType(string typeName)
        {
            List<MenuItem> items = null!;


            if (typeName == null || typeName == string.Empty)
            {
                items = this.menuItem.AsQueryable().ToList();
            }
            else
            {
                string query = $"SELECT I.iD, I.Title, I.[Description], I.FoodTypeId, I.Price, I.Ingredients, I.[Weight], I.Calories, I.Icon FROM FoodTypes AS F, MenuItem AS I WHERE F.Title = {typeName} AND F.id = I.FoodTypeId";
                items = new List<MenuItem>();
                using (SQLiteConnection conn = new SQLiteConnection(($"Data Source={path};")))
                {
                    conn.Open();
                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                MenuItem item = new MenuItem();
                                item = new MenuItem();
                                
                                item.Id = reader.GetInt32(0);
                                item.Title = reader.GetString(1);
                                item.Description = reader.GetString(2);
                                item.FoodTypeId = reader.GetInt32(3);
                                item.Price = reader.GetDouble(4);
                                item.Ingredients = reader.GetString(5);
                                item.Weight = reader.GetInt32(6);
                                item.Calories = reader.GetInt32(7);
                                item.Icon = (byte[])reader.GetValue(8);
                                items.Add(item);
                            }
                        }
                    }
                }
            }
            return items;
        }
    }
}
