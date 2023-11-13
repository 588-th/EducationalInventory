using Common;
using Logic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Interface.Pages
{
    /// <summary>
    /// Логика взаимодействия для ConsumableCharacteristicsEdit.xaml
    /// </summary>
    public partial class ConsumableCharacteristicsEdit : Page
    {
        private ConsumableCharacteristics _consumableCharacteristics;

        public ConsumableCharacteristicsEdit(ConsumableCharacteristics consumableCharacteristics)
        {
            InitializeComponent();
            _consumableCharacteristics = consumableCharacteristics;

            ButtonAdd.IsEnabled = false;
            ButtonUpdate.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Update);
            ButtonDelete.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Delete);

            InitializeUI();
        }

        public ConsumableCharacteristicsEdit()
        {
            InitializeComponent();

            ButtonAdd.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Add);
            ButtonUpdate.IsEnabled = false;
            ButtonDelete.IsEnabled = false;

            InitializeUI();
        }

        private void InitializeUI()
        {
            if (_consumableCharacteristics != null)
            {
                PopulateExistData();
            }
            PopulateComboBoxes();
        }

        private void PopulateExistData()
        {
            TextBoxName.Text = _consumableCharacteristics.Name;

            ConsumableCharacteristicsValues consumableCharacteristicsValues = DatabaseReader.GetEntity<ConsumableCharacteristicsValues>(_consumableCharacteristics.ConsumableCharacteristicsValuesId);

            ComboBoxConsumableCharacteristicsValues.SelectedItem = consumableCharacteristicsValues?.Name;
        }

        private void PopulateComboBoxes()
        {
            var consumableCharacteristics = DatabaseReader.GetEntityList("ConsumableCharacteristicsValues").OfType<ConsumableCharacteristicsValues>().ToList();

            ComboBoxConsumableCharacteristicsValues.ItemsSource = consumableCharacteristics.Select(consumableCharacteristic => consumableCharacteristic.Name);
        }

        private void ModifyEntity(DatabaseModify.Action action)
        {
            var consumableCharacteristics = _consumableCharacteristics ?? new ConsumableCharacteristics();

            consumableCharacteristics.Name = TextBoxName.Text;
            consumableCharacteristics.ConsumableCharacteristicsValuesId = GetIdFromComboBox(ComboBoxConsumableCharacteristicsValues);

            ComboBoxItem ConsumableCharacteristicsValuesItem = ComboBoxConsumableCharacteristicsValues.SelectedItem as ComboBoxItem;

            if (consumableCharacteristics.Name == "")
            {
                ShowError("Empty name");
                return;
            }

            if (ConsumableCharacteristicsValuesItem == null)
            {
                ShowError("Empty values");
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

            var (result, error) = DatabaseModify.ModifyEntity(consumableCharacteristics, action);

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

        private int GetIdFromComboBox(ComboBox comboBox)
        {
            if (!(comboBox.SelectedItem is string selectedName))
            {
                return -1;
            }
            var consumableCharacteristics = DatabaseReader.GetEntityList("ConsumableCharacteristicsValues").OfType<ConsumableCharacteristicsValues>().ToList();
            var consumableCharacteristic = consumableCharacteristics.FirstOrDefault(u => u.Name == selectedName);
            return consumableCharacteristic != null ? consumableCharacteristic.Id : -1;
        }
    }
}