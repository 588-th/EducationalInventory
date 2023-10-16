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

            OpenConnectingWindow();
        }

        private void OpenConnectingWindow()
        {
            ConnectingWindow connectingWindow = new ConnectingWindow();
            connectingWindow.Show();
            Close();
        }
    }
}