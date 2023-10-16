using Common;
using Logic;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Interface.Pages
{
    /// <summary>
    /// Логика взаимодействия для ConsumableCharacteristicsEdit.xaml
    /// </summary>
    public partial class ConsumableCharacteristicsEdit : Page
    {
        private ConsumableCharacteristics _consumableCharacteristics;
        public ConsumableCharacteristicsEdit()
        {
            InitializeComponent();
            InitializeUI();
            InitializeButtons();
        }

        public ConsumableCharacteristicsEdit(ConsumableCharacteristics consumableCharacteristics)
        {
            InitializeComponent();
            _consumableCharacteristics = consumableCharacteristics;
            InitializeUI();
            InitializeButtons();
        }

        private void InitializeUI()
        {
            if (_consumableCharacteristics != null)
            {
                ConsumableCharacteristics consumableCharacteristicsValues = GetEntity<ConsumableCharacteristics>(_consumableCharacteristics.ConsumableCharacteristicsValuesId);
                PopulateUserComboBoxes();
                TextBoxName.Text = _consumableCharacteristics.Name;
                ComboBoxConsumableCharacteristicsValues.SelectedItem = consumableCharacteristicsValues.Name;
            }
        }

        private void InitializeButtons()
        {
            ButtonAdd.Click += (_, __) => ModifyAudience(DatabaseModify.Action.Add);
            ButtonUpdate.Click += (_, __) => ModifyAudience(DatabaseModify.Action.Update);
            ButtonDelete.Click += (_, __) => ModifyAudience(DatabaseModify.Action.Delete);
        }

        private void PopulateUserComboBoxes()
        {
            var consumableCharacteristics = GetEntityList<ConsumableCharacteristics>("ConsumableCharacteristics");
            ComboBoxConsumableCharacteristicsValues.ItemsSource = consumableCharacteristics.Select(consumableCharacteristic => consumableCharacteristic.Name);
        }

        private void ModifyAudience(DatabaseModify.Action action)
        {
            int consumableCharacteristicsValuesId = GetIdFromComboBox(ComboBoxConsumableCharacteristicsValues);

            if (consumableCharacteristicsValuesId == 0)
            {
                TextBlockError.Text = "Empty value";
                TextBlockError.Visibility = System.Windows.Visibility.Visible;
            }

            var consumableCharacteristics = _consumableCharacteristics ?? new ConsumableCharacteristics();

            consumableCharacteristics.Name = TextBoxName.Text;
            consumableCharacteristics.ConsumableCharacteristicsValuesId = consumableCharacteristicsValuesId;

            var (result, error) = DatabaseModify.ModifyEntity(consumableCharacteristics, action);

            TextBlockError.Text = result ? "" : error;
            TextBlockError.Visibility = result ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
        }

        private int GetIdFromComboBox(ComboBox comboBox)
        {
            string selectedName = comboBox.SelectedItem as string;
            var consumableCharacteristics = GetEntityList<ConsumableCharacteristics>("ConsumableCharacteristics");
            var consumableCharacteristic = consumableCharacteristics.FirstOrDefault(u => u.Name == selectedName);
            return consumableCharacteristic != null ? consumableCharacteristic.Id : 0;
        }

        private TEntity GetEntity<TEntity>(int id) where TEntity : class
        {
            return DatabaseReader.GetEntity<TEntity>(id);
        }

        private List<TEntity> GetEntityList<TEntity>(string entityName) where TEntity : class
        {
            return DatabaseReader.GetEntityList(entityName).OfType<TEntity>().ToList();
        }
    }
}