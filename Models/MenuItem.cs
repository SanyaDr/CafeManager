using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CafeManager3.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int FoodTypeId { get; set; }
        public string Price { get; set; } = null!;
        public string Ingredients { get; set; } = null!;
        public int Weight { get; set; }
        public int Calories { get; set; }
        public Blob Icon { get; set; }
    }
}
