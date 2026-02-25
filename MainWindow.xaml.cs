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
            MessageBox.Show("开始练习功能开发中...\n\n请等待下一版本！", "提示");
        }
    }
}
