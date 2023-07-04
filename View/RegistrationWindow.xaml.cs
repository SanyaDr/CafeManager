using CafeManager3.ViewModel;
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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        AccountContext accountContext;
        public RegistrationWindow(string input, AccountContext context)
        {
            InitializeComponent();
            Input_TelefonNumber.Text = input;
            accountContext = context;
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {
            if (Input_Name.Text != string.Empty && Input_TelefonNumber.Text != string.Empty)
            {
                accountContext.CreateNewAccount(Input_Name.Text, Input_TelefonNumber.Text);
                Close();
            }
            else
            {
                MessageBox.Show("Нельзя оставить поля пустыми, им одиноко!", "Одно из полей пустое",
                    MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }
    }
}
