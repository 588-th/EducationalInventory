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
            User user = Logic.DatabaseReader.GetEntity<User>(_historyUserEquipment.UserId);
            Equipment equipment = Logic.DatabaseReader.GetEntity<Equipment>(_historyUserEquipment.EquipmentId);

            TextBlockUser.Text = user.FirstName + " " + user.MiddleName;
            TextBlockEquipment.Text = equipment.Name;
            TextBlockDate.Text = _historyUserEquipment.Date;
        }
    }
}