using System;

namespace TypingPractice.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; } = string.Empty;
        public int Grade { get; set; } = 1;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class UserSettings
    {
        public int UserId { get; set; }
        public bool SoundEnabled { get; set; } = true;
        public bool KeyboardHintEnabled { get; set; } = true;
        public bool DarkMode { get; set; } = false;
        public int DailyGoal { get; set; } = 20;
    }

    public class PracticeRecord
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Mode { get; set; } = "pinyin"; // pinyin / english
        public int WordsCount { get; set; }
        public int CorrectCount { get; set; }
        public double Speed { get; set; } // 字/分钟
        public double Accuracy { get; set; } // 百分比
        public int Duration { get; set; } // 秒
    }

    public class VocabularyItem
    {
        public string Word { get; set; } = string.Empty;
        public string Display { get; set; } = string.Empty;
        public string Pinyin { get; set; } = string.Empty;
        public string Definition { get; set; } = string.Empty;
        public string Example { get; set; } = string.Empty;
        public int Difficulty { get; set; } = 1;
        public string Category { get; set; } = string.Empty;
    }

    public class VocabularyBookItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Word { get; set; } = string.Empty;
        public string Mode { get; set; } = "pinyin";
        public DateTime AddedAt { get; set; } = DateTime.Now;
        public int ReviewCount { get; set; }
        public DateTime? LastReviewAt { get; set; }
    }

    public class TypingResult
    {
        public string Word { get; set; } = string.Empty;
        public string Input { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public int CorrectChars { get; set; }
        public int TotalChars { get; set; }
        public double TimeSeconds { get; set; }
        public double Speed { get; set; } // 字/分钟
    }
}
