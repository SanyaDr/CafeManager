using CafeManager3.Models;
using CafeManager3.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.CodeDom.Compiler;
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
    public partial class LoginAccountWindow : Window
    {
        private readonly AccountContext accountContext;
        string path = string.Empty;

        LoadingWindow loading;
        MainMenuWindow main;
        public LoginAccountWindow()
        {
            InitializeComponent();

            loading = new LoadingWindow(ref path);
            while(path == null || path.Length == 0 || !path.EndsWith(".db"))
            {
                var res = MessageBox.Show("Вы не указали путь к базе.\nХотите закрыть программу?", "Закрыть программу?",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if(res == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }

                loading = new LoadingWindow(ref path);
            }
            loading.ShowDialog();
            loading.Close();
            
            accountContext = new AccountContext(path);
            accountContext.Account.Load();
        }

        /// <summary>
        /// Возвращает true если номера совпадают (с учетом начала номера на +7 и 8)
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>

        private void Button_LogIn_Click(object sender, RoutedEventArgs e)
        {
            var inp = TextBox_Input.Text;
            Account confirm = null;
            bool found = false;
            List<Account> users = accountContext.Account.AsQueryable().ToList();

            foreach(var user in users)
            {
                if(user.mobileNumber == inp)
                {
                    found = true;
                    confirm = user;
                    break;
                }
            }
            if(found)
            {
                main = new MainMenuWindow(path, confirm);
                Hide();
                main.ShowDialog();
                Show();
            }
            else
            {
                var AskNewAcc = MessageBox.Show("Логин не найден. Проверьте ввод или зарегистрируйтесь\n\tЗарегистрировать аккаунт?","Аккаунт не найден",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(AskNewAcc == MessageBoxResult.Yes)
                {
                    RegistrationWindow regNew = new RegistrationWindow(inp, accountContext);
                    var res = regNew.ShowDialog();
                    accountContext.Account.Load();

                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoginAsGuest(object sender, RoutedEventArgs e)
        {
            Account guest =  new Account();
            guest.Name = "Гость";
            guest.mobileNumber = "+71234567890";
            main = new MainMenuWindow(path, guest);
            Close();
            main.Show();
        }
    }
}
