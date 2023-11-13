using Common;
using Logic;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Interface.Windows
{
    public partial class EmployeeWindow : Window
    {
        private string _currentEntityName = "Audience";
        private string _currentEntityNames = "Audiences";

        public EmployeeWindow()
        {
            InitializeComponent();
            InitializeButtonEventHandlers();
            Closing += ApplicationShutdown;
            OutputItems(_currentEntityName);
        }

        private void InitializeButtonEventHandlers()
        {
            var buttons = new[] { ButtonLoadAudience, ButtonLoadConsumable, ButtonLoadEquipment, ButtonLoadInventory };

            foreach (var button in buttons)
            {
                AttachButtonClickHandler(button);
            }

            ButtonBack.Click += (_, __) =>
            {
                HideBackButton();
                ClosePage();
                UpdateInformation();
            };

            ButtonExit.Click += (_, __) => Exit();
        }

        private void AttachButtonClickHandler(Controls.Button button)
        {
            button.Click += (_, __) =>
            {
                _currentEntityName = button.Tag.ToString();
                _currentEntityNames = button.ControlContent.ToString();
                StackPanelItems.Children.Clear();
                HideBackButton();
                ClosePage();
                OutputHead();
                OutputItems(_currentEntityName);
            };
        }

        private void OutputHead()
        {
            TextBlockHead.Text = _currentEntityNames;
        }

        public void UpdateInformation()
        {
            StackPanelItems.Children.Clear();
            OutputItems(_currentEntityName);
        }

        private void OutputItems(string entityName)
        {
            var entities = DatabaseReader.GetEntityList(entityName);

            foreach (var entity in entities)
            {
                var userControl = CreateUserControlForItem(entity);

                StackPanelItems.Children.Add(userControl);

                if (entity is Equipment eq)
                {
                    userControl.MouseUp += (_, __) =>
                    {
                        ShowBackButton();
                        mainFrame.Navigate(new Pages.InventoryEdit(eq, MainWindow.AuthUser));
                        ShowBackButton();
                    };
                }
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