using System;
using System.Windows;
using TypingPractice.Core;
using TypingPractice.Models;

namespace TypingPractice
{
    public partial class ReportWindow : Window
    {
        private readonly User _user;
        private readonly DatabaseService _dbService;
        
        public ReportWindow(User user, DatabaseService dbService)
        {
            InitializeComponent();
            _user = user;
            _dbService = dbService;
            
            LoadReport();
        }
        
        private void LoadReport()
        {
            // 今日统计
            var todayRecords = _dbService.GetPracticeRecords(_user.Id, DateTime.Today, DateTime.Today);
            TodayWordsText.Text = todayRecords.Sum(r => r.WordsCount).ToString();
            TodayDurationText.Text = FormatDuration(todayRecords.Sum(r => r.Duration));
            TodayAccuracyText.Text = todayRecords.Any() 
                ? $"{todayRecords.Average(r => r.Accuracy):F0}%" 
                : "0%";
            
            // 本周统计
            var weekStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            var weekRecords = _dbService.GetPracticeRecords(_user.Id, weekStart, DateTime.Today);
            WeekWordsText.Text = weekRecords.Sum(r => r.WordsCount).ToString();
            WeekDurationText.Text = FormatDuration(weekRecords.Sum(r => r.Duration));
            WeekAccuracyText.Text = weekRecords.Any() 
                ? $"{weekRecords.Average(r => r.Accuracy):F0}%" 
                : "0%";
            
            // 本月统计
            var monthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var monthRecords = _dbService.GetPracticeRecords(_user.Id, monthStart, DateTime.Today);
            MonthWordsText.Text = monthRecords.Sum(r => r.WordsCount).ToString();
            MonthDurationText.Text = FormatDuration(monthRecords.Sum(r => r.Duration));
            MonthAccuracyText.Text = monthRecords.Any() 
                ? $"{monthRecords.Average(r => r.Accuracy):F0}%" 
                : "0%";
        }
        
        private string FormatDuration(int seconds)
        {
            if (seconds < 60) return $"{seconds}秒";
            var minutes = seconds / 60;
            var secs = seconds % 60;
            return secs > 0 ? $"{minutes}分{secs}秒" : $"{minutes}分钟";
        }
        
        private void OnClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
