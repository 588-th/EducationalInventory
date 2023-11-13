using Common;
using System.Runtime.CompilerServices;
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
                ConsumableCharacteristicsValues consumableCharacteristicsValues = Logic.DatabaseReader.GetEntity<ConsumableCharacteristicsValues>(_consumableCharacteristics.ConsumableCharacteristicsValuesId);

                TextBlockName.Text = _consumableCharacteristics.Name;
                TextBlockConsumableCharacteristicsValues.Text = consumableCharacteristicsValues == null ? "" : consumableCharacteristicsValues.Name;
            }
        }
    }
}