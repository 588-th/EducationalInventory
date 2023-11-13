using Common;
using Logic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Interface.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProgrammEdit.xaml
    /// </summary>
    public partial class ProgrammEdit : Page
    {
        private Programm _programm;

        public ProgrammEdit(Programm programm)
        {
            InitializeComponent();
            _programm = programm;

            ButtonAdd.IsEnabled = false;
            ButtonUpdate.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Update);
            ButtonDelete.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Delete);

            InitializeUI();
        }

        public ProgrammEdit()
        {
            InitializeComponent();

            ButtonAdd.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Add);
            ButtonUpdate.IsEnabled = false;
            ButtonDelete.IsEnabled = false;

            InitializeUI();
        }

        private void InitializeUI()
        {
            if (_programm != null)
            {
                PopulateExistData();
            }
            PopulateComboBoxes();
        }

        private void PopulateExistData()
        {
            TextBoxName.Text = _programm.Name;
            TextBoxVersion.Text = _programm.Version;

            Developer developer = DatabaseReader.GetEntity<Developer>(_programm.DeveloperId);

            ComboBoxDeveloper.SelectedItem = developer?.Name;
        }

        private void PopulateComboBoxes()
        {
            var developers = DatabaseReader.GetEntityList("Developer").OfType<Developer>().ToList();

            ComboBoxDeveloper.ItemsSource = developers.Select(e => e.Name);
        }

        private void ModifyEntity(DatabaseModify.Action action)
        {
            var programm = _programm ?? new Programm();

            programm.Name = TextBoxName.Text;
            programm.Version = TextBoxVersion.Text;
            programm.DeveloperId = GetUserIdFromComboBoxDeveloper(ComboBoxDeveloper);

            if (programm.Name == "")
            {
                ShowError("Empty name");
                return;
            }

            if (action == DatabaseModify.Action.Delete)
            {
                Windows.MessageBox messageBox = new Windows.MessageBox("Delete object", "Are you sure you want to delete the record?");
                if (messageBox.ShowDialog() == false)
                {
                    return;
                }
            }

            var (result, error) = DatabaseModify.ModifyEntity(programm, action);

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

        private int GetUserIdFromComboBoxDeveloper(ComboBox comboBox)
        {
            if (!(comboBox.SelectedItem is string selectedName))
            {
                return -1;
            }
            var equipments = DatabaseReader.GetEntityList("Developer").OfType<Developer>().ToList();
            var equipment = equipments.First(e => e.Name == selectedName);
            return equipment != null ? equipment.Id : -1;
        }
    }
}