using Common;
using System.Windows.Controls;

namespace Interface.Items
{
    /// <summary>
    /// Логика взаимодействия для AudienceItem.xaml
    /// </summary>
    public partial class AudienceItem : UserControl
    {
        private Audience _audience;

        public AudienceItem(Audience audience)
        {
            InitializeComponent();
            _audience = audience;
            LoadData();
        }

        private void LoadData()
        {
            TextBlockName.Text = _audience.Name;
            TextBlockShortName.Text = _audience.ShortName;

            User responsibleUser = Logic.DatabaseReader.GetEntity<User>(_audience.ResponsibleUserId);
            User temporarilyResponsibleUser = Logic.DatabaseReader.GetEntity<User>(_audience.ResponsibleUserId);

            TextBlockResponsibleUser.Text = (_audience.ResponsibleUserId == -1) ? "" : responsibleUser.FirstName + " " + responsibleUser.MiddleName;
            TextBlockTemporarilyResponsibleUser.Text = (_audience.ResponsibleUserId == -1) ? "" : temporarilyResponsibleUser.FirstName + " " + temporarilyResponsibleUser.MiddleName;
        }
    }
}