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
            TextBlockUser.Text = _historyUserConsumable.User;
            TextBlockConsumable.Text = _historyUserConsumable.Consumable;
            TextBlockDate.Text = _historyUserConsumable.Date;
            TextBlockComment.Text = _historyUserConsumable.Comment;
        }
    }
}