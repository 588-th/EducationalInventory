using Common;
using Logic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Interface.Pages
{
    /// <summary>
    /// Логика взаимодействия для ConsumableTypeEdit.xaml
    /// </summary>
    public partial class ConsumableTypeEdit : Page
    {
        private ConsumableType _consumableType;

        public ConsumableTypeEdit(ConsumableType consumableType)
        {
            InitializeComponent();
            _consumableType = consumableType;

            ButtonAdd.IsEnabled = false;
            ButtonUpdate.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Update);
            ButtonDelete.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Delete);

            InitializeUI();
        }

        public ConsumableTypeEdit()
        {
            InitializeComponent();

            ButtonAdd.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Add);
            ButtonUpdate.IsEnabled = false;
            ButtonDelete.IsEnabled = false;

            InitializeUI();
        }

        private void InitializeUI()
        {
            if (_consumableType != null)
            {
                PopulateExistData();
            }
            PopulateComboBoxes();
        }

        private void PopulateExistData()
        {
            TextBoxName.Text = _consumableType.Name;

            ConsumableCharacteristics consumableCharacteristics = DatabaseReader.GetEntity<ConsumableCharacteristics>(_consumableType.ConsumableCharacteristicsId);

            ComboBoxConsumableCharacteristics.SelectedItem = consumableCharacteristics?.Name;
        }

        private void PopulateComboBoxes()
        {
            var consumableCharacteristics = DatabaseReader.GetEntityList("ConsumableCharacteristics").OfType<ConsumableCharacteristics>().ToList();

            ComboBoxConsumableCharacteristics.ItemsSource = consumableCharacteristics.Select(c => c.Name);
        }

        private void ModifyEntity(DatabaseModify.Action action)
        {
            var consumableType = _consumableType ?? new ConsumableType();

            consumableType.Name = TextBoxName.Text;
            consumableType.ConsumableCharacteristicsId = GetIdFromComboBox(ComboBoxConsumableCharacteristics);

            ComboBoxItem ConsumableCharacteristicsItem = ComboBoxConsumableCharacteristics.SelectedItem as ComboBoxItem;

            if (consumableType.Name == "")
            {
                ShowError("Empty name");
                return;
            }

            if (ConsumableCharacteristicsItem == null)
            {
                ShowError("Empty consumable characteristics");
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

            var (result, error) = DatabaseModify.ModifyEntity(consumableType, action);

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
            var consumableCharacteristics = DatabaseReader.GetEntityList("ConsumableCharacteristics").OfType<ConsumableCharacteristics>().ToList();
            var consumableCharacteristic = consumableCharacteristics.FirstOrDefault(u => u.Name == selectedName);
            return consumableCharacteristic != null ? consumableCharacteristic.Id : -1;
        }
    }
}