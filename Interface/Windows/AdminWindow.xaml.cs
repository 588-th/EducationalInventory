using Logic;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Interface.Windows
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            InitializeButtonEventHandlers();
        }

        private void InitializeButtonEventHandlers()
        {
            AttachButtonClickHandler(ButtonLoadAudience, "Audience");
            AttachButtonClickHandler(ButtonLoadConsumable, "Consumable");
            AttachButtonClickHandler(ButtonLoadConsumableCharacteristics, "ConsumableCharacteristics");
            AttachButtonClickHandler(ButtonLoadConsumableCharacteristicsValues, "ConsumableCharacteristicsValues");
            AttachButtonClickHandler(ButtonLoadConsumableType, "ConsumableType");
            AttachButtonClickHandler(ButtonLoadDeveloper, "Developer");
            AttachButtonClickHandler(ButtonLoadEquipment, "Equipment");
            AttachButtonClickHandler(ButtonLoadEquipmentType, "EquipmentType");
            AttachButtonClickHandler(ButtonLoadHistoryAudienceEquipment, "HistoryAudienceEquipment");
            AttachButtonClickHandler(ButtonLoadHistoryUserConsumable, "HistoryUserConsumable");
            AttachButtonClickHandler(ButtonLoadHistoryUserEquipment, "HistoryUserEquipment");
            AttachButtonClickHandler(ButtonLoadInventory, "HistoryUserEquipment");
            AttachButtonClickHandler(ButtonLoadModelType, "ModelType");
            AttachButtonClickHandler(ButtonLoadNetworkSetting, "NetworkSetting");
            AttachButtonClickHandler(ButtonLoadProgramm, "Programm");
            AttachButtonClickHandler(ButtonLoadRoute, "Route");
            AttachButtonClickHandler(ButtonLoadStatus, "Status");
            AttachButtonClickHandler(ButtonLoadUser, "User");

            ButtonBack.Click += (_, __) => ClosePage();
            ButtonExit.Click += (_, __) => Exit();
        }

        private void AttachButtonClickHandler(Controls.Button button, string entityName)
        {
            ClosePage();
            button.Click += (_, __) => OutputHead(entityName);
            button.Click += (_, __) => OutputItems(entityName);
        }

        private void Exit()
        {
            var authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            Close();
        }

        private void OutputHead(string entityName)
        {
            TextBlockHead.Text = entityName;
        }

        private void OutputItems(string entityName)
        {
            StackPanelItems.Children.Clear();
            var emptyItem = new Items.EmptyItem();
            StackPanelItems.Children.Add(emptyItem);

            try
            {
                var itemsList = DataBaseLogic.GetEntityList(entityName);

                if (itemsList.Count > 0)
                {
                    emptyItem.MouseDoubleClick += (sender, e) =>
                    {
                        Page page = CreatePageForItem(itemsList[0]);
                        mainFrame.Navigate(page);
                    };

                    foreach (var item in itemsList)
                    {
                        var userControl = CreateUserControlForItem(item);
                        StackPanelItems.Children.Add(userControl);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private UserControl CreateUserControlForItem(object item)
        {
            var itemType = item.GetType();
            string userControlTypeName = $"{itemType.Name}Item";
            var userControlType = Type.GetType($"Interface.Items.{userControlTypeName}");

            if (userControlType != null && typeof(UserControl).IsAssignableFrom(userControlType))
            {
                return (UserControl)Activator.CreateInstance(userControlType, item);
            }
            else
            {
                throw new InvalidOperationException($"No suitable UserControl found for item type: {itemType.Name}");
            }
        }

        private Page CreatePageForItem(object item)
        {
            var itemType = item.GetType();
            string pageTypeName = $"{itemType.Name}Edit";
            var pageType = Type.GetType($"Interface.Pages.{pageTypeName}");

            if (pageType != null && typeof(Page).IsAssignableFrom(pageType))
            {
                return (Page)Activator.CreateInstance(pageType, item);
            }
            else
            {
                throw new InvalidOperationException($"No suitable Page found for item type: {itemType.Name}");
            }
        }

        void ClosePage()
        {
            if (mainFrame.CanGoBack)
            {
                mainFrame.GoBack();
            }
            else
            {
                mainFrame.Content = null;
            }
        }
    }
}