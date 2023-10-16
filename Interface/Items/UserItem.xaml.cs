using Common;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Interface.Items
{
    /// <summary>
    /// Логика взаимодействия для UserItem.xaml
    /// </summary>
    public partial class UserItem : UserControl
    {
        private User _user;
        public UserItem(User user)
        {
            InitializeComponent();
            _user = user;
            LoadData();
        }

        private void LoadData()
        {
            TextBlockFirstName.Text = _user.FirstName;
            TextBlockSecondName.Text = _user.SecondName;
            TextBlockMiddleName.Text = _user.MiddleName;
            TextBlockEmail.Text = _user.Email;
            TextBlockPhone.Text = _user.Phone;
            TextBlockAddress.Text = _user.Address;
            TextBlockLogin.Text = _user.Login;
            TextBlockPassword.Text = _user.Password;
            TextBlockRole.Text = _user.Role;
        }
    }
}