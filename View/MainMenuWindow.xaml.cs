using CafeManager3.Models;
using CafeManager3.ViewModel;
using Microsoft.EntityFrameworkCore;
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
        public readonly FoodTypesContext foodContext = null!;
        Account curUser = null!;
        public List<FoodTypes> allTypes = new List<FoodTypes>();
        public MainMenuWindow(string path, Account curUser)
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
            foodContext = new FoodTypesContext(path);
            foodContext.foodTypes.Load();
            allTypes.AddRange(foodContext.foodTypes.AsQueryable()/*.ToList()*/);

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
