using CafeManager3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Windows;
using System.Reflection.Metadata;
using Microsoft.Data.Sqlite;
using System.IO;
using Castle.Components.DictionaryAdapter.Xml;
using System.Net;
using System.Runtime.InteropServices;
using System.Linq;

namespace CafeManager3.ViewModel
{
    public class MenuItemsContext:DbContext
    {
        private string path;
        public DbSet<MenuItem> menuItem { get; set; }
        public MenuItemsContext(string path)
        {
            this.path = path;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={path}");
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
                items = this.menuItem.Where(item => item.Title == typeName).ToList();
            }

            return items;
        }

    }
}
