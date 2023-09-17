using Common;
using System.Windows.Controls;

namespace Interface.Items
{
    /// <summary>
    /// Логика взаимодействия для ConsumableCharacteristicsValuesItem.xaml
    /// </summary>
    public partial class ConsumableCharacteristicsValuesItem : UserControl
    {
        private ConsumableCharacteristicsValues _consumableCharacteristicsValues;

        public ConsumableCharacteristicsValuesItem(ConsumableCharacteristicsValues consumableCharacteristicsValues)
        {
            InitializeComponent();
            _consumableCharacteristicsValues = consumableCharacteristicsValues;
            LoadData();
        }

        private void LoadData()
        {
            TextBlockName.Text = _consumableCharacteristicsValues.Name; 
        }
    }
}