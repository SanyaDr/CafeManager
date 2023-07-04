using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CafeManager3.Models;

namespace CafeManager3.Models
{
    /// <summary>
    /// Класс представляющий собой корзину для покупок
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Полный список покупок, состоит из предметов магазина
        /// </summary>
        protected List<MenuItem> items = new List<MenuItem>();

        /// <summary>
        /// Добавление в коозину
        /// </summary>
        /// <param name="item"> Товар из каталога </param>
        /// <param name="count"></param>
        public void AddToCart(MenuItem item, int count)
        {
            for(int i = 0; i < count; i++)
            {
                items.Add(item);
            }
        }
        /// <summary>
        /// Удаление из корзины по индексу
        /// </summary>
        /// <param name="index"></param>
        public void RemoveFromCart(int index)
        {
            items.RemoveAt(index);
        }
        /// <summary>
        /// Получение списка покупок
        /// </summary>
        /// <returns></returns>
        public List<MenuItem> GetCart()
        {
            return items;
        }
        /// <summary>
        /// Получение полной стоимости заказа
        /// </summary>
        public int GetFullAmount()
        {
            int total = 0;
            foreach (var item in items)
            {
                total += item.Price;
            }
            return total;
        }
        /// <summary>
        /// Получение количества товаров в корзине
        /// </summary>
        /// <returns></returns>
        public int GetCountInCart()
        {
            return items.Count;
        }
        //Очистка корзины
        public void Clear()
        {
            items.Clear();
        }
    }
}
