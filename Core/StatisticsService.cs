using System;
using System.Collections.Generic;
using System.Linq;
using TypingPractice.Models;

namespace TypingPractice.Core
{
    public class StatisticsService
    {
        private readonly List<PracticeRecord> _records = new();
        
        public void AddRecord(PracticeRecord record)
        {
            _records.Add(record);
        }
        
        public List<PracticeRecord> GetRecords(int userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _records.Where(r => r.UserId == userId);
            
            if (startDate.HasValue)
                query = query.Where(r => r.Date >= startDate.Value);
            
            if (endDate.HasValue)
                query = query.Where(r => r.Date <= endDate.Value);
            
            return query.OrderByDescending(r => r.Date).ToList();
        }
        
        public DailyStats GetDailyStats(int userId, DateTime date)
        {
            var dayRecords = _records
                .Where(r => r.UserId == userId && r.Date.Date == date.Date)
                .ToList();
            
            if (!dayRecords.Any())
            {
                return new DailyStats { Date = date };
            }
            
            return new DailyStats
            {
                Date = date,
                TotalWords = dayRecords.Sum(r => r.WordsCount),
                CorrectWords = dayRecords.Sum(r => r.CorrectCount),
                TotalDuration = dayRecords.Sum(r => r.Duration),
                AverageSpeed = dayRecords.Average(r => r.Speed),
                AverageAccuracy = dayRecords.Average(r => r.Accuracy),
                PinyinCount = dayRecords.Where(r => r.Mode == "pinyin").Sum(r => r.WordsCount),
                EnglishCount = dayRecords.Where(r => r.Mode == "english").Sum(r => r.WordsCount)
            };
        }
        
        public WeeklyStats GetWeeklyStats(int userId, DateTime weekStart)
        {
            var weekEnd = weekStart.AddDays(7);
            var weekRecords = _records
                .Where(r => r.UserId == userId && r.Date >= weekStart && r.Date < weekEnd)
                .ToList();
            
            var dailyStats = new List<DailyStats>();
            for (int i = 0; i < 7; i++)
            {
                dailyStats.Add(GetDailyStats(userId, weekStart.AddDays(i)));
            }
            
            return new WeeklyStats
            {
                WeekStart = weekStart,
                DailyStats = dailyStats,
                TotalWords = weekRecords.Sum(r => r.WordsCount),
                TotalDuration = weekRecords.Sum(r => r.Duration),
                AverageSpeed = weekRecords.Any() ? weekRecords.Average(r => r.Speed) : 0,
                AverageAccuracy = weekRecords.Any() ? weekRecords.Average(r => r.Accuracy) : 0
            };
        }
        
        public MonthlyStats GetMonthlyStats(int userId, int year, int month)
        {
            var monthStart = new DateTime(year, month, 1);
            var monthEnd = monthStart.AddMonths(1);
            
            var monthRecords = _records
                .Where(r => r.UserId == userId && r.Date >= monthStart && r.Date < monthEnd)
                .ToList();
            
            return new MonthlyStats
            {
                Year = year,
                Month = month,
                TotalWords = monthRecords.Sum(r => r.WordsCount),
                TotalDuration = monthRecords.Sum(r => r.Duration),
                PracticeDays = monthRecords.Select(r => r.Date.Date).Distinct().Count(),
                AverageSpeed = monthRecords.Any() ? monthRecords.Average(r => r.Speed) : 0,
                AverageAccuracy = monthRecords.Any() ? monthRecords.Average(r => r.Accuracy) : 0
            };
        }
    }
    
    public class DailyStats
    {
        public DateTime Date { get; set; }
        public int TotalWords { get; set; }
        public int CorrectWords { get; set; }
        public int TotalDuration { get; set; } // 秒
        public double AverageSpeed { get; set; }
        public double AverageAccuracy { get; set; }
        public int PinyinCount { get; set; }
        public int EnglishCount { get; set; }
    }
    
    public class WeeklyStats
    {
        public DateTime WeekStart { get; set; }
        public List<DailyStats> DailyStats { get; set; } = new();
        public int TotalWords { get; set; }
        public int TotalDuration { get; set; }
        public double AverageSpeed { get; set; }
        public double AverageAccuracy { get; set; }
    }
    
    public class MonthlyStats
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int TotalWords { get; set; }
        public int TotalDuration { get; set; }
        public int PracticeDays { get; set; }
        public double AverageSpeed { get; set; }
        public double AverageAccuracy { get; set; }
    }
}
