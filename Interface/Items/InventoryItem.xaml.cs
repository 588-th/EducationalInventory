using Common;
using System.Windows.Controls;

namespace Interface.Items
{
    /// <summary>
    /// Логика взаимодействия для InventoryItem.xaml
    /// </summary>
    public partial class InventoryItem : UserControl
    {
        private Inventory _inventory;
        public InventoryItem(Inventory inventory)
        {
            InitializeComponent();
            _inventory = inventory;
            LoadData();
        }

        private void LoadData()
        {
            User user = Logic.DataBaseLogic.GetEntity<User>(_inventory.UserId);
            Equipment equipment = Logic.DataBaseLogic.GetEntity<Equipment>(_inventory.EquipmentId);

            TextBlockName.Text = _inventory.Name;
            TextBlockStartDate.Text = _inventory.StartDate;
            TextBlockEndDate.Text = _inventory.EndDate;
            TextBlockUser.Text = user.FirstName + " " + user.MiddleName;
            TextBlockEquipment.Text = equipment.Name;
        }
    }
}