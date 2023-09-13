using System.Windows;

namespace Interface.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        public void OpenAdminWindow()
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Activate();
            Close();
        }

        public void OpenEmployeeWindow()
        {
            EmployeeWindow employeeWindow = new EmployeeWindow();
            employeeWindow.Activate();
            Close();
        }
    }
}