using Common;
using Logic;
using System.Linq;
using System.Windows.Controls;

namespace Interface.Pages
{
    /// <summary>
    /// Логика взаимодействия для AudienceEdit.xaml
    /// </summary>
    public partial class AudienceEdit : Page
    {
        private Audience _audience;

        public AudienceEdit(Audience audience)
        {
            InitializeComponent();
            _audience = audience;

            ButtonAdd.IsEnabled = false;
            ButtonUpdate.Click += (_, __) => ModifyAudience(DatabaseModify.Action.Update);
            ButtonDelete.Click += (_, __) => ModifyAudience(DatabaseModify.Action.Delete);

            InitializeUI();
        }

        public AudienceEdit()
        {
            InitializeComponent();

            ButtonAdd.Click += (_, __) => ModifyAudience(DatabaseModify.Action.Add);
            ButtonUpdate.IsEnabled = false;
            ButtonDelete.IsEnabled = false;

            InitializeUI();
        }

        private void InitializeUI()
        {
            if (_audience != null)
            {
                PopulateTextBoxes();
                PopulateComboBoxes();
            }
        }

        private void PopulateTextBoxes()
        {
            TextBoxName.Text = _audience.Name;
            TextBoxShortName.Text = _audience.ShortName;
        }

        private void PopulateComboBoxes()
        {
            User responsibleUser = DatabaseReader.GetEntity<User>(_audience.ResponsibleUserId);
            User temporarilyResponsibleUser = DatabaseReader.GetEntity<User>(_audience.ResponsibleUserId);

            var users = DatabaseReader.GetEntityList("User").OfType<User>().ToList();

            ComboBoxResponsibleUser.ItemsSource = users.Select(user => user.SecondName);
            ComboBoxTemporarilyResponsibleUser.ItemsSource = users.Select(user => user.SecondName);
            ComboBoxResponsibleUser.SelectedItem = responsibleUser == null ? null : responsibleUser.SecondName;
            ComboBoxTemporarilyResponsibleUser.Text = temporarilyResponsibleUser == null ? null : temporarilyResponsibleUser.SecondName;
        }

        private void ModifyAudience(DatabaseModify.Action action)
        {
            var audience = _audience ?? new Audience();

            audience.Name = TextBoxName.Text;
            audience.ShortName = TextBoxShortName.Text;
            audience.ResponsibleUserId = GetUserIdFromComboBox(ComboBoxResponsibleUser);
            audience.TemporarilyResponsibleUserId = GetUserIdFromComboBox(ComboBoxTemporarilyResponsibleUser);

            if (audience.Name == "")
            {
                ShowError("Empty name");
                return;
            }

            var (result, error) = DatabaseModify.ModifyEntity(audience, action);

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
            TextBlockError.Visibility = System.Windows.Visibility.Visible;
        }

        private void ClosePage()
        {
            InterfaceWindows.AdminWindow.ClosePage();
            InterfaceWindows.AdminWindow.UpdateInformation();
            InterfaceWindows.AdminWindow.HideBackButton();
        }

        private int GetUserIdFromComboBox(ComboBox comboBox)
        {
            if (!(comboBox.SelectedItem is string selectedName))
            {
                return -1;
            }
            var users = DatabaseReader.GetEntityList("User").OfType<User>().ToList();
            var user = users.First(u => u.SecondName == selectedName);
            return user != null ? user.Id : -1;
        }
    }
}