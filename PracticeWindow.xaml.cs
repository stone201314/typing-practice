using System.Windows;
using System.Windows.Input;
using TypingPractice.Core;
using TypingPractice.Models;
using TypingPractice.ViewModels;

namespace TypingPractice
{
    public partial class PracticeWindow : Window
    {
        private readonly PracticeViewModel _viewModel;
        
        public PracticeWindow(User user, string mode, UserSettings settings, 
            DatabaseService dbService, VocabularyService vocabService)
        {
            InitializeComponent();
            
            _viewModel = new PracticeViewModel(user, mode, user.Grade, 
                settings.DailyGoal, vocabService, dbService);
            _viewModel.PracticeCompleted += OnPracticeCompleted;
            
            DataContext = _viewModel;
            
            // 聚焦到窗口以接收键盘输入
            Loaded += (s, e) => Focus();
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            
            if (e.Key == Key.Escape)
            {
                Close();
                return;
            }
            
            // 获取按键字符
            var keyChar = GetKeyChar(e.Key);
            if (keyChar.HasValue)
            {
                _viewModel.HandleKey(keyChar.Value);
            }
        }
        
        private char? GetKeyChar(Key key)
        {
            // 字母键
            if (key >= Key.A && key <= Key.Z)
            {
                return (char)('a' + (key - Key.A));
            }
            
            // 数字键
            if (key >= Key.D0 && key <= Key.D9)
            {
                return (char)('0' + (key - Key.D0));
            }
            
            // 空格
            if (key == Key.Space)
            {
                return ' ';
            }
            
            return null;
        }
        
        private void OnPracticeCompleted()
        {
            // 练习完成后的处理
        }
        
        private void OnNextWord(object sender, RoutedEventArgs e)
        {
            _viewModel.NextWordCommand.Execute(null);
        }
        
        private void OnAddToVocabularyBook(object sender, RoutedEventArgs e)
        {
            _viewModel.AddToVocabularyBookCommand.Execute(null);
        }
        
        private void OnClose(object sender, RoutedEventArgs e)
        {
            _viewModel.Cleanup();
            Close();
        }
    }
}
