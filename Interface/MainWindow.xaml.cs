using Interface.Windows;
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
            Logic.DataBaseLogic.IsDataBasesExist();
        }

        private void OpenAuthorizationWindow()
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            Close();
        }
    }
}