using Common;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

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


            ImagePhoto.Source = ByteToImage();
            TextBlockNumber.Text = _equipment.Number.ToString();
            TextBlockName.Text = _equipment.Name.ToString();
            TextBlockType.Text = equipmentType == null ? "" : equipmentType.Name;
            TextBlockModel.Text = modelType == null ? "" : modelType.Name;
            TextBlockAudience.Text = audience == null ? "" : audience.Name;
            TextBlockResponsibleUser.Text = responsibleUser == null ? "" : responsibleUser.FirstName + " " + responsibleUser.MiddleName;
            TextBlockTemporarilyResponsibleUser.Text = TemporarilyResponsibleUser == null ? "" : TemporarilyResponsibleUser.FirstName + " " + TemporarilyResponsibleUser.MiddleName;
            TextBlockCost.Text = _equipment.Cost.ToString();
            TextBlockRoute.Text = route == null ? "" : route.Name;
            TextBlockStatus.Text = status == null ? "" : status.Name;
            TextBlockNetworkSettings.Text = networkSetting == null ? "" : networkSetting.Ip + " " + networkSetting.Mask + "\n" + networkSetting.Gateway + " " + networkSetting.Dns;
            TextBlockProgramm.Text = programm == null ? "" : programm.Name;
            TextBlockConsumable.Text = consumable == null ? "" : consumable.Name;
            TextBlockComment.Text = _equipment.Comment;
        }

        private ImageSource ByteToImage()
        {
            if (_equipment.Photo != null && _equipment.Photo.Length > 0)
            {
                using (MemoryStream memoryStream = new MemoryStream(_equipment.Photo))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();

                    return bitmapImage;
                }
            }
            else
            {
                return null;
            }
        }
    }
}