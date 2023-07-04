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
        /// <summary>
        /// Путь к БД
        /// </summary>
        private string path;
        public DbSet<FoodTypes> foodTypes {  get; set; }
        /// <summary>
        /// Контекст категорий еды
        /// </summary>
        /// <param name="path">Путь к БД</param>
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
