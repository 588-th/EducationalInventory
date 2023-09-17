using Common;
using System.Windows.Controls;

namespace Interface.Items
{
    /// <summary>
    /// Логика взаимодействия для DeveloperItem.xaml
    /// </summary>
    public partial class DeveloperItem : UserControl
    {
        private Developer _developer;
        public DeveloperItem(Developer developer)
        {
            InitializeComponent();
            _developer = developer;
            LoadData();
        }

        private void LoadData()
        {
            TextBlockName.Text = _developer.Name;
        }
    }
}