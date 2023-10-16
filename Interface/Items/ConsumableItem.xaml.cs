using Common;
using System.Windows.Controls;

namespace Interface.Items
{
    /// <summary>
    /// Логика взаимодействия для ConsumableItem.xaml
    /// </summary>
    public partial class ConsumableItem : UserControl
    {
        private Consumable _consumable;
        public ConsumableItem(Consumable consumable)
        {
            InitializeComponent();
            _consumable = consumable;
            LoadData();
        }

        private void LoadData()
        {
            User responsibleUser = Logic.DatabaseReader.GetEntity<User>(_consumable.ResponsibleUserId);
            User temporarilyResponsibleUser = Logic.DatabaseReader.GetEntity<User>(_consumable.ResponsibleUserId);
            ConsumableType consumableType = Logic.DatabaseReader.GetEntity<ConsumableType>(_consumable.ConsumableTypeId);

            //ImagePhoto. = 
            TextBlockName.Text = _consumable.Name;
            TextBlockDescription.Text = _consumable.Description;
            TextBlockResponsibleUser.Text = responsibleUser.FirstName + " " + responsibleUser.MiddleName;
            TextBlockTemporarilyResponsibleUser.Text = temporarilyResponsibleUser.FirstName + " " + temporarilyResponsibleUser.MiddleName;
            TextBlockCount.Text = _consumable.Count.ToString();
            TextBlockReceiptDate.Text = _consumable.ReceiptDate;
            TextBlockConsumableType.Text = consumableType.Name;
        }
    }
}