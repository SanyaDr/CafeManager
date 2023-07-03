using CafeManager3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManager3.ViewModel
{
    public class MenuItemsContext:DbContext
    {
        private string path;
        public DbSet<MenuItem> items { get; set; }
        public MenuItemsContext(string path)
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
