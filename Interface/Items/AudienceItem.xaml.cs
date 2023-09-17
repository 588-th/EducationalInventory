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
            User responsibleUser = Logic.DataBaseLogic.GetEntity<User>(_audience.ResponsibleUserId);
            User temporarilyResponsibleUser = Logic.DataBaseLogic.GetEntity<User>(_audience.ResponsibleUserId);

            TextBlockName.Text = _audience.Name;
            TextBlockShortName.Text = _audience.ShortName;
            TextBlockResponsibleUser.Text = responsibleUser.FirstName + " " + responsibleUser.MiddleName;
            TextBlockTemporarilyResponsibleUser.Text = temporarilyResponsibleUser.FirstName + " " + temporarilyResponsibleUser.MiddleName;
        }
    }
}