using Common;
using Logic;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Interface.Pages
{
    /// <summary>
    /// Логика взаимодействия для ConsumableEdit.xaml
    /// </summary>
    public partial class ConsumableEdit : Page
    {
        private Consumable _consumable;

        public ConsumableEdit(Consumable consumable)
        {
            InitializeComponent();
            _consumable = consumable;

            ButtonAdd.IsEnabled = false;
            ButtonUpdate.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Update);
            ButtonDelete.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Delete);

            ImagePhoto.MouseUp += (_, __) => SelectImage();

            InitializeUI();
        }

        public ConsumableEdit()
        {
            InitializeComponent();

            ButtonAdd.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Add);
            ButtonUpdate.IsEnabled = false;
            ButtonDelete.IsEnabled = false;

            ImagePhoto.MouseUp += (_, __) => SelectImage();

            InitializeUI();
        }

        private void SelectImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string imagePath = openFileDialog.FileName;

                    BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath));
                    ImagePhoto.Source = bitmapImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading the selected image: " + ex.Message);
                }
            }
        }

        private void InitializeUI()
        {
            if (_consumable != null)
            {
                PopulateExistData();
            }
            PopulateComboBoxes();
        }

        private void PopulateExistData()
        {
            TextBoxName.Text = _consumable.Name;
            TextBoxDescription.Text = _consumable.Description;
            TextBoxReceiptDate.Text = _consumable.ReceiptDate;
            ImagePhoto.Source = ByteToImage();
            TextBoxCount.Text = _consumable.Count.ToString();

            User responsibleUser = DatabaseReader.GetEntity<User>(_consumable.ResponsibleUserId);
            User temporarilyResponsibleUser = DatabaseReader.GetEntity<User>(_consumable.TemporarilyResponsibleUserId);
            ConsumableType consumableType = DatabaseReader.GetEntity<ConsumableType>(_consumable.ConsumableTypeId);

            ComboBoxResponsibleUser.SelectedItem = responsibleUser?.SecondName;
            ComboBoxTemporarilyResponsibleUser.SelectedItem = temporarilyResponsibleUser?.SecondName;
            ComboBoxConsumableType.SelectedItem = consumableType?.Name;
        }

        private void PopulateComboBoxes()
        {
            var users = DatabaseReader.GetEntityList("User").OfType<User>().ToList();
            var consumableTypes = DatabaseReader.GetEntityList("ConsumableType").OfType<ConsumableType>().ToList();

            ComboBoxResponsibleUser.ItemsSource = users.Select(c => c.SecondName);
            ComboBoxTemporarilyResponsibleUser.ItemsSource = users.Select(c => c.SecondName);
            ComboBoxConsumableType.ItemsSource = consumableTypes.Select(c => c.Name);
        }

        private void ModifyEntity(DatabaseModify.Action action)
        {
            var consumable = _consumable ?? new Consumable();

            string name = TextBoxName.Text.Trim();
            string description = TextBoxDescription.Text.Trim();
            string receiptDate = TextBoxReceiptDate.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                ShowError("Empty name");
                return;
            }

            consumable.Name = name;
            consumable.Description = description;
            consumable.ReceiptDate = receiptDate;
            consumable.Photo = ImageToByte();

            if (int.TryParse(TextBoxCount.Text, out int count))
            {
                consumable.Count = count;
            }
            else
            {
                ShowError("Invalid count");
                return;
            }

            consumable.ResponsibleUserId = GetIdFromComboBoxUser(ComboBoxResponsibleUser);
            consumable.TemporarilyResponsibleUserId = GetIdFromComboBoxUser(ComboBoxTemporarilyResponsibleUser);
            consumable.ConsumableTypeId = GetIdFromComboBoxConsumableType(ComboBoxConsumableType);

            if (action != DatabaseModify.Action.Delete && consumable.ResponsibleUserId != -1)
            {
                CreateAndAddHistoryUserConsumable(consumable);
            }

            if (action == DatabaseModify.Action.Delete)
            {
                Windows.MessageBox messageBox = new Windows.MessageBox("Delete object", "Are you sure you want to delete the record?");
                if (messageBox.ShowDialog() == false)
                {
                    return;
                }
            }

            var (result, error) = DatabaseModify.ModifyEntity(consumable, action);

            if (!result)
            {
                ShowError(error);
            }
            else
            {
                ClosePage();
            }
        }

        private void CreateAndAddHistoryUserConsumable(Consumable consumable)
        {
            HistoryUserConsumable historyUserConsumable = new HistoryUserConsumable();
            User user = DatabaseReader.GetEntity<User>(consumable.ResponsibleUserId);
            historyUserConsumable.User = user.FirstName + " " + user.SecondName;
            historyUserConsumable.Consumable = consumable.Name;
            historyUserConsumable.Date = DateTime.Now.ToString();

            DatabaseModify.ModifyEntity(historyUserConsumable, DatabaseModify.Action.Add);
        }


        private byte[] ImageToByte()
        {
            BitmapSource bitmapSource = (BitmapSource)ImagePhoto.Source;

            byte[] imageBytes;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(memoryStream);
                imageBytes = memoryStream.ToArray();
            }

            return imageBytes;
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

        private void ShowError(string error)
        {
            TextBlockError.Text = error;
            TextBlockError.Visibility =Visibility.Visible;
        }

        private void ClosePage()
        {
            InterfaceWindows.AdminWindow.ClosePage();
            InterfaceWindows.AdminWindow.UpdateInformation();
            InterfaceWindows.AdminWindow.HideBackButton();
        }

        private int GetIdFromComboBoxUser(ComboBox comboBox)
        {
            if (!(comboBox.SelectedItem is string selectedName))
            {
                return -1;
            }
            var users = DatabaseReader.GetEntityList("User").OfType<User>().ToList();
            var user = users.FirstOrDefault(u => u.SecondName == selectedName);
            return user != null ? user.Id : -1;
        }

        private int GetIdFromComboBoxConsumableType(ComboBox comboBox)
        {
            if (!(comboBox.SelectedItem is string selectedName))
            {
                return -1;
            }
            var consumableTypes = DatabaseReader.GetEntityList("ConsumableType").OfType<ConsumableType>().ToList();
            var consumableType = consumableTypes.FirstOrDefault(u => u.Name == selectedName);
            return consumableType != null ? consumableType.Id : -1;
        }
    }
}