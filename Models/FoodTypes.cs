using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CafeManager3.Models
{
    public class FoodTypes
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte[]? Icon { get; set; }
    }
}
