using Common;
using Logic;
using System.Windows;
using System.Windows.Controls;

namespace Interface.Pages
{
    /// <summary>
    /// Логика взаимодействия для NetworkSettingEdit.xaml
    /// </summary>
    public partial class NetworkSettingEdit : Page
    {
        private NetworkSetting _networkSetting;

        public NetworkSettingEdit(NetworkSetting networkSetting)
        {
            InitializeComponent();
            _networkSetting = networkSetting;

            ButtonAdd.IsEnabled = false;
            ButtonUpdate.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Update);
            ButtonDelete.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Delete);

            InitializeUI();
        }

        public NetworkSettingEdit()
        {
            InitializeComponent();

            ButtonAdd.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Add);
            ButtonUpdate.IsEnabled = false;
            ButtonDelete.IsEnabled = false;

            InitializeUI();
        }

        private void InitializeUI()
        {
            if (_networkSetting != null)
            {
                PopulateExistData();
            }
        }

        private void PopulateExistData()
        {
            TextBoxIP.Text = _networkSetting.Ip;
            TextBoxMask.Text = _networkSetting.Mask;
            TextBoxGetway.Text = _networkSetting.Gateway;
            TextBoxDns.Text = _networkSetting.Dns;
        }

        private void ModifyEntity(DatabaseModify.Action action)
        {
            var networkSetting = _networkSetting ?? new NetworkSetting();

            networkSetting.Ip = TextBoxIP.Text;
            networkSetting.Mask = TextBoxMask.Text;
            networkSetting.Gateway = TextBoxGetway.Text;
            networkSetting.Dns = TextBoxDns.Text;

            if (networkSetting.Ip == "")
            {
                ShowError("Empty ip");
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

            var (result, error) = DatabaseModify.ModifyEntity(_networkSetting, action);

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
            TextBlockError.Visibility = System.Windows.Visibility.Visible;
        }

        private void ClosePage()
        {
            InterfaceWindows.AdminWindow.ClosePage();
            InterfaceWindows.AdminWindow.UpdateInformation();
            InterfaceWindows.AdminWindow.HideBackButton();
        }
    }
}