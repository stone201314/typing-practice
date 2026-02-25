using System.Windows;

namespace TypingPractice
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void OnStartPractice(object sender, RoutedEventArgs e)
        {
            var practiceWindow = new PracticeWindow();
            practiceWindow.Owner = this;
            practiceWindow.ShowDialog();
        }
    }
}
