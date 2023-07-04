using CafeManager3.Models;
using CafeManager3.ViewModel;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CafeManager3.View
{
    /// <summary>
    /// Окно оплаты выбранных товаров
    /// </summary>
    public partial class PaymentWindow : Window
    {
        /// <summary>
        /// Наша корзина
        /// </summary>
        private Cart cart;
        /// <summary>
        /// Текущий пользователь
        /// </summary>
        private Account account;
        /// <summary>
        /// Контекст БД истории покупок
        /// </summary>
        public HistoryContext historyContext;
        /// <summary>
        /// Путь к БД
        /// </summary>
        private string path;
        /// <summary>
        /// Окно оплаты выбранных товаров
        /// </summary>
        /// <param name="cart">Класс корзины для покупок</param>
        /// <param name="dataBasePath">Путь к БД</param>
        /// <param name="currentUser">Текущий пользователь</param>
        public PaymentWindow(ref Cart cart, string dataBasePath, Account currentUser)
        {
            InitializeComponent();
            path = dataBasePath;
            this.cart = cart;
            account = currentUser;
            Loaded += PaymentWindow_Loaded;
            Button_Pay.Click += Button_Pay_Click;
        }

        /// <summary>
        /// Обработка кнопки оплаты
        /// </summary>
        private void Button_Pay_Click(object sender, RoutedEventArgs e)
        {
            PaymentTransactions payment = new PaymentTransactions();
            if(payment.Pay())
            {
                //Оплата успешна
                Close();
                historyContext = new HistoryContext(path, cart, account);
                historyContext.AddNewHistory();
                cart.Clear();
                historyContext.SaveChanges();
            }
        }

        /// <summary>
        /// Проверяет количество товаров в корзине.
        /// Если корзина пуста, то закрывает окно оплаты
        /// </summary>
        public void CheckCountItemsInCart()
        {
            if (cart.GetCountInCart() == 0)
            {
                MessageBox.Show("Корзина пуста!");
                Close();
            }
        }

        /// <summary>
        /// Вывод на экран элемента в чеке
        /// </summary>
        /// <param name="item">Выбранный товар</param>
        /// <param name="num">Его Идентификатор (просто порядковый номер в чеке)</param>
        /// <returns></returns>
        private Border DrowItem(CafeManager3.Models.MenuItem item, int num)  
        {
            Border border = new Border();
            border.Margin = new Thickness(5);

            StackPanel stackPanel = new StackPanel();
            stackPanel.Height = 55;
            stackPanel.Orientation = Orientation.Horizontal;

            TextBlock TextBlock_iD = new TextBlock();
            TextBlock_iD.Text = num.ToString();
            TextBlock_iD.VerticalAlignment = VerticalAlignment.Center;
            TextBlock_iD.Margin = new Thickness(0, 0, 10, 0);

            Image image_myImage = new Image();
            image_myImage.Margin = new Thickness(10, 0, 0, 0);
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
                image_myImage.Source = bitmapImage;
            }

            TextBlock textBlock_ItemName = new TextBlock();
            textBlock_ItemName.Text = item.Title;
            textBlock_ItemName.VerticalAlignment = VerticalAlignment.Center;
            textBlock_ItemName.Margin = new Thickness(10, 0, 0, 0);
            textBlock_ItemName.FontSize = 25;
            textBlock_ItemName.FontWeight = FontWeights.SemiBold;

            TextBlock textBlock_Price = new TextBlock();
            textBlock_Price.Text = $"{item.Price} руб";
            textBlock_Price.Margin = new Thickness(10, 0, 10, 0);
            textBlock_Price.VerticalAlignment = VerticalAlignment.Center;
            textBlock_Price.FontSize = 20;

            Button button_DelButton = new Button();
            button_DelButton.Name = "b" + num.ToString();
            button_DelButton.Content = "X";
            button_DelButton.Click += DelButton_Click;

            ResourceDictionary dictionary = new ResourceDictionary();
            dictionary.Source = new Uri(@"\Media\StylesPayment.xaml", UriKind.Relative);
            Style style = (Style)TryFindResource("Style_DeleteFromCart_Button");
            button_DelButton.Style = style;

            stackPanel.Children.Add(TextBlock_iD);
            stackPanel.Children.Add(image_myImage);
            stackPanel.Children.Add(textBlock_ItemName);
            stackPanel.Children.Add(textBlock_Price);
            stackPanel.Children.Add(button_DelButton);
            

            border.Child = stackPanel;

            //StackPanel_Cheque.Children.Add(border);
            return border;
        }

        /// <summary>
        /// Обработка кнопки удаления товара из корзина и чека
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            Button clButt = (Button)sender;
            int ind;
            string got = clButt.Name;
            if (int.TryParse(got[1].ToString(), out ind))
            {
                cart.RemoveFromCart(ind);
            }
            StackPanel_Cheque.Children.Clear();
            PaymentWindow_Loaded(sender, e);
        }
        
        /// <summary>
        /// Отрисовка чека после загрузки окна
        /// </summary>
        private void PaymentWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CheckCountItemsInCart();
            Button_Pay.Content = "Оплатить / " + cart.GetFullAmount().ToString() + " рублей";
            int i = 0;
            foreach (var item in cart.GetCart())
            {
                StackPanel_Cheque.Children.Add(DrowItem(item, i));
                i++;
            }
        }        
    }
}
