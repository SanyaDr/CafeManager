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
    /// Логика взаимодействия для MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        Account curUser;
        public MainMenuWindow(Account curUser)
        {
            InitializeComponent();
            if (curUser != null)
            {
                this.curUser = curUser;
                GetName();
            }
            else
            {
                MessageBox.Show("Ошибка! Активный пользователь не считан!");
                Close();
            }

        }

        public void GetName()
        {
            Label_UserName.Content = $"Приветствуем, {curUser.Name}!";
        }

        private void Button_ExitFromAccount_Click(object sender, RoutedEventArgs e)
        {
            curUser = null;
            Close();
        }
    }
}
