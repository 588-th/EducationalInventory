using Common;
using System.Windows.Controls;

namespace Interface.Items
{
    /// <summary>
    /// Логика взаимодействия для HistoryUserConsumableItem.xaml
    /// </summary>
    public partial class HistoryUserConsumableItem : UserControl
    {
        private HistoryUserConsumable _historyUserConsumable;
        public HistoryUserConsumableItem(HistoryUserConsumable historyUserConsumable)
        {
            InitializeComponent();
            _historyUserConsumable = historyUserConsumable;
            LoadData();
        }

        private void LoadData()
        {
            User user = Logic.DataBaseLogic.GetEntity<User>(_historyUserConsumable.UserId);
            Consumable consumable = Logic.DataBaseLogic.GetEntity<Consumable>(_historyUserConsumable.ConsumableId);

            TextBlockUser.Text = user.FirstName + " " + user.MiddleName;
            TextBlockConsumable.Text = consumable.Name;
            TextBlockDate.Text = _historyUserConsumable.Date;
        }
    }
}