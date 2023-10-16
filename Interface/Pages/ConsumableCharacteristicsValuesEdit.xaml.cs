using Common;
using Logic;
using System.Windows.Controls;

namespace Interface.Pages
{
    /// <summary>
    /// Логика взаимодействия для ConsumableCharacteristicsValuesEdit.xaml
    /// </summary>
    public partial class ConsumableCharacteristicsValuesEdit : Page
    {
        private ConsumableCharacteristicsValues _consumableCharacteristicsValues;

        public ConsumableCharacteristicsValuesEdit()
        {
            InitializeComponent();
            InitializeUI();
            InitializeButtons();
        }
        public ConsumableCharacteristicsValuesEdit(ConsumableCharacteristicsValues consumableCharacteristicsValues)
        {
            InitializeComponent();
            _consumableCharacteristicsValues = consumableCharacteristicsValues;
            InitializeUI();
            InitializeButtons();
        }

        private void InitializeUI()
        {
            if (_consumableCharacteristicsValues != null)
            {
                TextBoxName.Text = _consumableCharacteristicsValues.Name;
            }
        }

        private void InitializeButtons()
        {
            ButtonAdd.Click += (_, __) => ModifyAudience(DatabaseModify.Action.Add);
            ButtonUpdate.Click += (_, __) => ModifyAudience(DatabaseModify.Action.Update);
            ButtonDelete.Click += (_, __) => ModifyAudience(DatabaseModify.Action.Delete);
        }

        private void ModifyAudience(DatabaseModify.Action action)
        {
            var consumableCharacteristicsValues = _consumableCharacteristicsValues ?? new ConsumableCharacteristicsValues();

            consumableCharacteristicsValues.Name = TextBoxName.Text;

            var (result, error) = DatabaseModify.ModifyEntity(consumableCharacteristicsValues, action);

            TextBlockError.Text = result ? "" : error;
            TextBlockError.Visibility = result ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
        }
    }
}