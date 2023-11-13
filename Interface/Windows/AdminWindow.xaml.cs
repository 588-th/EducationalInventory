using Logic;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Interface.Windows
{
    public partial class AdminWindow : Window
    {


        private string currentEntityName = "Audience";
        private string currentEntityNames = "Audiences";

        public AdminWindow()
        {
            InitializeComponent();
            InitializeButtonEventHandlers();
            Closing += ApplicationShutdown;
            OutputEmptyItem(currentEntityName);
            OutputItems(currentEntityName, true);
        }

        private void InitializeButtonEventHandlers()
        {
            var buttons = new[] {
                ButtonLoadAudience, ButtonLoadConsumable, ButtonLoadConsumableCharacteristics, ButtonLoadConsumableCharacteristicsValues,
                ButtonLoadConsumableType, ButtonLoadDeveloper, ButtonLoadEquipment, ButtonLoadEquipmentType, ButtonLoadHistoryInventoryEquipment,
                ButtonLoadHistoryUserConsumable, ButtonLoadHistoryUserEquipment, ButtonLoadInventory, ButtonLoadModelType, ButtonLoadNetworkSetting,
                ButtonLoadProgramm, ButtonLoadRoute, ButtonLoadStatus, ButtonLoadUser
            };

            foreach (var button in buttons)
            {
                AttachButtonClickHandler(button);
            }

            ButtonBack.Click += (_, __) => ClosePage();
            ButtonBack.Click += (_, __) => UpdateInformation();
            ButtonBack.Click += (_, __) => HideBackButton();
            ButtonExit.Click += (_, __) => Exit();
        }

        private void AttachButtonClickHandler(Controls.Button button)
        {
            button.Click += (_, __) =>
            {
                ClosePage();
                HideBackButton();
                currentEntityName = button.Tag.ToString();
                currentEntityNames = button.ControlContent.ToString();
                CommonButtonClickHandler(currentEntityName);
            };
        }

        private void CommonButtonClickHandler(string entityName = null)
        {
            StackPanelItems.Children.Clear();
            OutputHead();
            if (entityName == "HistoryInventoryEquipment" || entityName == "HistoryUserConsumable" || entityName == "HistoryUserEquipment")
            {
                OutputItems(entityName, false);
            }
            else
            {
                OutputEmptyItem(entityName);
                OutputItems(entityName, true);
            }
        }

        public void UpdateInformation()
        {
            StackPanelItems.Children.Clear();
            if (currentEntityName == "HistoryInventoryEquipment" || currentEntityName == "HistoryUserConsumable" || currentEntityName == "HistoryUserEquipment")
            {
                OutputItems(currentEntityName, false);
            }
            else
            {
                OutputEmptyItem(currentEntityName);
                OutputItems(currentEntityName, true);
            }
        }

        private void OutputHead()
        {
            TextBlockHead.Text = currentEntityNames;
        }

        private void OutputEmptyItem(string entityName)
        {
            var emptyItem = new Items.EmptyItem();
            emptyItem.MouseUp += (_, __) =>
            {
                mainFrame.Navigate(CreatePageForItem(entityName));
                ShowBackButton();
            };
            StackPanelItems.Children.Add(emptyItem);
        }

        private void OutputItems(string entityName, bool openPage)
        {
            var entities = DatabaseReader.GetEntityList(entityName);
            foreach (var entity in entities)
            {
                var userControl = CreateUserControlForItem(entity);
                if (openPage)
                {
                    userControl.MouseUp += (_, __) =>
                    {
                        mainFrame.Navigate(CreatePageForItem(entity));
                        ShowBackButton();
                    };
                }
                else
                {
                    userControl.MouseUp += (_, __) =>
                    {
                        Window window = CreateWindowForItem(entity);
                        window.Show();
                    };
                }
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

        private Window CreateWindowForItem(object item)
        {
            var itemType = item.GetType();
            var pageType = Type.GetType($"Interface.Windows.AddComment");

            if (pageType != null && typeof(Window).IsAssignableFrom(pageType))
            {
                return (Window)Activator.CreateInstance(pageType, item);
            }
            else
            {
                throw new InvalidOperationException($"No suitable Window found for item type: {itemType.Name}");
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

        private void Exit()
        {
            Closing -= ApplicationShutdown;
            var authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            Hide();
        }

        void ApplicationShutdown(object s, EventArgs e) => Application.Current.Shutdown();
    }
}