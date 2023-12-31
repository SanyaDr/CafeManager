﻿using CafeManager3.Models;
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
    /// <summary>
    /// Окно входа в аккаунт
    /// </summary>
    public partial class LoginAccountWindow : Window
    {
        /// <summary>
        /// Контекст аккаунтов для бд
        /// </summary>
        private readonly AccountContext accountContext;
        /// <summary>
        /// Путь к БД
        /// </summary>
        string path = string.Empty;

        /// <summary>
        /// Загрузочный экран
        /// </summary>
        LoadingWindow loading;
        /// <summary>
        /// Главное меню приложения
        /// </summary>
        MainMenuWindow main;

        public LoginAccountWindow()
        {
            InitializeComponent();

            path = @"Media\CafeSet.db";

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

            try
            {
                accountContext = new AccountContext(path);
                accountContext.Account.Load();
            }
            catch (Exception ex)
            {
                var res =MessageBox.Show("Ошибка базы данных!\nВозможно она не найдена, проверьте её наличие в папке с фалом приложения и перезапустите приложение" +
                    "\nВыбрать базу вручную?", "База данных не найдена", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (res == MessageBoxResult.Yes)
                {
                    try
                    {
                        path = null;
                        loading = new LoadingWindow(ref path);
                        accountContext = new AccountContext(path);
                        accountContext.Account.Load();
                    }
                    catch (Exception ex2)
                    {
                        MessageBox.Show("Все равно не удаётся открыть файл. Проверьте базу данных и её наличие\nЗатем пожалуйста перезапустите приложение\n\nТекст ошибки:\n" + ex2.Message, "Проблема загрузки базы данных",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        Application.Current.Shutdown();
                    }
                }
                else
                {
                    Application.Current.Shutdown();
                }
            }
            Closing += LoginAccountWindow_Closing;
        }


        private void LoginAccountWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            //var res = MessageBox.Show("Вы точно хотите выйти?", "Закрытие приложения",
            //    MessageBoxButton.YesNo, MessageBoxImage.Question);
            //if (res == MessageBoxResult.No)
            //{
            //    e.Cancel = true;
            //}
            //else
            //{

                MessageBox.Show("Работу выполнил:\n\tДровосеков Александр Александрович\n\tВПД111", "Автор",
        MessageBoxButton.OK, MessageBoxImage.Asterisk);


            //    e.Cancel = false;
            //}
        }

        /// <summary>
        /// Обработка нажатия на кнопку входа в аккаунт.
        /// Ищет совпадения по номеру телефона, если есть совпадение то вход успешен, иначе предлагает зарегистрироваться
        /// </summary>
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

        /// <summary>
        /// Выход из приложения
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Войти как гость. Гостевой режим, без сохранения истории и скучной авторизации
        /// </summary>
        private void LoginAsGuest(object sender, RoutedEventArgs e)
        {
            Account guest =  new Account();
            guest.Name = "Гость";
            guest.mobileNumber = "+71234567890";
            guest.Id = -99;
            main = new MainMenuWindow(path, guest);
            Hide();
            main.ShowDialog();
            Show();
        }
    }
}
