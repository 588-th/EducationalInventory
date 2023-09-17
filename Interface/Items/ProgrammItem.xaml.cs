using Common;
using System.Windows.Controls;

namespace Interface.Items
{
    /// <summary>
    /// Логика взаимодействия для ProgrammItem.xaml
    /// </summary>
    public partial class ProgrammItem : UserControl
    {
        private Programm _programm;
        public ProgrammItem(Programm programm)
        {
            InitializeComponent();
            _programm = programm;
            LoadData();
        }

        private void LoadData()
        {
            TextBlockName.Text = _programm.Name;
        }
    }
}