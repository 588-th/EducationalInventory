using Common;
using System.Windows.Controls;

namespace Interface.Items
{
    /// <summary>
    /// Логика взаимодействия для ConsumableTypeItem.xaml
    /// </summary>
    public partial class ConsumableTypeItem : UserControl
    {
        private ConsumableType _consumableType;

        public ConsumableTypeItem(ConsumableType consumableType)
        {
            InitializeComponent();
            _consumableType = consumableType;
            LoadData();
        }

        private void LoadData()
        {
            ConsumableCharacteristics consumableCharacteristics = Logic.DatabaseReader.GetEntity<ConsumableCharacteristics>(_consumableType.Id);

            TextBlockName.Text = _consumableType.Name;
            TextBlockConsumableCharacteristics.Text = consumableCharacteristics.Name;
        }
    }
}