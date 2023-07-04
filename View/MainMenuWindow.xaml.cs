using CafeManager3.Models;
using CafeManager3.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace CafeManager3.View
{
    /// <summary>
    /// Главное меню прложения. Откуда мы делаем заказы, выбираем товары и многое другое)
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        /// <summary>
        /// Контекст БД категорий еды
        /// </summary>
        public readonly FoodTypesContext foodContext = null!;
        /// <summary>
        /// Контекст БД товаров
        /// </summary>
        public readonly MenuItemsContext itemsContext = null!;
        /// <summary>
        /// Наша корзина
        /// </summary>
        public Cart cart;
        /// <summary>
        /// Текущий, активный пользователь
        /// </summary>
        Account curUser = null!;
        /// <summary>
        /// Список всех загруженных категорий еды
        /// </summary>
        public List<FoodTypes> allTypes = new List<FoodTypes>();
        /// <summary>
        /// Путь к БД
        /// </summary>
        private string path;
        /// <summary>
        /// Главное меню приложения
        /// </summary>
        /// <param name="path">Путь к БД</param>
        /// <param name="curUser">Объект текущего пользователя</param>
        public MainMenuWindow(string path, Account curUser)
        {
            InitializeComponent();
            this.path = path;
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

            itemsContext = new MenuItemsContext(path, foodContext);
            itemsContext.menuItem.Load();

            LoadTypes();

            Loaded += MainMenuWindow_Loaded;

            cart = new Cart();
        }

        /// <summary>
        /// Выполняет стартовую загрузку и вывод товаров на экран
        /// </summary>
        private void MainMenuWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ButtonTypes_Click(Button_ShowAllTypes, new RoutedEventArgs());
        }

        /// <summary>
        /// Загрузка категорий
        /// </summary>
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
                myButton.Click += ButtonTypes_Click;

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
        /// <summary>
        /// Загрузка товаров выбранных в категории
        /// </summary>
        /// <param name="items">Выводит на экран список товаров выбранной категории</param>
        public void LoadItems(List<CafeManager3.Models.MenuItem> items)
        {
            WrapPanel wrapPanel = new WrapPanel();
            wrapPanel.Orientation = Orientation.Horizontal;
            wrapPanel.Name = "myWrapPanel";

            foreach (var item in items)
            {
                Button myButton = new Button();
                myButton.Width = 150;
                myButton.Style = (Style)this.FindResource("Style_ButtonItem");

                Grid buttonGrid = new Grid();
                buttonGrid.Height = 180;
                buttonGrid.Width = 150;

                RowDefinition rowDefinition1 = new RowDefinition();
                rowDefinition1.Height = new GridLength(2, GridUnitType.Star);
                RowDefinition rowDefinition2 = new RowDefinition();
                rowDefinition2.Height = new GridLength(1, GridUnitType.Star);

                buttonGrid.RowDefinitions.Add(rowDefinition1);
                buttonGrid.RowDefinitions.Add(rowDefinition2);

                Image image = new Image();
                image.Margin = new Thickness(13);
                Grid.SetRow(image, 0);

                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Vertical;
                Grid.SetRow(stackPanel, 1);

                TextBlock textName = new TextBlock();
                textName.Style = (Style)this.FindResource("Style_LabelsItems");

                TextBlock textPrice = new TextBlock();
                textPrice.Style = (Style)this.FindResource("Style_LabelPrice");

                textName.Text = item.Title;
                textPrice.Text = item.Price.ToString() + " руб";
                if (item.Icon != null)
                {
                    var bitmapImage = new BitmapImage();
                    using (var memoryStream = new MemoryStream(item.Icon))
                    {
                        bitmapImage.BeginInit();
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.StreamSource = memoryStream;
                        bitmapImage.EndInit();
                    }
                    image.Source = bitmapImage;
                }

                stackPanel.Children.Add(textName);
                stackPanel.Children.Add(textPrice);

                buttonGrid.Children.Add(image);
                buttonGrid.Children.Add(stackPanel);

                myButton.Content = buttonGrid;

                myButton.Click += ButtonItems_Click;
                wrapPanel.Children.Add(myButton);

            }
            while (StackPanel_AllFood.Children.Count > 2)
            {
                StackPanel_AllFood.Children.RemoveAt(2);
            }
            StackPanel_AllFood.Children.Add(wrapPanel);
        }

        /// <summary>
        /// Обработка нажатия на одну из категорий
        /// </summary>
        private void ButtonItems_Click(object sender, RoutedEventArgs e)
        {
            Button clButt = (Button)sender;
            string nameItem = string.Empty;
            try
            {
                nameItem = ((TextBlock)((StackPanel)((Grid)clButt.Content).Children[1]).Children[0]).Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ShowItemInfo info = new ShowItemInfo(nameItem, itemsContext.ShowByType(string.Empty), cart);
            info.ShowDialog();
        }

        /// <summary>
        /// Обработка нажатия на один из товаров
        /// </summary>
        private void ButtonTypes_Click(object sender, RoutedEventArgs e)
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
            }
            LoadItems(itemsContext.ShowByType(text));

            //MessageBox.Show(text, text);
        }

        /// <summary>
        /// Вывод имени пользователя на экран
        /// </summary>
        public void GetName()
        {
            Label_UserName.Content = $"Приветствуем, {curUser.Name}!";
        }

        /// <summary>
        /// Выход из аккаунта. Вызывается окно входа в аккаунт
        /// </summary>
        private void Button_ExitFromAccount_Click(object sender, RoutedEventArgs e)
        {
            curUser = null;
            Close();
        }

        /// <summary>
        /// Обработка кнопки оплаты
        /// </summary>
        private void Button_Cheque_Click(object sender, RoutedEventArgs e)
        {
            PaymentWindow payWindow = new PaymentWindow(ref cart, path, curUser);
            Hide();
            payWindow.ShowDialog();
            ShowDialog();
        }

        private void Button_Author_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Работу выполнил:\n\tДровосеков Александр Александрович\n\tВПД111\n", "Автор",
                MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}
