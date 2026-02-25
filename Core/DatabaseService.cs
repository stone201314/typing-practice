using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;
using TypingPractice.Models;

namespace TypingPractice.Core
{
    public class DatabaseService
    {
        private readonly string _dbPath;
        private readonly string _connectionString;
        
        public DatabaseService(string basePath)
        {
            _dbPath = Path.Combine(basePath, "Data", "typing.db");
            _connectionString = $"Data Source={_dbPath}";
            
            InitializeDatabase();
        }
        
        private void InitializeDatabase()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_dbPath)!);
            
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            // 创建用户表
            var createUsersTable = @"
                CREATE TABLE IF NOT EXISTS users (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    nickname TEXT NOT NULL,
                    grade INTEGER DEFAULT 1,
                    created_at TEXT DEFAULT CURRENT_TIMESTAMP
                )";
            
            // 创建设置表
            var createSettingsTable = @"
                CREATE TABLE IF NOT EXISTS settings (
                    user_id INTEGER PRIMARY KEY,
                    sound_enabled INTEGER DEFAULT 1,
                    keyboard_hint_enabled INTEGER DEFAULT 1,
                    dark_mode INTEGER DEFAULT 0,
                    daily_goal INTEGER DEFAULT 20,
                    FOREIGN KEY (user_id) REFERENCES users(id)
                )";
            
            // 创建练习记录表
            var createRecordsTable = @"
                CREATE TABLE IF NOT EXISTS practice_records (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    user_id INTEGER NOT NULL,
                    date TEXT NOT NULL,
                    mode TEXT NOT NULL,
                    words_count INTEGER DEFAULT 0,
                    correct_count INTEGER DEFAULT 0,
                    speed REAL DEFAULT 0,
                    accuracy REAL DEFAULT 0,
                    duration INTEGER DEFAULT 0,
                    FOREIGN KEY (user_id) REFERENCES users(id)
                )";
            
            // 创建生词本表
            var createVocabularyBookTable = @"
                CREATE TABLE IF NOT EXISTS vocabulary_book (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    user_id INTEGER NOT NULL,
                    word TEXT NOT NULL,
                    mode TEXT NOT NULL,
                    added_at TEXT DEFAULT CURRENT_TIMESTAMP,
                    review_count INTEGER DEFAULT 0,
                    last_review_at TEXT,
                    FOREIGN KEY (user_id) REFERENCES users(id)
                )";
            
            using var cmd = connection.CreateCommand();
            cmd.CommandText = createUsersTable;
            cmd.ExecuteNonQuery();
            
            cmd.CommandText = createSettingsTable;
            cmd.ExecuteNonQuery();
            
            cmd.CommandText = createRecordsTable;
            cmd.ExecuteNonQuery();
            
            cmd.CommandText = createVocabularyBookTable;
            cmd.ExecuteNonQuery();
        }
        
        #region 用户管理
        
        public User CreateUser(string nickname, int grade = 1)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO users (nickname, grade) VALUES (@nickname, @grade); SELECT last_insert_rowid();";
            cmd.Parameters.AddWithValue("@nickname", nickname);
            cmd.Parameters.AddWithValue("@grade", grade);
            
            var id = Convert.ToInt32(cmd.ExecuteScalar());
            
            // 创建默认设置
            CreateDefaultSettings(id);
            
            return new User { Id = id, Nickname = nickname, Grade = grade };
        }
        
        private void CreateDefaultSettings(int userId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO settings (user_id) VALUES (@userId)";
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.ExecuteNonQuery();
        }
        
        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT id, nickname, grade, created_at FROM users";
            
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32(0),
                    Nickname = reader.GetString(1),
                    Grade = reader.GetInt32(2),
                    CreatedAt = DateTime.Parse(reader.GetString(3))
                });
            }
            
            return users;
        }
        
        public User? GetUser(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT id, nickname, grade, created_at FROM users WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetInt32(0),
                    Nickname = reader.GetString(1),
                    Grade = reader.GetInt32(2),
                    CreatedAt = DateTime.Parse(reader.GetString(3))
                };
            }
            
            return null;
        }
        
        public void UpdateUser(User user)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE users SET nickname = @nickname, grade = @grade WHERE id = @id";
            cmd.Parameters.AddWithValue("@nickname", user.Nickname);
            cmd.Parameters.AddWithValue("@grade", user.Grade);
            cmd.Parameters.AddWithValue("@id", user.Id);
            cmd.ExecuteNonQuery();
        }
        
        public void DeleteUser(int userId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            // 删除关联数据
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM practice_records WHERE user_id = @userId";
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.ExecuteNonQuery();
            
            cmd.CommandText = "DELETE FROM vocabulary_book WHERE user_id = @userId";
            cmd.ExecuteNonQuery();
            
            cmd.CommandText = "DELETE FROM settings WHERE user_id = @userId";
            cmd.ExecuteNonQuery();
            
            cmd.CommandText = "DELETE FROM users WHERE id = @userId";
            cmd.ExecuteNonQuery();
        }
        
        #endregion
        
        #region 设置管理
        
        public UserSettings GetSettings(int userId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT sound_enabled, keyboard_hint_enabled, dark_mode, daily_goal FROM settings WHERE user_id = @userId";
            cmd.Parameters.AddWithValue("@userId", userId);
            
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new UserSettings
                {
                    UserId = userId,
                    SoundEnabled = reader.GetBoolean(0),
                    KeyboardHintEnabled = reader.GetBoolean(1),
                    DarkMode = reader.GetBoolean(2),
                    DailyGoal = reader.GetInt32(3)
                };
            }
            
            return new UserSettings { UserId = userId };
        }
        
        public void UpdateSettings(UserSettings settings)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                UPDATE settings SET 
                    sound_enabled = @sound,
                    keyboard_hint_enabled = @keyboard,
                    dark_mode = @dark,
                    daily_goal = @goal
                WHERE user_id = @userId";
            
            cmd.Parameters.AddWithValue("@sound", settings.SoundEnabled ? 1 : 0);
            cmd.Parameters.AddWithValue("@keyboard", settings.KeyboardHintEnabled ? 1 : 0);
            cmd.Parameters.AddWithValue("@dark", settings.DarkMode ? 1 : 0);
            cmd.Parameters.AddWithValue("@goal", settings.DailyGoal);
            cmd.Parameters.AddWithValue("@userId", settings.UserId);
            cmd.ExecuteNonQuery();
        }
        
        #endregion
        
        #region 练习记录
        
        public void SavePracticeRecord(PracticeRecord record)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO practice_records (user_id, date, mode, words_count, correct_count, speed, accuracy, duration)
                VALUES (@userId, @date, @mode, @words, @correct, @speed, @accuracy, @duration)";
            
            cmd.Parameters.AddWithValue("@userId", record.UserId);
            cmd.Parameters.AddWithValue("@date", record.Date.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@mode", record.Mode);
            cmd.Parameters.AddWithValue("@words", record.WordsCount);
            cmd.Parameters.AddWithValue("@correct", record.CorrectCount);
            cmd.Parameters.AddWithValue("@speed", record.Speed);
            cmd.Parameters.AddWithValue("@accuracy", record.Accuracy);
            cmd.Parameters.AddWithValue("@duration", record.Duration);
            cmd.ExecuteNonQuery();
        }
        
        public List<PracticeRecord> GetPracticeRecords(int userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var records = new List<PracticeRecord>();
            
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            var sql = "SELECT id, user_id, date, mode, words_count, correct_count, speed, accuracy, duration FROM practice_records WHERE user_id = @userId";
            
            if (startDate.HasValue)
                sql += " AND date >= @startDate";
            
            if (endDate.HasValue)
                sql += " AND date <= @endDate";
            
            sql += " ORDER BY date DESC";
            
            using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@userId", userId);
            
            if (startDate.HasValue)
                cmd.Parameters.AddWithValue("@startDate", startDate.Value.ToString("yyyy-MM-dd"));
            
            if (endDate.HasValue)
                cmd.Parameters.AddWithValue("@endDate", endDate.Value.ToString("yyyy-MM-dd"));
            
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                records.Add(new PracticeRecord
                {
                    Id = reader.GetInt32(0),
                    UserId = reader.GetInt32(1),
                    Date = DateTime.Parse(reader.GetString(2)),
                    Mode = reader.GetString(3),
                    WordsCount = reader.GetInt32(4),
                    CorrectCount = reader.GetInt32(5),
                    Speed = reader.GetDouble(6),
                    Accuracy = reader.GetDouble(7),
                    Duration = reader.GetInt32(8)
                });
            }
            
            return records;
        }
        
        #endregion
        
        #region 生词本
        
        public void AddToVocabularyBook(int userId, string word, string mode)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            // 检查是否已存在
            using var checkCmd = connection.CreateCommand();
            checkCmd.CommandText = "SELECT id FROM vocabulary_book WHERE user_id = @userId AND word = @word AND mode = @mode";
            checkCmd.Parameters.AddWithValue("@userId", userId);
            checkCmd.Parameters.AddWithValue("@word", word);
            checkCmd.Parameters.AddWithValue("@mode", mode);
            
            var exists = checkCmd.ExecuteScalar() != null;
            if (exists) return;
            
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO vocabulary_book (user_id, word, mode) VALUES (@userId, @word, @mode)";
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@word", word);
            cmd.Parameters.AddWithValue("@mode", mode);
            cmd.ExecuteNonQuery();
        }
        
        public void RemoveFromVocabularyBook(int itemId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM vocabulary_book WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", itemId);
            cmd.ExecuteNonQuery();
        }
        
        public List<VocabularyBookItem> GetVocabularyBook(int userId, string? mode = null)
        {
            var items = new List<VocabularyBookItem>();
            
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            var sql = "SELECT id, user_id, word, mode, added_at, review_count, last_review_at FROM vocabulary_book WHERE user_id = @userId";
            
            if (!string.IsNullOrEmpty(mode))
                sql += " AND mode = @mode";
            
            sql += " ORDER BY added_at DESC";
            
            using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@userId", userId);
            
            if (!string.IsNullOrEmpty(mode))
                cmd.Parameters.AddWithValue("@mode", mode);
            
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new VocabularyBookItem
                {
                    Id = reader.GetInt32(0),
                    UserId = reader.GetInt32(1),
                    Word = reader.GetString(2),
                    Mode = reader.GetString(3),
                    AddedAt = DateTime.Parse(reader.GetString(4)),
                    ReviewCount = reader.GetInt32(5),
                    LastReviewAt = reader.IsDBNull(6) ? null : DateTime.Parse(reader.GetString(6))
                });
            }
            
            return items;
        }
        
        public void UpdateReviewCount(int itemId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE vocabulary_book SET review_count = review_count + 1, last_review_at = @now WHERE id = @id";
            cmd.Parameters.AddWithValue("@now", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@id", itemId);
            cmd.ExecuteNonQuery();
        }
        
        #endregion
    }
}
