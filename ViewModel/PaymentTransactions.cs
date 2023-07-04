using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CafeManager3.ViewModel
{
    /// <summary>
    /// Имитация решения оплаты покупки
    /// </summary>
    public class PaymentTransactions
    {
        public PaymentTransactions() { }
        /// <summary>
        /// Оплатить покупку.
        /// </summary>
        /// <returns>Если транзакция успешна то возвращает true, иначе false</returns>
        public bool Pay()
        {
            MessageBox.Show("Имитиция оплаты заказа");
            MessageBox.Show("Оплата прошла успешно!");
            return true;
        }
    }
}
