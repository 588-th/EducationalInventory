using Common;
using Logic;
using System.Windows;
using System.Windows.Controls;

namespace Interface.Pages
{
    /// <summary>
    /// Логика взаимодействия для RouteEdit.xaml
    /// </summary>
    public partial class RouteEdit : Page
    {
        private Route _route;

        public RouteEdit(Route route)
        {
            InitializeComponent();
            _route = route;

            ButtonAdd.IsEnabled = false;
            ButtonUpdate.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Update);
            ButtonDelete.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Delete);

            InitializeUI();
        }

        public RouteEdit()
        {
            InitializeComponent();

            ButtonAdd.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Add);
            ButtonUpdate.IsEnabled = false;
            ButtonDelete.IsEnabled = false;

            InitializeUI();
        }

        private void InitializeUI()
        {
            if (_route != null)
            {
                PopulateExistData();
            }
        }

        private void PopulateExistData()
        {
            TextBoxName.Text = _route.Name;
        }

        private void ModifyEntity(DatabaseModify.Action action)
        {
            var route = _route ?? new Route();

            route.Name = TextBoxName.Text;

            if (route.Name == "")
            {
                ShowError("Empty name");
                return;
            }

            if (action == DatabaseModify.Action.Delete)
            {
                Windows.MessageBox messageBox = new Windows.MessageBox("Delete object", "Are you sure you want to delete the record?");
                if (messageBox.ShowDialog() == false)
                {
                    return;
                }
            }

            var (result, error) = DatabaseModify.ModifyEntity(route, action);

            if (!result)
            {
                ShowError(error);
            }
            else
            {
                ClosePage();
            }
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
        }
    }
}