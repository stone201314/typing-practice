using System.Windows;

namespace TypingPractice
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void OnChineseMode(object sender, RoutedEventArgs e)
        {
            var selectWindow = new SelectTypeWindow("chinese");
            selectWindow.Owner = this;
            selectWindow.ShowDialog();
        }
        
        private void OnEnglishMode(object sender, RoutedEventArgs e)
        {
            var selectWindow = new SelectTypeWindow("english");
            selectWindow.Owner = this;
            selectWindow.ShowDialog();
        }
    }
}
