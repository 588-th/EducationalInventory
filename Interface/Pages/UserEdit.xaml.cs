using Common;
using Logic;
using System.Windows;
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

            ButtonAdd.IsEnabled = false;
            ButtonUpdate.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Update);
            ButtonDelete.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Delete);

            InitializeUI();
        }

        public UserEdit()
        {
            InitializeComponent();

            ButtonAdd.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Add);
            ButtonUpdate.IsEnabled = false;
            ButtonDelete.IsEnabled = false;

            InitializeUI();
        }

        private void InitializeUI()
        {
            if (_user != null)
            {
                PopulateExistData();
            }
            PopulateComboBoxes();
        }

        private void PopulateExistData()
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

        private void PopulateComboBoxes()
        {
            ComboBoxRole.Items.Add("Administrator");
            ComboBoxRole.Items.Add("Employee");
        }

        private void ModifyEntity(DatabaseModify.Action action)
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

            if (action == DatabaseModify.Action.Delete)
            {
                Windows.MessageBox messageBox = new Windows.MessageBox("Delete object", "Are you sure you want to delete the record?");
                if (messageBox.ShowDialog() == false)
                {
                    return;
                }
            }

            var (result, error) = DatabaseModify.ModifyEntity(user, action);

            if (!result)
            {
                ShowError(error);
            }
            else
            {
                ClosePage();
            }
        }

        private void ShowError(string error)
        {
            TextBlockError.Text = error;
            TextBlockError.Visibility = Visibility.Visible;
        }

        private void ClosePage()
        {
            InterfaceWindows.AdminWindow.ClosePage();
            InterfaceWindows.AdminWindow.UpdateInformation();
            InterfaceWindows.AdminWindow.HideBackButton();
        }
    }
}