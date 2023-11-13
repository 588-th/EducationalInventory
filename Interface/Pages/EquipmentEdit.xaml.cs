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
    /// Логика взаимодействия для EquipmentEdit.xaml
    /// </summary>
    public partial class EquipmentEdit : Page
    {
        private Equipment _equipment;

        public EquipmentEdit(Equipment equipment)
        {
            InitializeComponent();
            _equipment = equipment;

            ButtonAdd.IsEnabled = false;
            ButtonUpdate.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Update);
            ButtonDelete.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Delete);

            ImagePhoto.MouseUp += (_, __) => SelectImage();

            InitializeUI();
        }

        public EquipmentEdit()
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
            if (_equipment != null)
            {
                PopulateExistData();
            }
            PopulateComboBoxes();
        }

        private void PopulateExistData()
        {
            TextBoxName.Text = _equipment.Name;
            ImagePhoto.Source = ByteToImage();
            TextBoxNumber.Text = _equipment.Number.ToString();
            TextBoxCost.Text = _equipment.Cost.ToString();
            TextBoxComment.Text = _equipment.Comment;

            EquipmentType equipmentType = DatabaseReader.GetEntity<EquipmentType>(_equipment.EquipmentTypeId);
            ModelType modelType = DatabaseReader.GetEntity<ModelType>(_equipment.ModelTypeId);
            Audience audience = DatabaseReader.GetEntity<Audience>(_equipment.AudienceId);
            User responsibleUser = DatabaseReader.GetEntity<User>(_equipment.ResponsibleUserId);
            User temporarilyresponsibleUser = DatabaseReader.GetEntity<User>(_equipment.TemporarilyResponsibleUserId);
            Route route = DatabaseReader.GetEntity<Route>(_equipment.RouteId);
            Status status = DatabaseReader.GetEntity<Status>(_equipment.StatusId);
            NetworkSetting networkSetting = DatabaseReader.GetEntity<NetworkSetting>(_equipment.NetworkSettingsId);
            Programm programm = DatabaseReader.GetEntity<Programm>(_equipment.ProgrammId);
            Consumable consumable = DatabaseReader.GetEntity<Consumable>(_equipment.ConsumableId);

            ComboBoxTypeEquipment.SelectedItem = equipmentType?.Name;
            ComboBoxTypeModel.SelectedItem = modelType?.Name;
            ComboBoxAuditoruim.SelectedItem = audience?.Name;
            ComboBoxResponsibleUser.SelectedItem = responsibleUser?.SecondName;
            ComboBoxTemporarilyResponsibleUser.SelectedItem = temporarilyresponsibleUser?.SecondName;
            ComboBoxRoute.SelectedItem = route?.Name;
            ComboBoxStatus.SelectedItem = status?.Name;
            ComboBoxNetworkSettings.SelectedItem = networkSetting?.Ip;
            ComboBoxProgramm.SelectedItem = programm?.Name;
            ComboBoxConsumable.SelectedItem = consumable?.Name;
        }

        private void PopulateComboBoxes()
        {
            var equipmentTypes = DatabaseReader.GetEntityList("EquipmentType").OfType<EquipmentType>().ToList();
            var modelTypes = DatabaseReader.GetEntityList("ModelType").OfType<ModelType>().ToList();
            var audiences = DatabaseReader.GetEntityList("Audience").OfType<Audience>().ToList();
            var users = DatabaseReader.GetEntityList("User").OfType<User>().ToList();
            var routes = DatabaseReader.GetEntityList("Route").OfType<Route>().ToList();
            var statuses = DatabaseReader.GetEntityList("Status").OfType<Status>().ToList();
            var networkSettings = DatabaseReader.GetEntityList("NetworkSetting").OfType<NetworkSetting>().ToList();
            var programms = DatabaseReader.GetEntityList("Programm").OfType<Programm>().ToList();
            var consumables = DatabaseReader.GetEntityList("Consumable").OfType<Consumable>().ToList();

            ComboBoxTypeEquipment.ItemsSource = equipmentTypes.Select(c => c.Name);
            ComboBoxTypeModel.ItemsSource = modelTypes.Select(c => c.Name);
            ComboBoxAuditoruim.ItemsSource = audiences.Select(c => c.Name);
            ComboBoxResponsibleUser.ItemsSource = users.Select(c => c.SecondName);
            ComboBoxTemporarilyResponsibleUser.ItemsSource = users.Select(c => c.SecondName);
            ComboBoxRoute.ItemsSource = routes.Select(c => c.Name);
            ComboBoxStatus.ItemsSource = statuses.Select(c => c.Name);
            ComboBoxNetworkSettings.ItemsSource = networkSettings.Select(c => c.Ip);
            ComboBoxProgramm.ItemsSource = programms.Select(c => c.Name);
            ComboBoxConsumable.ItemsSource = consumables.Select(c => c.Name);
        }

        private void ModifyEntity(DatabaseModify.Action action)
        {
            var equipment = _equipment ?? new Equipment();

            string name = TextBoxName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                ShowError("Empty name");
                return;
            }

            if (int.TryParse(TextBoxNumber.Text, out int number))
            {
                equipment.Number = number;
            }
            else
            {
                ShowError("Invalid number");
                return;
            }

            if (!string.IsNullOrEmpty(TextBoxCost.Text) && decimal.TryParse(TextBoxCost.Text, out decimal cost))
            {
                equipment.Cost = cost;
            }
            else if (!string.IsNullOrEmpty(TextBoxCost.Text))
            {
                ShowError("Invalid cost");
                return;
            }

            equipment.Name = name;
            equipment.Photo = ImageToByte();
            equipment.Comment = TextBoxComment.Text;
            equipment.EquipmentTypeId = GetIdFromComboBoxEquipmentType(ComboBoxTypeEquipment);
            equipment.ModelTypeId = GetIdFromComboBoxModelType(ComboBoxTypeModel);
            equipment.AudienceId = GetIdFromComboBoxAudience(ComboBoxAuditoruim);
            equipment.ResponsibleUserId = GetIdFromComboBoxUser(ComboBoxResponsibleUser);
            equipment.TemporarilyResponsibleUserId = GetIdFromComboBoxUser(ComboBoxTemporarilyResponsibleUser);
            equipment.RouteId = GetIdFromComboBoxRoute(ComboBoxRoute);
            equipment.StatusId = GetIdFromComboBoxStatus(ComboBoxStatus);
            equipment.NetworkSettingsId = GetIdFromComboBoxNetworkSettings(ComboBoxNetworkSettings);
            equipment.ProgrammId = GetIdFromComboBoxProgramm(ComboBoxProgramm);
            equipment.ConsumableId = GetIdFromComboBoxConsumable(ComboBoxConsumable);

            if (action != DatabaseModify.Action.Delete && equipment.ResponsibleUserId != -1)
            {
                CreateAndAddHistoryUserEquipment(equipment);
            }

            if (action == DatabaseModify.Action.Delete)
            {
                Windows.MessageBox messageBox = new Windows.MessageBox("Delete object", "Are you sure you want to delete the record?");
                if (messageBox.ShowDialog() == false)
                {
                    return;
                }
            }

            var (result, error) = DatabaseModify.ModifyEntity(equipment, action);

            if (!result)
            {
                ShowError(error);
            }
            else
            {
                ClosePage();
            }
        }

        private void CreateAndAddHistoryUserEquipment(Equipment equipment)
        {
            HistoryUserEquipment historyUserEquipment = new HistoryUserEquipment();
            User user = DatabaseReader.GetEntity<User>(equipment.ResponsibleUserId);
            historyUserEquipment.User = user.FirstName + " " + user.SecondName;
            historyUserEquipment.Equipment = equipment.Name;
            historyUserEquipment.Date = DateTime.Now.ToString();

            DatabaseModify.ModifyEntity(historyUserEquipment, DatabaseModify.Action.Add);
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

        private void ShowError(string error)
        {
            TextBlockError.Text = error;
            TextBlockError.Visibility = System.Windows.Visibility.Visible;
        }

        private void ClosePage()
        {
            InterfaceWindows.AdminWindow.ClosePage();
            InterfaceWindows.AdminWindow.UpdateInformation();
            InterfaceWindows.AdminWindow.HideBackButton();
        }

        private int GetIdFromComboBoxEquipmentType(ComboBox comboBox)
        {
            if (!(comboBox.SelectedItem is string selectedName))
            {
                return -1;
            }
            var equipmentTypes = DatabaseReader.GetEntityList("EquipmentType").OfType<EquipmentType>().ToList();
            var equipmentType = equipmentTypes.FirstOrDefault(u => u.Name == selectedName);
            return equipmentType != null ? equipmentType.Id : -1;
        }

        private int GetIdFromComboBoxModelType(ComboBox comboBox)
        {
            if (!(comboBox.SelectedItem is string selectedName))
            {
                return -1;
            }
            var modelTypes = DatabaseReader.GetEntityList("ModelType").OfType<ModelType>().ToList();
            var modelType = modelTypes.FirstOrDefault(u => u.Name == selectedName);
            return modelType != null ? modelType.Id : -1;
        }

        private int GetIdFromComboBoxAudience(ComboBox comboBox)
        {
            if (!(comboBox.SelectedItem is string selectedName))
            {
                return -1;
            }
            var audiences = DatabaseReader.GetEntityList("Audience").OfType<Audience>().ToList();
            var audience = audiences.FirstOrDefault(u => u.Name == selectedName);
            return audience != null ? audience.Id : -1;
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

        private int GetIdFromComboBoxRoute(ComboBox comboBox)
        {
            if (!(comboBox.SelectedItem is string selectedName))
            {
                return -1;
            }
            var routes = DatabaseReader.GetEntityList("Route").OfType<Route>().ToList();
            var route = routes.FirstOrDefault(u => u.Name == selectedName);
            return route != null ? route.Id : -1;
        }

        private int GetIdFromComboBoxStatus(ComboBox comboBox)
        {
            if (!(comboBox.SelectedItem is string selectedName))
            {
                return -1;
            }
            var statuses = DatabaseReader.GetEntityList("Status").OfType<Status>().ToList();
            var status = statuses.FirstOrDefault(u => u.Name == selectedName);
            return status != null ? status.Id : -1;
        }

        private int GetIdFromComboBoxNetworkSettings(ComboBox comboBox)
        {
            if (!(comboBox.SelectedItem is string selectedName))
            {
                return -1;
            }
            var networkSettings = DatabaseReader.GetEntityList("NetworkSetting").OfType<NetworkSetting>().ToList();
            var networkSetting = networkSettings.FirstOrDefault(u => u.Ip == selectedName);
            return networkSetting != null ? networkSetting.Id : -1;
        }

        private int GetIdFromComboBoxProgramm(ComboBox comboBox)
        {
            if (!(comboBox.SelectedItem is string selectedName))
            {
                return -1;
            }
            var programms = DatabaseReader.GetEntityList("Programm").OfType<Programm>().ToList();
            var programm = programms.FirstOrDefault(u => u.Name == selectedName);
            return programm != null ? programm.Id : -1;
        }

        private int GetIdFromComboBoxConsumable(ComboBox comboBox)
        {
            if (!(comboBox.SelectedItem is string selectedName))
            {
                return -1;
            }
            var consumables = DatabaseReader.GetEntityList("Consumable").OfType<Consumable>().ToList();
            var consumable = consumables.FirstOrDefault(u => u.Name == selectedName);
            return consumable != null ? consumable.Id : -1;
        }
    }
}