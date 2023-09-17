using Common;
using System.Windows.Controls;

namespace Interface.Items
{
    /// <summary>
    /// Логика взаимодействия для StatusItem.xaml
    /// </summary>
    public partial class StatusItem : UserControl
    {
        private Status _status;
        public StatusItem(Status status)
        {
            InitializeComponent();
            _status = status;
            LoadData();
        }

        private void LoadData()
        {
            TextBlockName.Text = _status.Name;
        }
    }
}