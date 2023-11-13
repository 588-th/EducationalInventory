using System.Windows;

namespace Interface.Windows
{
    /// <summary>
    /// Логика взаимодействия для MessageBox.xaml
    /// </summary>
    public partial class MessageBox : Window
    {
        public MessageBox(string title, string text)
        {
            InitializeComponent();

            ButtonYes.Click += (_, __) => DialogResult = true;
            ButtonNo.Click += (_, __) => Close();

            LoadData(title, text);
        }

        private void LoadData(string title, string text)
        {
            Title = title;
            TextBlockText.Text = text;
        }
    }
}