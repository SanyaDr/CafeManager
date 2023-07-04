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
    /// Загрузочный экран
    /// </summary>
    public partial class LoadingWindow : Window
    {
        /// <summary>
        /// Таймер для отсчета времени загрузки. (имитирует загрузку приложения - по умолчанию 3 секунды)
        /// </summary>
        DispatcherTimer timer = new DispatcherTimer();

        /// <summary>
        /// Загрузочный экран
        /// </summary>
        /// <param name="path">Путь к базе данных</param>
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
        }

        /// <summary>
        ///Запускает таймер на 3 секунд для анимации загрузки
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += TimerTick;
            timer.Start();
        }

        /// <summary>
        ///Срабатывает после конца таймера
        /// </summary>
        private void TimerTick(object sender, EventArgs e)
        {
            timer.Stop();
            Close();
        }
    }
}
