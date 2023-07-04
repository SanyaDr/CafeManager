using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeManager3.Models;

namespace CafeManager3.Models
{
    public class Cart
    {
        protected List<MenuItem> items = new List<MenuItem>();

        public void AddToCart(MenuItem item, int count)
        {
            for(int i = 0; i < count; i++)
            {
                items.Add(item);
            }
        }
        public void RemoveFromCart(int index)
        {
            items.RemoveAt(index);
        }
        public List<MenuItem> GetCart()
        {
            return items;
        }
    }
}
