using Common;
using System.Windows.Controls;

namespace Interface.Items
{
    /// <summary>
    /// Логика взаимодействия для HistoryInventoryEquipmentItem.xaml
    /// </summary>
    public partial class HistoryInventoryEquipmentItem : UserControl
    {
        private HistoryInventoryEquipment _historyInventoryEquipment;
        public HistoryInventoryEquipmentItem(HistoryInventoryEquipment historyInventoryEquipment)
        {
            InitializeComponent();
            _historyInventoryEquipment = historyInventoryEquipment;
            LoadData();
        }

        private void LoadData()
        {
            TextBlockUser.Text = _historyInventoryEquipment.User;
            TextBlockInventory.Text = _historyInventoryEquipment.Inventory;
            TextBlockEquipment.Text = _historyInventoryEquipment.Equipment;
            TextBlockDate.Text = _historyInventoryEquipment.Date;
            TextBlockComment.Text = _historyInventoryEquipment.Comment;
        }
    }
}