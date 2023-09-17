﻿using Interface.Windows;
using System.Windows;

namespace Interface
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            OpenAuthorizationWindow();
            StartDataBase();
        }

        private void OpenAuthorizationWindow()
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            Close();
        }

        private void StartDataBase()
        {
            Logic.DataBaseLogic.StartDataBase();
        }
    }
}