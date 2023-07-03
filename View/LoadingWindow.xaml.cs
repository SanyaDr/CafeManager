using Microsoft.EntityFrameworkCore.Sqlite.Query.Internal;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CafeManager3.View
{
    /// <summary>
    /// Логика взаимодействия для LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        public LoadingWindow(ref string path)
        {
            InitializeComponent();
            if (path == null || path == string.Empty)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = false;
                ofd.Filter = "База данных SQLite |*.db";
                ofd.ShowDialog();
                path = ofd.FileName;
            }
            
//Анимация которая вращает иконку на экране загрузки
//DoubleAnimation anim = new DoubleAnimation();
//anim.From = 0;
//anim.To = 500;
//anim.Duration = TimeSpan.FromSeconds(3);
//LoadingIcon.RenderTransform = new TranslateTransform(50, 50);
//LoadingIcon.RenderTransform.ApplyAnimationClock(RotateTransform.AngleProperty, anim);

        }

        //Запускает таймер на 3 секунд для анимации загрузки
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += TimerTick;
            timer.Start();
        }

        //Срабатывает после конца таймера
        private void TimerTick(object sender, EventArgs e)
        {
            timer.Stop();
            Close();
        }
    }
}
