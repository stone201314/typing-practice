using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace TypingPractice
{
    public partial class PracticeWindow : Window
    {
        private readonly List<WordItem> _words;
        private int _currentIndex = 0;
        private int _correctCount = 0;
        private string _currentInput = "";
        private bool _showingResult = false;
        
        public PracticeWindow()
        {
            InitializeComponent();
            
            // 内置词库
            _words = new List<WordItem>
            {
                new() { Word = "yi", Display = "一", Pinyin = "yī", Meaning = "数字1" },
                new() { Word = "er", Display = "二", Pinyin = "èr", Meaning = "数字2" },
                new() { Word = "san", Display = "三", Pinyin = "sān", Meaning = "数字3" },
                new() { Word = "si", Display = "四", Pinyin = "sì", Meaning = "数字4" },
                new() { Word = "wu", Display = "五", Pinyin = "wǔ", Meaning = "数字5" },
                new() { Word = "apple", Display = "apple", Pinyin = "", Meaning = "苹果" },
                new() { Word = "book", Display = "book", Pinyin = "", Meaning = "书" },
                new() { Word = "cat", Display = "cat", Pinyin = "", Meaning = "猫" },
                new() { Word = "dog", Display = "dog", Pinyin = "", Meaning = "狗" },
                new() { Word = "fish", Display = "fish", Pinyin = "", Meaning = "鱼" },
            };
            
            ShowCurrentWord();
            this.Focus();
        }
        
        private void ShowCurrentWord()
        {
            if (_currentIndex >= _words.Count)
            {
                // 练习完成
                WordText.Text = "🎉 练习完成！";
                WordText.FontSize = 36;
                MeaningText.Text = "";
                InputBox.Text = "";
                HintText.Text = $"正确率：{_correctCount}/{_words.Count}";
                ResultText.Text = "点击「返回」退出";
                ResultText.Foreground = Brushes.Green;
                return;
            }
            
            var word = _words[_currentIndex];
            WordText.Text = word.Display;
            WordText.FontSize = 56;
            MeaningText.Text = $"含义：{word.Meaning}";
            if (!string.IsNullOrEmpty(word.Pinyin))
            {
                MeaningText.Text += $"  拼音：{word.Pinyin}";
            }
            ProgressText.Text = $"进度：{_currentIndex + 1}/{_words.Count}";
            InputBox.Text = "";
            HintText.Text = "请输入对应的字母，按 Enter 确认";
            ResultText.Text = "";
            _currentInput = "";
            _showingResult = false;
        }
        
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (_currentIndex >= _words.Count) return;
            
            if (_showingResult)
            {
                // 正在显示结果，按任意键继续下一个
                _currentIndex++;
                ShowCurrentWord();
                return;
            }
            
            var word = _words[_currentIndex];
            
            // 处理按键
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                // 检查答案
                if (string.IsNullOrEmpty(_currentInput))
                {
                    // 没有输入，提示
                    ResultText.Text = "❌ 请先输入！";
                    ResultText.Foreground = Brushes.Red;
                    return;
                }
                
                bool isCorrect = _currentInput.ToLower() == word.Word.ToLower();
                
                if (isCorrect)
                {
                    _correctCount++;
                    ResultText.Text = "✅ 正确！按任意键继续";
                    ResultText.Foreground = Brushes.Green;
                }
                else
                {
                    ResultText.Text = $"❌ 错误！正确答案：{word.Word}，按任意键继续";
                    ResultText.Foreground = Brushes.Red;
                }
                
                _showingResult = true;
            }
            else if (e.Key == System.Windows.Input.Key.Back && _currentInput.Length > 0)
            {
                _currentInput = _currentInput[..^1];
                InputBox.Text = _currentInput;
            }
            else if (e.Key == System.Windows.Input.Key.Escape)
            {
                Close();
            }
            else if (e.Key >= System.Windows.Input.Key.A && e.Key <= System.Windows.Input.Key.Z)
            {
                var c = (char)('a' + (e.Key - System.Windows.Input.Key.A));
                _currentInput += c;
                InputBox.Text = _currentInput;
            }
        }
        
        private void OnClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
    
    public class WordItem
    {
        public string Word { get; set; } = "";
        public string Display { get; set; } = "";
        public string Pinyin { get; set; } = "";
        public string Meaning { get; set; } = "";
    }
}
