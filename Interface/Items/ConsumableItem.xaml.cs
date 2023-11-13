using Common;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

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

            ImagePhoto.Source = ByteToImage();
            TextBlockName.Text = _consumable.Name;
            TextBlockDescription.Text = _consumable.Description;
            TextBlockResponsibleUser.Text = responsibleUser == null ? "" : responsibleUser.FirstName + " " + responsibleUser.MiddleName;
            TextBlockTemporarilyResponsibleUser.Text = temporarilyResponsibleUser == null ? "" : temporarilyResponsibleUser.FirstName + " " + temporarilyResponsibleUser.MiddleName;
            TextBlockCount.Text = _consumable.Count.ToString();
            TextBlockReceiptDate.Text = _consumable.ReceiptDate;
            TextBlockConsumableType.Text = consumableType == null ? "" : consumableType.Name;
        }

        private ImageSource ByteToImage()
        {
            if (_consumable.Photo != null && _consumable.Photo.Length > 0)
            {
                using (MemoryStream memoryStream = new MemoryStream(_consumable.Photo))
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