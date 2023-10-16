using System.Windows;

namespace Interface.Windows
{
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
            ButtonAuthorization.Click += (_, __) => ButtonAuthorization_Click();
            Closing += (_, __) => Application.Current.Shutdown();
        }

        private void ButtonAuthorization_Click()
        {
            string login = TextBoxLogin.Text;
            string password = PasswordBoxPassword.Password;

            (bool isUserValid, string error, string role) = Logic.Authorization.AuthorizeUser(login, password);

            if (!isUserValid)
            {
                ShowError(error);
                return;
            }

            TextBlockError.Visibility = Visibility.Hidden;

            if (role == "Administrator")
            {
                OpenAdminWindow();
                return;
            }

            if (role == "Employee")
            {
                OpenEmployeeWindow();
            }
        }

        private void OpenAdminWindow()
        {
            InterfaceWindows.AdminWindow.Show();
            Hide();
        }

        private void OpenEmployeeWindow()
        {
            InterfaceWindows.EmployeeWindow.Show();
            Hide();
        }

        private void ShowError(string errorMessage)
        {
            TextBlockError.Visibility = Visibility.Visible;
            TextBlockError.Text = errorMessage;
        }
    }
}