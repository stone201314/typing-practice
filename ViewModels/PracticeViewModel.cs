using System;
using System.Collections.Generic;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TypingPractice.Core;
using TypingPractice.Models;

namespace TypingPractice.ViewModels
{
    public partial class PracticeViewModel : ObservableObject
    {
        private readonly TypingEngine _engine = new();
        private readonly VocabularyService _vocabService;
        private readonly DatabaseService _dbService;
        private readonly User _user;
        private readonly string _mode;
        private readonly List<VocabularyItem> _words;
        private System.Timers.Timer? _timer;
        
        private int _currentIndex;
        private int _correctCount;
        private int _totalCount;
        private DateTime _startTime;
        
        [ObservableProperty]
        private string _targetWord = string.Empty;
        
        [ObservableProperty]
        private string _displayWord = string.Empty;
        
        [ObservableProperty]
        private string _currentInput = string.Empty;
        
        [ObservableProperty]
        private string _progressText = "0/20";
        
        [ObservableProperty]
        private string _speedText = "0 字/分";
        
        [ObservableProperty]
        private string _accuracyText = "100%";
        
        [ObservableProperty]
        private bool _isDefinitionVisible;
        
        [ObservableProperty]
        private string _definitionText = string.Empty;
        
        [ObservableProperty]
        private string _exampleText = string.Empty;
        
        [ObservableProperty]
        private VocabularyItem? _currentWord;
        
        public event Action? PracticeCompleted;
        
        public PracticeViewModel(User user, string mode, int grade, int wordCount, 
            VocabularyService vocabService, DatabaseService dbService)
        {
            _user = user;
            _mode = mode;
            _vocabService = vocabService;
            _dbService = dbService;
            
            _engine.OnKeyPress += OnKeyPress;
            _engine.OnWordComplete += OnWordComplete;
            
            _words = _vocabService.GetRandomWords(mode, grade, wordCount);
            _totalCount = _words.Count;
            
            // 检查词库是否为空
            if (_totalCount == 0)
            {
                ProgressText = "词库为空";
                SpeedText = "请检查词库";
                AccuracyText = $"年级: {grade}";
                return;
            }
            
            _timer = new System.Timers.Timer(100);
            _timer.Elapsed += Timer_Elapsed;
            
            ProgressText = $"0/{_totalCount}";
            StartNextWord();
        }
        
        private void OnKeyPress(char key, bool isCorrect)
        {
            CurrentInput = _engine.CurrentInput;
        }
        
        private void OnWordComplete()
        {
            _correctCount++;
            _timer?.Stop();
            
            if (CurrentWord != null)
            {
                DefinitionText = $"{CurrentWord.Display} {CurrentWord.Pinyin}\n释义：{CurrentWord.Definition}";
                ExampleText = $"例句：{CurrentWord.Example}";
                IsDefinitionVisible = true;
            }
            
            UpdateStats();
        }
        
        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Application.Current?.Dispatcher?.Invoke(() => UpdateStats());
            }
            catch { }
        }
        
        public void HandleKey(char key)
        {
            if (IsDefinitionVisible) return;
            if (string.IsNullOrEmpty(TargetWord)) return;
            if (_words.Count == 0) return;
            
            _engine.HandleKeyPress(key);
        }
        
        [RelayCommand]
        private void NextWord()
        {
            IsDefinitionVisible = false;
            
            _currentIndex++;
            ProgressText = $"{_currentIndex}/{_totalCount}";
            
            if (_currentIndex >= _totalCount)
            {
                Complete();
                return;
            }
            
            StartNextWord();
        }
        
        [RelayCommand]
        private void AddToVocabularyBook()
        {
            if (CurrentWord != null && _user != null)
            {
                _dbService.AddToVocabularyBook(_user.Id, CurrentWord.Word, _mode);
                MessageBox.Show("已添加到生词本");
            }
        }
        
        private void StartNextWord()
        {
            if (_currentIndex >= _words.Count)
            {
                Complete();
                return;
            }
            
            CurrentWord = _words[_currentIndex];
            TargetWord = CurrentWord.Word;
            DisplayWord = _mode == "pinyin" ? CurrentWord.Display : CurrentWord.Word;
            CurrentInput = string.Empty;
            
            _engine.StartWord(TargetWord);
            _startTime = DateTime.Now;
            _timer?.Start();
        }
        
        private void UpdateStats()
        {
            var elapsed = (DateTime.Now - _startTime).TotalMinutes;
            if (elapsed > 0)
            {
                var speed = _engine.CurrentInput.Length / elapsed;
                SpeedText = $"{speed:F0} 字/分";
            }
            
            var total = _correctCount + _engine.ErrorCount;
            if (total > 0)
            {
                var accuracy = (double)_correctCount / total * 100;
                AccuracyText = $"{accuracy:F0}%";
            }
        }
        
        private void Complete()
        {
            _timer?.Stop();
            
            var elapsed = DateTime.Now - _startTime;
            var record = new PracticeRecord
            {
                UserId = _user.Id,
                Date = DateTime.Now,
                Mode = _mode,
                WordsCount = _totalCount,
                CorrectCount = _correctCount,
                Speed = _engine.CurrentSpeed,
                Accuracy = _totalCount > 0 ? (double)_correctCount / _totalCount * 100 : 0,
                Duration = (int)elapsed.TotalSeconds
            };
            
            _dbService.SavePracticeRecord(record);
            PracticeCompleted?.Invoke();
            
            MessageBox.Show($"练习完成！\n正确率：{record.Accuracy:F0}%\n速度：{record.Speed:F0} 字/分");
        }
        
        public void Cleanup()
        {
            _timer?.Stop();
            _timer?.Dispose();
        }
    }
}
