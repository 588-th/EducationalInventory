using Common;
using Logic;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Interface.Pages
{
    /// <summary>
    /// Логика взаимодействия для InventoryEdit.xaml
    /// </summary>
    public partial class InventoryEdit : Page
    {
        private Inventory _inventory;
        private Equipment _equipment;
        private User _user;

        public InventoryEdit(Inventory inventory)
        {
            InitializeComponent();
            _inventory = inventory;

            ButtonAdd.IsEnabled = false;
            ButtonUpdate.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Update);
            ButtonDelete.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Delete);

            InitializeUI();
        }

        public InventoryEdit(Equipment equipment, User user)
        {
            InitializeComponent();
            _equipment = equipment;
            _user = user;

            ButtonAdd.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Add);
            ButtonUpdate.IsEnabled = false;
            ButtonDelete.IsEnabled = false;

            ComboBoxUser.SelectedItem = _user.SecondName;
            ComboBoxEquipment.SelectedItem = _equipment.Name;

            var equipments = DatabaseReader.GetEntityList("Equipment").OfType<Equipment>().ToList();
            var users = DatabaseReader.GetEntityList("User").OfType<User>().ToList();
            ComboBoxEquipment.ItemsSource = equipments.Where(e => e.ResponsibleUserId == _user.Id).Select(e => e.Name);
            ComboBoxUser.ItemsSource = users.Where(e => e.Id == _user.Id).Select(e => e.SecondName);
        }

        public InventoryEdit()
        {
            InitializeComponent();

            ButtonAdd.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Add);
            ButtonUpdate.IsEnabled = false;
            ButtonDelete.IsEnabled = false;

            ComboBoxUser.SelectedItem = MainWindow.AuthUser?.SecondName;

            InitializeUI();
        }

        private void InitializeUI()
        {
            if (_inventory != null)
            {
                PopulateExistData();
            }
            PopulateComboBoxes();
        }

        private void PopulateExistData()
        {
            TextBoxName.Text = _inventory.Name;
            TextBoxStartDate.Text = _inventory.StartDate;
            TextBoxEndDate.Text = _inventory.EndDate;

            Equipment equipment = DatabaseReader.GetEntity<Equipment>(_inventory.EquipmentId);
            User user = DatabaseReader.GetEntity<User>(_inventory.UserId);

            ComboBoxEquipment.SelectedItem = equipment?.Name;
            if (ComboBoxUser.SelectedItem == null)
            {
                ComboBoxUser.SelectedItem = user?.SecondName;
            }
        }

        private void PopulateComboBoxes()
        {
            var equipments = DatabaseReader.GetEntityList("Equipment").OfType<Equipment>().ToList();
            var users = DatabaseReader.GetEntityList("User").OfType<User>().ToList();

            ComboBoxEquipment.ItemsSource = equipments.Select(e => e.Name);
            ComboBoxUser.ItemsSource = users.Select(u => u.SecondName);
        }

        private void ModifyEntity(DatabaseModify.Action action)
        {
            var inventory = _inventory ?? new Inventory();

            string name = TextBoxName.Text.Trim();
            string startDate = TextBoxStartDate.Text.Trim();
            string endDate = TextBoxEndDate.Text.Trim();
            string equipmentComment = TextBoxEquipmentComment.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                ShowError("Empty name");
                return;
            }

            if (string.IsNullOrEmpty(startDate))
            {
                ShowError("Empty start date");
                return;
            }

            if (string.IsNullOrEmpty(endDate))
            {
                ShowError("Empty end date");
                return;
            }

            if (ComboBoxEquipment.SelectedItem == null)
            {
                ShowError("Empty equipment");
                return;
            }

            if (ComboBoxUser.SelectedItem == null)
            {
                ShowError("Empty user");
                return;
            }

            inventory.Name = name;
            inventory.StartDate = startDate;
            inventory.EndDate = endDate;
            inventory.EquipmentId = GetUserIdFromComboBoxEquipment(ComboBoxEquipment);
            inventory.UserId = GetUserIdFromComboBoxUser(ComboBoxUser);

            if (equipmentComment != "" && inventory.EquipmentId != -1 && action != DatabaseModify.Action.Delete)
            {
                Equipment equipment = DatabaseReader.GetEntity<Equipment>(inventory.EquipmentId);
                equipment.Comment = equipmentComment;
                DatabaseModify.ModifyEntity(equipment, DatabaseModify.Action.Update);
            }

            if (action != DatabaseModify.Action.Delete && inventory.UserId != -1 && inventory.EquipmentId != -1)
            {
                CreateAndAddHistoryInventoryEquipment(inventory);
            }

            if (action == DatabaseModify.Action.Delete)
            {
                Windows.MessageBox messageBox = new Windows.MessageBox("Delete object", "Are you sure you want to delete the record?");
                if (messageBox.ShowDialog() == false)
                {
                    return;
                }
            }

            var (result, error) = DatabaseModify.ModifyEntity(inventory, action);

            if (!result)
            {
                ShowError(error);
            }
            else
            {
                ClosePage();
            }
        }

        private void CreateAndAddHistoryInventoryEquipment(Inventory inventory)
        {
            HistoryInventoryEquipment historyInventoryEquipment = new HistoryInventoryEquipment();
            User user = DatabaseReader.GetEntity<User>(inventory.UserId);
            Equipment equipment = DatabaseReader.GetEntity<Equipment>(inventory.EquipmentId);
            historyInventoryEquipment.User = user.FirstName + " " + user.SecondName;
            historyInventoryEquipment.Inventory = inventory.Name;
            historyInventoryEquipment.Equipment = equipment.Name;
            historyInventoryEquipment.Date = DateTime.Now.ToString();

            DatabaseModify.ModifyEntity(historyInventoryEquipment, DatabaseModify.Action.Add);
        }

        private void ShowError(string error)
        {
            TextBlockError.Text = error;
            TextBlockError.Visibility = Visibility.Visible;
        }

        private void ClosePage()
        {
            InterfaceWindows.AdminWindow.ClosePage();
            InterfaceWindows.AdminWindow.UpdateInformation();
            InterfaceWindows.AdminWindow.HideBackButton();

            InterfaceWindows.EmployeeWindow.ClosePage();
            InterfaceWindows.EmployeeWindow.UpdateInformation();
            InterfaceWindows.EmployeeWindow.HideBackButton();
        }

        private int GetUserIdFromComboBoxEquipment(ComboBox comboBox)
        {
            if (!(comboBox.SelectedItem is string selectedName))
            {
                return -1;
            }
            var equipments = DatabaseReader.GetEntityList("Equipment").OfType<Equipment>().ToList();
            var equipment = equipments.First(e => e.Name == selectedName);
            return equipment != null ? equipment.Id : -1;
        }

        private int GetUserIdFromComboBoxUser(ComboBox comboBox)
        {
            if (!(comboBox.SelectedItem is string selectedName))
            {
                return -1;
            }
            var users = DatabaseReader.GetEntityList("User").OfType<User>().ToList();
            var user = users.First(u => u.SecondName == selectedName);
            return user != null ? user.Id : -1;
        }
    }
}