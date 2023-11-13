using Common;
using Logic;
using System.Windows;
using System.Windows.Controls;

namespace Interface.Pages
{
    /// <summary>
    /// Логика взаимодействия для ModelTypeEdit.xaml
    /// </summary>
    public partial class ModelTypeEdit : Page
    {
        private ModelType _modelType;

        public ModelTypeEdit(ModelType modelType)
        {
            InitializeComponent();
            _modelType = modelType;

            ButtonAdd.IsEnabled = false;
            ButtonUpdate.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Update);
            ButtonDelete.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Delete);

            InitializeUI();
        }

        public ModelTypeEdit()
        {
            InitializeComponent();

            ButtonAdd.Click += (_, __) => ModifyEntity(DatabaseModify.Action.Add);
            ButtonUpdate.IsEnabled = false;
            ButtonDelete.IsEnabled = false;

            InitializeUI();
        }

        private void InitializeUI()
        {
            if (_modelType != null)
            {
                PopulateExistData();
            }
        }

        private void PopulateExistData()
        {
            TextBoxName.Text = _modelType.Name;
            TextBoxCode.Text = _modelType.Code;
        }

        private void ModifyEntity(DatabaseModify.Action action)
        {
            var modelType = _modelType ?? new ModelType();

            modelType.Name = TextBoxName.Text;
            modelType.Code = TextBoxCode.Text;

            if (modelType.Name == "")
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

            var (result, error) = DatabaseModify.ModifyEntity(modelType, action);

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