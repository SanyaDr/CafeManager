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
    /// Логика взаимодействия для ShowItemInfo.xaml
    /// </summary>
    public partial class ShowItemInfo : Window
    {
        private CafeManager3.Models.MenuItem selected;
        Image itemImage;
        Cart cart;
        public ShowItemInfo(string name, List<CafeManager3.Models.MenuItem> items, Cart cart)
        {
            InitializeComponent();
            this.cart = cart;
            if (name != null && name != string.Empty)
            {
                selected = items.SingleOrDefault(i => i.Title == name);
            }
            else
            {
                Close();
                return;
            }

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

        private void Button_ToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cart.AddToCart(selected, int.Parse(TextBlock_Count.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Consist_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Info.Text = "Состав: " + selected.Ingredients;
        }

        private void Button_Description_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Info.Text = selected.Description;
        }

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

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            int.TryParse(TextBlock_Count.Text, out count);
            count++;
            TextBlock_Count.Text = count.ToString();
        }

        public Image GetImage()
        {
            return null;
        }
    }
}
