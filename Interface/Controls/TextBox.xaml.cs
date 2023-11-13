using System.Windows.Controls;

namespace Interface.Controls
{
    /// <summary>
    /// Логика взаимодействия для TextBox.xaml
    /// </summary>
    public partial class TextBox : UserControl
    {
        public TextBox()
        {
            InitializeComponent();
            DataContext = this;
        }
        private string _placeholder;
        private string _background;
        private string _foreground;
        private string _fontFamily;
        public string BindPlaceholder { get { return _placeholder; } set { _placeholder = " " + value; } }
        public string BindBackground { get { return _background; } set { _background = value; } }
        public string BindForeground { get { return _foreground; } set { _foreground = value; } }
        public string BindFontFamily { get { return _fontFamily; } set { _fontFamily = value; } }
    }
}