using System.Windows;
using System.Windows.Media;

namespace TypingPractice
{
    public partial class MainWindow : Window
    {
        private string _mode = "pinyin";
        private string _difficulty = "medium";
        
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void OnPinyinMode(object sender, RoutedEventArgs e)
        {
            _mode = "pinyin";
            var practiceWindow = new PracticeWindow(_mode, _difficulty);
            practiceWindow.Owner = this;
            practiceWindow.ShowDialog();
        }
        
        private void OnEnglishMode(object sender, RoutedEventArgs e)
        {
            _mode = "english";
            var practiceWindow = new PracticeWindow(_mode, _difficulty);
            practiceWindow.Owner = this;
            practiceWindow.ShowDialog();
        }
        
        private void OnPoetryMode(object sender, RoutedEventArgs e)
        {
            _mode = "poetry";
            var practiceWindow = new PracticeWindow(_mode, _difficulty);
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
        
        private void UpdateDifficultyButtons()
        {
            EasyBtn.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
            EasyBtn.Foreground = new SolidColorBrush(System.Windows.Media.Colors.Black);
            MediumBtn.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
            MediumBtn.Foreground = new SolidColorBrush(System.Windows.Media.Colors.Black);
            HardBtn.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
            HardBtn.Foreground = new SolidColorBrush(System.Windows.Media.Colors.Black);
            
            var green = new SolidColorBrush(System.Windows.Media.Color.FromRgb(76, 175, 80));
            
            if (_difficulty == "easy")
            {
                EasyBtn.Background = green;
                EasyBtn.Foreground = new SolidColorBrush(System.Windows.Media.Colors.White);
            }
            else if (_difficulty == "medium")
            {
                MediumBtn.Background = green;
                MediumBtn.Foreground = new SolidColorBrush(System.Windows.Media.Colors.White);
            }
            else
            {
                HardBtn.Background = green;
                HardBtn.Foreground = new SolidColorBrush(System.Windows.Media.Colors.White);
            }
        }
    }
}
