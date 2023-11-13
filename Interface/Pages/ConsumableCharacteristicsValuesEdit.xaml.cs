using Common;
using Logic;
using System.Windows;
using System.Windows.Controls;

namespace Interface.Pages
{
    /// <summary>
    /// Логика взаимодействия для ConsumableCharacteristicsValuesEdit.xaml
    /// </summary>
    public partial class ConsumableCharacteristicsValuesEdit : Page
    {
        private ConsumableCharacteristicsValues _consumableCharacteristicsValues;

        public ConsumableCharacteristicsValuesEdit(ConsumableCharacteristicsValues consumableCharacteristicsValues)
        {
            InitializeComponent();
            _consumableCharacteristicsValues = consumableCharacteristicsValues;

            ButtonAdd.IsEnabled = false;
            ButtonUpdate.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Update);
            ButtonDelete.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Delete);

            InitializeUI();
        }

        public ConsumableCharacteristicsValuesEdit()
        {
            InitializeComponent();

            ButtonAdd.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Add);
            ButtonUpdate.IsEnabled = false;
            ButtonDelete.IsEnabled = false;

            InitializeUI();
        }

        private void InitializeUI()
        {
            if (_consumableCharacteristicsValues != null)
            {
                PopulateExistData();
            }
        }

        private void PopulateExistData()
        {
            TextBoxName.Text = _consumableCharacteristicsValues.Name;
        }

        private void ModifyEntity(DatabaseModify.Action action)
        {
            var consumableCharacteristicsValues = _consumableCharacteristicsValues ?? new ConsumableCharacteristicsValues();

            consumableCharacteristicsValues.Name = TextBoxName.Text;

            if (consumableCharacteristicsValues.Name == "")
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

            var (result, error) = DatabaseModify.ModifyEntity(consumableCharacteristicsValues, action);

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
    }
}