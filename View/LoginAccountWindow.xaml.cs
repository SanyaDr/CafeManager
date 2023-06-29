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
    /// <summary>
    /// Логика взаимодействия для LoginAccountWindow.xaml
    /// </summary>
    public partial class LoginAccountWindow : Window
    {
        LoadingWindow loading;
        public LoginAccountWindow()
        {
            InitializeComponent();

            loading = new LoadingWindow();
            //loading.ShowDialog();
            loading.Close();
        }

    }
}
