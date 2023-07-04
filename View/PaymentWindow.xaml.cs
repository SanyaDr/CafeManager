using CafeManager3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CafeManager3.View
{
    /// <summary>
    /// Логика взаимодействия для PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        Cart cart;
        public PaymentWindow(Cart cart)
        {
            InitializeComponent();

            this.cart = cart;

            Loaded += PaymentWindow_Loaded;
        }

        private Border DrowItem()

        private void PaymentWindow_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in cart.GetCart())
            {
                StackPanel_Cheque.Children.Add(item);

            }
        }        
    }
}
