using Common;
using Logic;
using System.Windows.Controls;

namespace Interface.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserEdit.xaml
    /// </summary>
    public partial class UserEdit : Page
    {
        private User _user;

        public UserEdit(User user)
        {
            InitializeComponent();
            _user = user;
            LoadData();

            ButtonAdd.Click += (_, __) => ModifyUser(DataBaseLogic.Action.Add);
            ButtonUpdate.Click += (_, __) => ModifyUser(DataBaseLogic.Action.Update);
            ButtonDelete.Click += (_, __) => ModifyUser(DataBaseLogic.Action.Delete);
        }

        private void LoadData()
        {
            ComboBoxRole.Items.Add("Administrator");
            ComboBoxRole.Items.Add("Employee");

            if (_user != null)
            {
                TextBoxLogin.Text = _user.Login;
                TextBoxPassword.Text = _user.Password;
                ComboBoxRole.SelectedItem = _user.Role;
                TextBoxFirstName.Text = _user.FirstName;
                TextBoxSecondName.Text = _user.SecondName;
                TextBoxMiddleName.Text = _user.MiddleName;
                TextBoxPhone.Text = _user.Phone;
                TextBoxEmail.Text = _user.Email;
                TextBoxAddress.Text = _user.Address;
            }
        }

        private void ModifyUser(DataBaseLogic.Action action)
        {
            User user = new User
            {
                Login = TextBoxLogin.Text,
                Password = TextBoxPassword.Text,
                Role = ComboBoxRole.SelectedItem.ToString(),
                FirstName = TextBoxFirstName.Text,
                SecondName = TextBoxSecondName.Text,
                MiddleName = TextBoxMiddleName.Text,
                Phone = TextBoxPhone.Text,
                Email = TextBoxEmail.Text,
                Address = TextBoxAddress.Text,
            };

            (bool result, string error) = DataBaseLogic.ModifyUser(user, action);

            TextBlockError.Text = result ? "" : error;
            TextBlockError.Visibility = result ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
        }
    }
}