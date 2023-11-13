using Common;
using System.Windows.Controls;

namespace Interface.Items
{
    /// <summary>
    /// Логика взаимодействия для HistoryUserEquipmentItem.xaml
    /// </summary>
    public partial class HistoryUserEquipmentItem : UserControl
    {
        private HistoryUserEquipment _historyUserEquipment;
        public HistoryUserEquipmentItem(HistoryUserEquipment historyUserEquipment)
        {
            InitializeComponent();
            _historyUserEquipment = historyUserEquipment;
            LoadData();
        }

        private void LoadData()
        {
            TextBlockUser.Text = _historyUserEquipment.User;
            TextBlockEquipment.Text = _historyUserEquipment.Equipment;
            TextBlockDate.Text = _historyUserEquipment.Date;
            TextBlockComment.Text = _historyUserEquipment.Comment;
        }
    }
}