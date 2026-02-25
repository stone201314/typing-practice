using System;
using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TypingPractice.Core;
using TypingPractice.Models;

namespace TypingPractice.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;
        private readonly VocabularyService _vocabService;
        private readonly StatisticsService _statsService;
        
        [ObservableProperty]
        private User? _currentUser;
        
        [ObservableProperty]
        private UserSettings _settings = new();
        
        [ObservableProperty]
        private int _todayProgress;
        
        [ObservableProperty]
        private int _todayGoal = 20;
        
        [ObservableProperty]
        private string _currentMode = "pinyin"; // pinyin / english
        
        [ObservableProperty]
        private ObservableCollection<User> _users = new();
        
        [ObservableProperty]
        private bool _isLoginVisible = true;
        
        [ObservableProperty]
        private bool _isMainVisible;
        
        public MainViewModel(DatabaseService dbService, VocabularyService vocabService, StatisticsService statsService)
        {
            _dbService = dbService;
            _vocabService = vocabService;
            _statsService = statsService;
            
            LoadUsers();
        }
        
        private void LoadUsers()
        {
            Users.Clear();
            foreach (var user in _dbService.GetAllUsers())
            {
                Users.Add(user);
            }
        }
        
        [RelayCommand]
        private void CreateUser(string nickname)
        {
            if (string.IsNullOrWhiteSpace(nickname))
            {
                MessageBox.Show("请输入昵称");
                return;
            }
            
            var user = _dbService.CreateUser(nickname);
            Users.Add(user);
            SelectUser(user);
        }
        
        [RelayCommand]
        private void SelectUser(User user)
        {
            CurrentUser = user;
            Settings = _dbService.GetSettings(user.Id);
            TodayGoal = Settings.DailyGoal;
            
            // 加载今日进度
            var todayRecords = _dbService.GetPracticeRecords(user.Id, DateTime.Today, DateTime.Today);
            TodayProgress = todayRecords.Sum(r => r.WordsCount);
            
            IsLoginVisible = false;
            IsMainVisible = true;
        }
        
        [RelayCommand]
        private void Logout()
        {
            CurrentUser = null;
            IsLoginVisible = true;
            IsMainVisible = false;
        }
        
        [RelayCommand]
        private void SwitchMode(string mode)
        {
            CurrentMode = mode;
        }
        
        public void UpdateSettings(UserSettings settings)
        {
            _dbService.UpdateSettings(settings);
            Settings = settings;
            TodayGoal = settings.DailyGoal;
        }
        
        public void RefreshProgress()
        {
            if (CurrentUser == null) return;
            
            var todayRecords = _dbService.GetPracticeRecords(CurrentUser.Id, DateTime.Today, DateTime.Today);
            TodayProgress = todayRecords.Sum(r => r.WordsCount);
        }
    }
}
