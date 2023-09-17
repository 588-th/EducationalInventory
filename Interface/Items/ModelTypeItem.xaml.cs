using Common;
using System.Windows.Controls;

namespace Interface.Items
{
    /// <summary>
    /// Логика взаимодействия для ModelTypeItem.xaml
    /// </summary>
    public partial class ModelTypeItem : UserControl
    {
        private ModelType _modelType;
        public ModelTypeItem(ModelType modelType)
        {
            InitializeComponent();
            _modelType = modelType;
            LoadData();
        }

        private void LoadData()
        {
            TextBlockName.Text = _modelType.Name;
        }
    }
}