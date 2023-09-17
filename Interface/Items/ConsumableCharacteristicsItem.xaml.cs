using Common;
using System.Windows.Controls;

namespace Interface.Items
{
    /// <summary>
    /// Логика взаимодействия для ConsumableCharacteristicsItem.xaml
    /// </summary>
    public partial class ConsumableCharacteristicsItem : UserControl
    {
        private ConsumableCharacteristics _consumableCharacteristics;
        public ConsumableCharacteristicsItem(ConsumableCharacteristics consumableCharacteristics)
        {
            InitializeComponent();
            _consumableCharacteristics = consumableCharacteristics;
            LoadData();
        }

        private void LoadData()
        {
            if (_consumableCharacteristics != null)
            {
                ConsumableCharacteristicsValues consumableCharacteristicsValues = Logic.DataBaseLogic.GetEntity<ConsumableCharacteristicsValues>(_consumableCharacteristics.ConsumableCharacteristicsValuesId);

                TextBlockName.Text = _consumableCharacteristics.Name;
                TextBlockConsumableCharacteristicsValues.Text = consumableCharacteristicsValues.Name;
            }
        }
    }
}