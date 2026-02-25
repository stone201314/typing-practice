using System.Windows;
using System.Windows.Media;

namespace TypingPractice
{
    public partial class SelectTypeWindow : Window
    {
        private readonly string _mainMode;
        private string _subType = "type1";
        private string _difficulty = "medium";
        private int _count = 10;
        
        public SelectTypeWindow(string mainMode)
        {
            InitializeComponent();
            
            _mainMode = mainMode;
            
            if (mainMode == "chinese")
            {
                TitleText.Text = "🇨🇳 中文练习";
                Type1Btn.Content = "📝 词组练习";
                Type2Btn.Content = "📜 古诗词练习";
                UpdateDesc();
            }
            else
            {
                TitleText.Text = "🇬🇧 英文练习";
                Type1Btn.Content = "📝 单词练习";
                Type2Btn.Content = "📜 句子练习";
                UpdateDesc();
            }
        }
        
        private void UpdateDesc()
        {
            if (_mainMode == "chinese")
            {
                if (_subType == "type1")
                {
                    DescText.Text = "• 简单：单个汉字（一、二、山、水）\n• 中等：常用词组（你好、谢谢、学习）\n• 困难：成语词组（春暖花开、秋高气爽）";
                }
                else
                {
                    DescText.Text = "• 简单：诗句中的单个汉字\n• 中等：诗句中的词组（床前、明月、地上霜）\n• 困难：完整诗句（床前明月光、疑是地上霜）";
                }
            }
            else
            {
                if (_subType == "type1")
                {
                    DescText.Text = "• 简单：简单单词（cat、dog、book）\n• 中等：常用单词（teacher、student、hospital）\n• 困难：较长单词（beautiful、wonderful、important）";
                }
                else
                {
                    DescText.Text = "• 简单：简单短语（good morning、thank you）\n• 中等：常用句子（how are you、nice to meet you）\n• 困难：较长句子（what is your favorite color）";
                }
            }
        }
        
        private void OnType1(object sender, RoutedEventArgs e)
        {
            _subType = "type1";
            Type1Btn.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80));
            Type1Btn.Foreground = new SolidColorBrush(Colors.White);
            if (_mainMode == "chinese")
            {
                Type2Btn.Background = new SolidColorBrush(Color.FromRgb(156, 39, 176));
            }
            else
            {
                Type2Btn.Background = new SolidColorBrush(Color.FromRgb(156, 39, 176));
            }
            Type2Btn.Foreground = new SolidColorBrush(Colors.White);
            UpdateDesc();
        }
        
        private void OnType2(object sender, RoutedEventArgs e)
        {
            _subType = "type2";
            Type2Btn.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80));
            Type2Btn.Foreground = new SolidColorBrush(Colors.White);
            if (_mainMode == "chinese")
            {
                Type1Btn.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80));
            }
            else
            {
                Type1Btn.Background = new SolidColorBrush(Color.FromRgb(33, 150, 243));
            }
            Type1Btn.Foreground = new SolidColorBrush(Colors.White);
            UpdateDesc();
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
            var gray = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            var green = new SolidColorBrush(Color.FromRgb(76, 175, 80));
            var white = new SolidColorBrush(Colors.White);
            var black = new SolidColorBrush(Colors.Black);
            
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
            var gray = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            var green = new SolidColorBrush(Color.FromRgb(76, 175, 80));
            var white = new SolidColorBrush(Colors.White);
            var black = new SolidColorBrush(Colors.Black);
            
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
        
        private void OnStart(object sender, RoutedEventArgs e)
        {
            // 组合模式：chinese_type1, chinese_type2, english_type1, english_type2
            string mode = $"{_mainMode}_{_subType}";
            
            var practiceWindow = new PracticeWindow(mode, _difficulty, _count);
            practiceWindow.Owner = this.Owner;
            practiceWindow.ShowDialog();
            Close();
        }
        
        private void OnBack(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
