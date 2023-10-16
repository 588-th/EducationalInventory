using System.Windows;

namespace Interface.Windows
{
    /// <summary>
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow()
        {
            InitializeComponent();
            Closing += (_, __) => Application.Current.Shutdown();
        }
    }
}