using Logic;
using System;
using System.Text;
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
            Closing += (_, __) => Application.Current.Shutdown();
        }

        private string currentEntityName;

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
            AttachButtonClickHandler(ButtonLoadInventory, "Inventory");
            AttachButtonClickHandler(ButtonLoadModelType, "ModelType");
            AttachButtonClickHandler(ButtonLoadNetworkSetting, "NetworkSetting");
            AttachButtonClickHandler(ButtonLoadProgramm, "Programm");
            AttachButtonClickHandler(ButtonLoadRoute, "Route");
            AttachButtonClickHandler(ButtonLoadStatus, "Status");
            AttachButtonClickHandler(ButtonLoadUser, "User");

            ButtonBack.Click += (_, __) => ClosePage();
            ButtonBack.Click += (_, __) => UpdateInformation();
            ButtonBack.Click += (_, __) => HideBackButton();
            ButtonExit.Click += (_, __) => Exit();
        }

        public void UpdateInformation()
        {
            OutputItems(currentEntityName);
        }

        private void AttachButtonClickHandler(Controls.Button button, string entityName)
        {
            button.Click += (_, __) => ClosePage();
            button.Click += (_, __) => HideBackButton();
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
            StringBuilder spacedEntityName = new StringBuilder();
            foreach (char c in entityName)
            {
                if (char.IsUpper(c))
                {
                    spacedEntityName.Append(' ');
                }
                spacedEntityName.Append(c);
            }
            TextBlockHead.Text = spacedEntityName.ToString().Trim();
        }

        private void OutputItems(string entityName)
        {
            currentEntityName = entityName;

            StackPanelItems.Children.Clear();

            if (entityName != "HistoryAudienceEquipment" && entityName != "HistoryUserConsumable" && entityName != "HistoryUserEquipment")
            {
                var emptyItem = new Items.EmptyItem();
                emptyItem.MouseUp += (_, __) =>
                {
                    Page page = CreatePageForItem(entityName);
                    ShowBackButton();
                    mainFrame.Navigate(page);
                };
                StackPanelItems.Children.Add(emptyItem);
            }

            var itemsList = DatabaseReader.GetEntityList(entityName);
            foreach (var item in itemsList)
            {
                var userControl = CreateUserControlForItem(item);
                userControl.MouseUp += (sender, e) =>
                {
                    Page page = CreatePageForItem(item);
                    ShowBackButton();
                    mainFrame.Navigate(page);
                };
                StackPanelItems.Children.Add(userControl);
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

        private Page CreatePageForItem(string entityName)
        {
            string pageTypeName = $"{entityName}Edit";
            var pageType = Type.GetType($"Interface.Pages.{pageTypeName}");

            if (pageType != null && typeof(Page).IsAssignableFrom(pageType))
            {
                return (Page)Activator.CreateInstance(pageType);
            }
            else
            {
                throw new InvalidOperationException($"No suitable Page found for item type: {entityName}");
            }
        }

        public void HideBackButton()
        {
            ButtonBack.Visibility = Visibility.Collapsed;
        }

        private void ShowBackButton()
        {
            ButtonBack.Visibility = Visibility.Visible;
        }

        public void ClosePage()
        {
            mainFrame.Content = null;
        }
    }
}