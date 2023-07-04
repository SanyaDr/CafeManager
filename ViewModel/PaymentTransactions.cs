using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CafeManager3.ViewModel
{
    public class PaymentTransactions
    {
        public PaymentTransactions() { }
        public bool Pay()
        {
            MessageBox.Show("Имитиция оплаты заказа");
            MessageBox.Show("Оплата прошла успешно!");
            return true;
        }
    }
}
