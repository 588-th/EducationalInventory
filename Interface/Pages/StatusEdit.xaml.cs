using Common;
using Logic;
using System.Windows;
using System.Windows.Controls;

namespace Interface.Pages
{
    /// <summary>
    /// Логика взаимодействия для StatusEdit.xaml
    /// </summary>
    public partial class StatusEdit : Page
    {
        private Status _status;

        public StatusEdit(Status status)
        {
            InitializeComponent();
            _status = status;

            ButtonAdd.IsEnabled = false;
            ButtonUpdate.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Update);
            ButtonDelete.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Delete);

            InitializeUI();
        }

        public StatusEdit()
        {
            InitializeComponent();

            ButtonAdd.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Add);
            ButtonUpdate.IsEnabled = false;
            ButtonDelete.IsEnabled = false;

            InitializeUI();
        }

        private void InitializeUI()
        {
            if (_status != null)
            {
                PopulateExistData();
            }
        }

        private void PopulateExistData()
        {
            TextBoxName.Text = _status.Name;
        }

        private void ModifyEntity(DatabaseModify.Action action)
        {
            var status = _status ?? new Status();

            status.Name = TextBoxName.Text;

            if (status.Name == "")
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

            var (result, error) = DatabaseModify.ModifyEntity(status, action);

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