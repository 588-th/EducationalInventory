using System;
using System.Windows;
using System.Windows.Controls;

namespace Interface.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();

            ButtonExit.Click += (_, __) => Exit();
            ButtonLoadAudience.Click += (sender, __) => ButtonLoadData_Click(sender);
            ButtonLoadConsumable.Click += (sender, __) => ButtonLoadData_Click(sender);
            ButtonLoadConsumableCharacteristics.Click += (sender, __) => ButtonLoadData_Click(sender);
            ButtonLoadConsumableCharacteristicsValues.Click += (sender, __) => ButtonLoadData_Click(sender);
            ButtonLoadConsumableCharacteristicsValues.Click += (sender, __) => ButtonLoadData_Click(sender);
            ButtonLoadDeveloper.Click += (sender, __) => ButtonLoadData_Click(sender);
            ButtonLoadDeveloper.Click += (sender, __) => ButtonLoadData_Click(sender);
            ButtonLoadEquipmentType.Click += (sender, __) => ButtonLoadData_Click(sender);
            ButtonLoadHistoryAudienceEquipment.Click += (sender, __) => ButtonLoadData_Click(sender);
            ButtonLoadHistoryUserConsumable.Click += (sender, __) => ButtonLoadData_Click(sender);
            ButtonLoadHistoryUserEquipment.Click += (sender, __) => ButtonLoadData_Click(sender);
            ButtonLoadInventory.Click += (sender, __) => ButtonLoadData_Click(sender);
            ButtonLoadModelType.Click += (sender, __) => ButtonLoadData_Click(sender);
            ButtonLoadNetworkSetting.Click += (sender, __) => ButtonLoadData_Click(sender);
            ButtonLoadProgramm.Click += (sender, __) => ButtonLoadData_Click(sender);
            ButtonLoadRoute.Click += (sender, __) => ButtonLoadData_Click(sender);
            ButtonLoadStatus.Click += (sender, __) => ButtonLoadData_Click(sender);
            ButtonLoadUser.Click += (sender, __) => ButtonLoadData_Click(sender);
        }

        private void Exit()
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            Close();
        }

        private void ButtonLoadData_Click(object sender)
        {
            string entitytName;
            if (sender is Interface.Controls.Button button)
            {
                entitytName = button.Tag.ToString();
                OutputHead(entitytName);
                OutputItems(entitytName);
            }
        }

        private void OutputHead(string entitytName)
        {
            TextBlockHead.Text = entitytName;
        }

        private void OutputItems(string entityName)
        {
            StackPanelItems.Children.Clear();

            var itemsList = Logic.DataBaseLogic.GetEntityList(entityName);

            foreach (var item in itemsList)
            {
                UserControl userControl = CreateUserControlForItem(item);
                StackPanelItems.Children.Add(userControl);
            }
        }

        private UserControl CreateUserControlForItem(object item)
        {
            Type itemType = item.GetType();
            string userControlTypeName = $"{itemType.Name}Item";

            Type userControlType = Type.GetType(userControlTypeName);

            if (userControlType == null || !typeof(UserControl).IsAssignableFrom(userControlType))
            {
                throw new InvalidOperationException($"No suitable UserControl found for item type: {itemType.Name}");
            }

            return (UserControl)Activator.CreateInstance(userControlType, item);
        }
    }
}