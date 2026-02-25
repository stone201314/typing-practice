using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TypingPractice.Models;

namespace TypingPractice.Core
{
    public class VocabularyService
    {
        private readonly string _vocabularyPath;
        private Dictionary<string, List<VocabularyItem>> _cache = new();
        
        public VocabularyService(string basePath)
        {
            _vocabularyPath = Path.Combine(basePath, "Data", "Vocabulary");
        }
        
        public List<VocabularyItem> GetVocabulary(string mode, int grade)
        {
            var cacheKey = $"{mode}_{grade}";
            
            if (_cache.ContainsKey(cacheKey))
            {
                return _cache[cacheKey];
            }
            
            var folder = mode.ToLower() == "pinyin" ? "pinyin" : "english";
            var filePath = Path.Combine(_vocabularyPath, folder, $"grade_{grade}.json");
            
            if (!File.Exists(filePath))
            {
                System.Diagnostics.Debug.WriteLine($"词库文件不存在: {filePath}");
                return new List<VocabularyItem>();
            }
            
            try
            {
                var json = File.ReadAllText(filePath);
                var items = JsonConvert.DeserializeObject<List<VocabularyItem>>(json) ?? new List<VocabularyItem>();
                _cache[cacheKey] = items;
                System.Diagnostics.Debug.WriteLine($"加载词库成功: {filePath}, 共 {items.Count} 词");
                return items;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"加载词汇失败: {ex.Message}");
                return new List<VocabularyItem>();
            }
        }
        
        public List<VocabularyItem> GetRandomWords(string mode, int grade, int count)
        {
            var allWords = GetVocabulary(mode, grade);
            
            if (allWords.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine($"词库为空: mode={mode}, grade={grade}");
                return new List<VocabularyItem>();
            }
            
            if (allWords.Count <= count) return allWords;
            
            var random = new Random();
            return allWords.OrderBy(x => random.Next()).Take(count).ToList();
        }
        
        public VocabularyItem? GetWord(string mode, int grade, string word)
        {
            var allWords = GetVocabulary(mode, grade);
            return allWords.FirstOrDefault(w => w.Word.Equals(word, StringComparison.OrdinalIgnoreCase));
        }
        
        public void ClearCache()
        {
            _cache.Clear();
        }
    }
}
