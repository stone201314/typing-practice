using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TypingPractice
{
    public partial class PracticeWindow : Window
    {
        private readonly List<WordItem> _words;
        private int _currentIndex = 0;
        private int _correctCount = 0;
        private bool _showingResult = false;
        
        public PracticeWindow()
        {
            InitializeComponent();
            
            // 内置词库 - 只显示汉字，不显示拼音
            _words = new List<WordItem>
            {
                new() { Word = "yi", Display = "一", Meaning = "数字1" },
                new() { Word = "er", Display = "二", Meaning = "数字2" },
                new() { Word = "san", Display = "三", Meaning = "数字3" },
                new() { Word = "si", Display = "四", Meaning = "数字4" },
                new() { Word = "wu", Display = "五", Meaning = "数字5" },
                new() { Word = "liu", Display = "六", Meaning = "数字6" },
                new() { Word = "qi", Display = "七", Meaning = "数字7" },
                new() { Word = "ba", Display = "八", Meaning = "数字8" },
                new() { Word = "jiu", Display = "九", Meaning = "数字9" },
                new() { Word = "shi", Display = "十", Meaning = "数字10" },
            };
            
            ShowCurrentWord();
        }
        
        private void ShowCurrentWord()
        {
            if (_currentIndex >= _words.Count)
            {
                // 练习完成
                WordText.Text = "🎉";
                WordText.FontSize = 64;
                MeaningText.Text = "练习完成！";
                InputBox.Text = "";
                InputBox.IsEnabled = false;
                HintText.Text = $"正确率：{_correctCount}/{_words.Count}";
                ResultText.Text = "点击「返回」退出";
                ResultText.Foreground = Brushes.Green;
                return;
            }
            
            var word = _words[_currentIndex];
            WordText.Text = word.Display;
            WordText.FontSize = 64;
            MeaningText.Text = $"含义：{word.Meaning}";
            ProgressText.Text = $"进度：{_currentIndex + 1}/{_words.Count}";
            InputBox.Text = "";
            InputBox.IsEnabled = true;
            HintText.Text = "输入拼音后按 Enter 确认";
            ResultText.Text = "";
            _showingResult = false;
            
            // 聚焦到输入框
            InputBox.Focus();
        }
        
        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (_currentIndex >= _words.Count) return;
            
            if (e.Key == Key.Enter)
            {
                if (_showingResult)
                {
                    // 正在显示结果，继续下一个
                    _currentIndex++;
                    ShowCurrentWord();
                    return;
                }
                
                var word = _words[_currentIndex];
                var input = InputBox.Text.Trim().ToLower();
                
                if (string.IsNullOrEmpty(input))
                {
                    ResultText.Text = "❌ 请先输入拼音！";
                    ResultText.Foreground = Brushes.Red;
                    return;
                }
                
                // 检查答案
                bool isCorrect = input == word.Word.ToLower();
                
                if (isCorrect)
                {
                    _correctCount++;
                    ResultText.Text = "✅ 正确！按 Enter 继续";
                    ResultText.Foreground = Brushes.Green;
                }
                else
                {
                    ResultText.Text = $"❌ 错误！正确答案：{word.Word}，按 Enter 继续";
                    ResultText.Foreground = Brushes.Red;
                }
                
                _showingResult = true;
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                Close();
            }
        }
        
        private void OnClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
    
    public class WordItem
    {
        public string Word { get; set; } = "";      // 正确的拼音
        public string Display { get; set; } = "";   // 显示的汉字
        public string Meaning { get; set; } = "";   // 含义
    }
}
