using CafeManager3.Models;
using CafeManager3.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media.Imaging;
using System;

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

            LoadTypes();

            Loaded += MainMenuWindow_Loaded;
        }

        private void MainMenuWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MyButton_Click(Button_ShowAllTypes, new RoutedEventArgs());
        }

        public void LoadTypes()
        {
            foreach (var type in allTypes)
            {
                Button myButton = new Button();
                myButton.Style = (Style)FindResource("Style_ButtonFoodType");

                Grid myGrid = new Grid();
                myGrid.Height = 73;
                myGrid.Width = 175;

                ColumnDefinition column1 = new ColumnDefinition();
                column1.Width = new GridLength(1, GridUnitType.Star);
                ColumnDefinition column2 = new ColumnDefinition();
                column2.Width = new GridLength(2.5, GridUnitType.Star);
                myGrid.ColumnDefinitions.Add(column1);
                myGrid.ColumnDefinitions.Add(column2);

                System.Windows.Controls.Image myImage = new System.Windows.Controls.Image();
                myImage.Margin = new Thickness(5, 0, -5, 0);
                Grid.SetColumn(myImage, 0);

                TextBlock myTextBlock = new TextBlock();

                myTextBlock.Style = (Style)FindResource("Style_LabelsFoodType");
                Grid.SetColumn(myTextBlock, 1);
                Grid.SetColumnSpan(myTextBlock, 2);

                myGrid.Children.Add(myImage);
                myGrid.Children.Add(myTextBlock);

                myButton.Content = myGrid;
                myButton.Click += MyButton_Click;

                /* ТУТА */
                //Получение Blob, иконки из базы данных
                if (type.Icon != null)
                {
                    var bitmapImage = new BitmapImage();
                    using (var memoryStream = new MemoryStream(type.Icon))
                    {
                        bitmapImage.BeginInit();
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.StreamSource = memoryStream;
                        bitmapImage.EndInit();
                    }

                    myImage.Source = bitmapImage;
                }
                /* ТУТА */
                //Получение названия группы еды
                myTextBlock.Text = type.Title;
                FoodTypesStackPanel.Children.Add(myButton);
            }          
        }

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            Button clButt = (Button)sender;
            string text;
            try
            {
                text = ((TextBlock)((Grid)clButt.Content).Children[1]).Text;
            }
            catch
            {
                text = string.Empty;
                //Сразу вывод всех


                /*
                try
                {
                    TextBlock t = (TextBlock)clButt.Content;
                    text = t.Text;
                }
                catch (Exception ex)
                {
                    text = string.Empty;
                    MessageBox.Show(ex.Message);
                }
                */
            }
            MessageBox.Show(text, text);
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
