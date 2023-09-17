using Common;
using System.Windows.Controls;

namespace Interface.Items
{
    /// <summary>
    /// Логика взаимодействия для HistoryAudienceEquipmentItem.xaml
    /// </summary>
    public partial class HistoryAudienceEquipmentItem : UserControl
    {
        private HistoryAudienceEquipment _historyAudienceEquipment;
        public HistoryAudienceEquipmentItem(HistoryAudienceEquipment historyAudienceEquipment)
        {
            InitializeComponent();
            _historyAudienceEquipment = historyAudienceEquipment;
            LoadData();
        }

        private void LoadData()
        {
            Audience audience = Logic.DataBaseLogic.GetEntity<Audience>(_historyAudienceEquipment.AudienceId);
            Equipment equipment = Logic.DataBaseLogic.GetEntity<Equipment>(_historyAudienceEquipment.EquipmentId);

            TextBlockAudience.Text = audience.Name;
            TextBlockEquipment.Text = equipment.Name;
            TextBlockDate.Text = _historyAudienceEquipment.Date;
        }
    }
}