using System.Windows;
using System.Windows.Media;

namespace TypingPractice
{
    public partial class MainWindow : Window
    {
        private string _mode = "pinyin";
        private string _difficulty = "easy";
        private int _count = 10;
        
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void OnPinyinMode(object sender, RoutedEventArgs e)
        {
            _mode = "pinyin";
            StartPractice();
        }
        
        private void OnEnglishMode(object sender, RoutedEventArgs e)
        {
            _mode = "english";
            StartPractice();
        }
        
        private void OnPoetryMode(object sender, RoutedEventArgs e)
        {
            _mode = "poetry";
            StartPractice();
        }
        
        private void StartPractice()
        {
            var practiceWindow = new PracticeWindow(_mode, _difficulty, _count);
            practiceWindow.Owner = this;
            practiceWindow.ShowDialog();
        }
        
        private void OnEasy(object sender, RoutedEventArgs e)
        {
            _difficulty = "easy";
            UpdateDifficultyButtons();
        }
        
        private void OnMedium(object sender, RoutedEventArgs e)
        {
            _difficulty = "medium";
            UpdateDifficultyButtons();
        }
        
        private void OnHard(object sender, RoutedEventArgs e)
        {
            _difficulty = "hard";
            UpdateDifficultyButtons();
        }
        
        private void OnCount5(object sender, RoutedEventArgs e)
        {
            _count = 5;
            UpdateCountButtons();
        }
        
        private void OnCount10(object sender, RoutedEventArgs e)
        {
            _count = 10;
            UpdateCountButtons();
        }
        
        private void OnCount20(object sender, RoutedEventArgs e)
        {
            _count = 20;
            UpdateCountButtons();
        }
        
        private void OnCount50(object sender, RoutedEventArgs e)
        {
            _count = 50;
            UpdateCountButtons();
        }
        
        private void UpdateDifficultyButtons()
        {
            var gray = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
            var green = new SolidColorBrush(System.Windows.Media.Color.FromRgb(76, 175, 80));
            var white = new SolidColorBrush(System.Windows.Media.Colors.White);
            var black = new SolidColorBrush(System.Windows.Media.Colors.Black);
            
            EasyBtn.Background = gray; EasyBtn.Foreground = black;
            MediumBtn.Background = gray; MediumBtn.Foreground = black;
            HardBtn.Background = gray; HardBtn.Foreground = black;
            
            switch (_difficulty)
            {
                case "easy": EasyBtn.Background = green; EasyBtn.Foreground = white; break;
                case "medium": MediumBtn.Background = green; MediumBtn.Foreground = white; break;
                case "hard": HardBtn.Background = green; HardBtn.Foreground = white; break;
            }
        }
        
        private void UpdateCountButtons()
        {
            var gray = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
            var green = new SolidColorBrush(System.Windows.Media.Color.FromRgb(76, 175, 80));
            var white = new SolidColorBrush(System.Windows.Media.Colors.White);
            var black = new SolidColorBrush(System.Windows.Media.Colors.Black);
            
            Count5Btn.Background = gray; Count5Btn.Foreground = black;
            Count10Btn.Background = gray; Count10Btn.Foreground = black;
            Count20Btn.Background = gray; Count20Btn.Foreground = black;
            Count50Btn.Background = gray; Count50Btn.Foreground = black;
            
            switch (_count)
            {
                case 5: Count5Btn.Background = green; Count5Btn.Foreground = white; break;
                case 10: Count10Btn.Background = green; Count10Btn.Foreground = white; break;
                case 20: Count20Btn.Background = green; Count20Btn.Foreground = white; break;
                case 50: Count50Btn.Background = green; Count50Btn.Foreground = white; break;
            }
        }
    }
}
