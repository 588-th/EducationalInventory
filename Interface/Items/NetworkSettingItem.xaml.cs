using Common;
using System.Windows.Controls;

namespace Interface.Items
{
    /// <summary>
    /// Логика взаимодействия для NetworkSettingItem.xaml
    /// </summary>
    public partial class NetworkSettingItem : UserControl
    {
        private NetworkSetting _networkSetting;
        public NetworkSettingItem(NetworkSetting networkSetting)
        {
            InitializeComponent();
            _networkSetting = networkSetting;
            LoadData();
        }

        private void LoadData()
        {
            TextBlockIp.Text = _networkSetting.Ip;
            TextBlockMask.Text = _networkSetting.Mask;
            TextBlockGateway.Text = _networkSetting.Gateway;
            TextBlockDns.Text = _networkSetting.Dns;
        }
    }
}