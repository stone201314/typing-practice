using System;
using System.Collections.Generic;
using System.Windows;

namespace TypingPractice
{
    public partial class PracticeWindow : Window
    {
        private readonly List<WordItem> _words;
        private int _currentIndex = 0;
        private int _correctCount = 0;
        private string _currentInput = "";
        
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
        }
        
        private void ShowCurrentWord()
        {
            if (_currentIndex >= _words.Count)
            {
                // 练习完成
                ResultText.Text = $"练习完成！\n正确率：{_correctCount}/{_words.Count}";
                WordText.Text = "";
                InputText.Text = "";
                MeaningText.Text = "";
                return;
            }
            
            var word = _words[_currentIndex];
            WordText.Text = word.Display;
            MeaningText.Text = word.Meaning;
            ProgressText.Text = $"{_currentIndex + 1}/{_words.Count}";
            InputText.Text = "";
            _currentInput = "";
        }
        
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (_currentIndex >= _words.Count) return;
            
            var word = _words[_currentIndex];
            
            // 处理按键
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                // 检查答案
                if (_currentInput.ToLower() == word.Word.ToLower())
                {
                    _correctCount++;
                }
                _currentIndex++;
                ShowCurrentWord();
            }
            else if (e.Key == System.Windows.Input.Key.Back && _currentInput.Length > 0)
            {
                _currentInput = _currentInput[..^1];
                InputText.Text = _currentInput;
            }
            else if (e.Key >= System.Windows.Input.Key.A && e.Key <= System.Windows.Input.Key.Z)
            {
                var c = (char)('a' + (e.Key - System.Windows.Input.Key.A));
                _currentInput += c;
                InputText.Text = _currentInput;
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
