using Common;
using System.Windows.Controls;

namespace Interface.Items
{
    /// <summary>
    /// Логика взаимодействия для RouteItem.xaml
    /// </summary>
    public partial class RouteItem : UserControl
    {
        private Route _route;
        public RouteItem(Route route)
        {
            InitializeComponent();
            _route = route;
            LoadData();
        }

        private void LoadData()
        {
            TextBlockName.Text = _route.Name;
        }
    }
}