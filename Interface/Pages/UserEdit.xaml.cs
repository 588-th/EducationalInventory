using Common;
using Logic;
using System.Windows.Controls;

namespace Interface.Pages
{
    public partial class UserEdit : Page
    {
        private User _user;

        public UserEdit(User user)
        {
            InitializeComponent();
            _user = user;
            InitializeUI();
            InitializeButtons();
        }

        public UserEdit()
        {
            InitializeComponent();
            InitializeUI();
            InitializeButtons();
        }

        private void InitializeUI()
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

        private void InitializeButtons()
        {
            ButtonAdd.Click += (_, __) => ModifyUser(DatabaseModify.Action.Add);
            ButtonUpdate.Click += (_, __) => ModifyUser(DatabaseModify.Action.Update);
            ButtonDelete.Click += (_, __) => ModifyUser(DatabaseModify.Action.Delete);
        }

        private void ModifyUser(DatabaseModify.Action action)
        {
            var user = _user ?? new User();

            user.Login = TextBoxLogin.Text;
            user.Password = TextBoxPassword.Text;
            user.Role = ComboBoxRole.SelectedItem?.ToString();
            user.FirstName = TextBoxFirstName.Text;
            user.SecondName = TextBoxSecondName.Text;
            user.MiddleName = TextBoxMiddleName.Text;
            user.Phone = TextBoxPhone.Text;
            user.Email = TextBoxEmail.Text;
            user.Address = TextBoxAddress.Text;

            var (result, error) = DatabaseModify.ModifyEntity(user, action);

            TextBlockError.Text = result ? "" : error;
            TextBlockError.Visibility = result ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
        }
    }
}