using Common;
using Logic;
using System.Windows;

namespace Interface.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddComment.xaml
    /// </summary>
    public partial class AddComment : Window
    {
        private HistoryInventoryEquipment _historyInventoryEquipment;
        private HistoryUserConsumable _historyUserConsumable;
        private HistoryUserEquipment _historyUserEquipment;
        public AddComment(HistoryInventoryEquipment historyInventoryEquipment)
        {
            InitializeComponent();
            _historyInventoryEquipment = historyInventoryEquipment;
            ButtonAdd.Click += (_, __) => AddEntityComment();
        }

        public AddComment(HistoryUserConsumable historyUserConsumable)
        {
            InitializeComponent();
            _historyUserConsumable = historyUserConsumable;
            ButtonAdd.Click += (_, __) => AddEntityComment();
        }

        public AddComment(HistoryUserEquipment historyUserEquipment)
        {
            InitializeComponent();
            _historyUserEquipment = historyUserEquipment;
            ButtonAdd.Click += (_, __) => AddEntityComment();
        }

        private void AddEntityComment()
        {
            string comment = TextBoxComment.Text;
            if (_historyInventoryEquipment != null)
            {
                _historyInventoryEquipment.Comment = comment;
                DatabaseModify.ModifyEntity(_historyInventoryEquipment, DatabaseModify.Action.Update);
            }
            if (_historyUserConsumable != null)
            {
                _historyUserConsumable.Comment = comment;
                DatabaseModify.ModifyEntity(_historyUserConsumable, DatabaseModify.Action.Update);
            }
            if (_historyUserEquipment != null)
            {
                _historyUserEquipment.Comment = comment;
                DatabaseModify.ModifyEntity(_historyUserEquipment, DatabaseModify.Action.Update);
            }

            CloseWindow();
        }

        private void CloseWindow()
        {
            Close();
            InterfaceWindows.AdminWindow.UpdateInformation();
        }
    }
}