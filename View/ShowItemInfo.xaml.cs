using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using CafeManager3.Models;

namespace CafeManager3.View
{
    /// <summary>
    /// Окно показа информации по товару. Вызывается при клике на товар с главного окна
    /// </summary>
    public partial class ShowItemInfo : Window
    {
        /// <summary>
        /// Выбранный товар
        /// </summary>
        private CafeManager3.Models.MenuItem selected;

        /// <summary>
        /// Наша корзина
        /// </summary>
        Cart cart;
        /// <summary>
        /// Окно показа информации по товару. Вызывается при клике на товар с главного окна
        /// </summary>
        /// <param name="Title">Название товара</param>
        /// <param name="items">Список всех товаров. В дальнейшем идет поиск по названию для загрузки дополнительной информации</param>
        /// <param name="cart">Корзина покупок</param>
        public ShowItemInfo(string Title, List<CafeManager3.Models.MenuItem> items, Cart cart)
        {
            InitializeComponent();
            this.cart = cart;
            if (Title != null && Title != string.Empty)
            {
                selected = items.SingleOrDefault(i => i.Title == Title);
            }
            else
            {
                Close();
                return;
            }

            TextBlock_ItemName.Text = selected.Title;

            if (selected.Icon != null)
            {
                var bitmapImage = new BitmapImage();
                using (var memoryStream = new MemoryStream(selected.Icon))
                {
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.EndInit();
                }
                myImage.Source = bitmapImage;
            }
            Button_Add.Click += Button_Add_Click;
            Button_Remove.Click += Button_Remove_Click;
            Button_Description.Click += Button_Description_Click;
            Button_Consist.Click += Button_Consist_Click;
            Button_ToCart.Click += Button_ToCart_Click;

            Button_Description_Click(null, null);
        }

        /// <summary>
        /// Кнопка добавления в корзину
        /// </summary>
        private void Button_ToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cart.AddToCart(selected, int.Parse(TextBlock_Count.Text));
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Кнопка вывод состава товара
        /// </summary>
        private void Button_Consist_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Info.Text = "Состав: " + selected.Ingredients;
        }
        /// <summary>
        /// Кнопка вывода описания товара
        /// </summary>
        private void Button_Description_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Info.Text = selected.Description;
        }
        /// <summary>
        /// Кнопка уменьшения количества выбранного товара
        /// </summary>
        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            int.TryParse(TextBlock_Count.Text, out count);
            count--;
            if (count < 0)
            {
                count = 0;
            }
            TextBlock_Count.Text = count.ToString();
        }
        /// <summary>
        /// Кнопка добавления еще одного выбранного товара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            int.TryParse(TextBlock_Count.Text, out count);
            count++;
            TextBlock_Count.Text = count.ToString();
        }
    }
}
