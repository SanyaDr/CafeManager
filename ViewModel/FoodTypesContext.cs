using CafeManager3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManager3.ViewModel
{
    ///<summary>
    ///Контекст типов еды
    ///</summary>
    public class FoodTypesContext:DbContext
    {
        private string path;
        public DbSet<FoodTypes> foodTypes {  get; set; }
        public FoodTypesContext(string path)
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
