using Common;
using System.Windows.Controls;

namespace Interface.Items
{
    /// <summary>
    /// Логика взаимодействия для EquipmentItem.xaml
    /// </summary>
    public partial class EquipmentItem : UserControl
    {
        private Equipment _equipment;
        public EquipmentItem(Equipment equipment)
        {
            InitializeComponent();
            _equipment = equipment;
            LoadData();
        }

        private void LoadData()
        {
            Audience audience = Logic.DatabaseReader.GetEntity<Audience>(_equipment.AudienceId);
            EquipmentType equipmentType = Logic.DatabaseReader.GetEntity<EquipmentType>(_equipment.EquipmentTypeId);
            ModelType modelType = Logic.DatabaseReader.GetEntity<ModelType>(_equipment.ModelTypeId);
            User responsibleUser = Logic.DatabaseReader.GetEntity<User>(_equipment.ResponsibleUserId);
            User TemporarilyResponsibleUser = Logic.DatabaseReader.GetEntity<User>(_equipment.TemporarilyResponsibleUserId);
            Route route = Logic.DatabaseReader.GetEntity<Route>(_equipment.RouteId);
            Status status = Logic.DatabaseReader.GetEntity<Status>(_equipment.StatusId);
            NetworkSetting networkSetting = Logic.DatabaseReader.GetEntity<NetworkSetting>(_equipment.NetworkSettingsId);
            Programm programm = Logic.DatabaseReader.GetEntity<Programm>(_equipment.ProgrammId);
            Consumable consumable = Logic.DatabaseReader.GetEntity<Consumable>(_equipment.ConsumableId);


            // ImagePhoto. =
            TextBlockNumber.Text = _equipment.Number.ToString();
            TextBlockName.Text = _equipment.Name.ToString();
            TextBlockType.Text = equipmentType.Name;
            TextBlockModel.Text = modelType.Name;
            TextBlockAudience.Text = audience.Name;
            TextBlockResponsibleUser.Text = responsibleUser.FirstName + " " + responsibleUser.MiddleName;
            TextBlockTemporarilyResponsibleUser.Text = TemporarilyResponsibleUser.FirstName + " " + TemporarilyResponsibleUser.MiddleName;
            TextBlockCost.Text = _equipment.Cost.ToString();
            TextBlockRoute.Text = route.Name;
            TextBlockStatus.Text = status.Name;
            TextBlockNetworkSettings.Text = networkSetting.Ip + " " + networkSetting.Mask + "\n" + networkSetting.Gateway + " " + networkSetting.Dns;
            TextBlockProgramm.Text = programm.Name;
            TextBlockConsumable.Text = consumable.Name;
            TextBlockComment.Text = _equipment.Comment;
        }
    }
}