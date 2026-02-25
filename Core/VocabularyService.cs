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
                return new List<VocabularyItem>();
            }
            
            try
            {
                var json = File.ReadAllText(filePath);
                var items = JsonConvert.DeserializeObject<List<VocabularyItem>>(json) ?? new List<VocabularyItem>();
                _cache[cacheKey] = items;
                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"加载词汇失败: {ex.Message}");
                return new List<VocabularyItem>();
            }
        }
        
        // 新增：获取扩展词库（唐诗、宋词、短语）
        public List<VocabularyItem> GetExtendedVocabulary(string type)
        {
            var cacheKey = $"extended_{type}";
            
            if (_cache.ContainsKey(cacheKey))
            {
                return _cache[cacheKey];
            }
            
            string folder, fileName;
            
            switch (type.ToLower())
            {
                case "tangshi":
                case "唐诗":
                    folder = "pinyin";
                    fileName = "tangshi_300.json";
                    break;
                case "songci":
                case "宋词":
                    folder = "pinyin";
                    fileName = "songci_300.json";
                    break;
                case "phrases":
                case "短语":
                    folder = "english";
                    fileName = "phrases.json";
                    break;
                default:
                    return new List<VocabularyItem>();
            }
            
            var filePath = Path.Combine(_vocabularyPath, folder, fileName);
            
            if (!File.Exists(filePath))
            {
                return new List<VocabularyItem>();
            }
            
            try
            {
                var json = File.ReadAllText(filePath);
                var items = JsonConvert.DeserializeObject<List<VocabularyItem>>(json) ?? new List<VocabularyItem>();
                _cache[cacheKey] = items;
                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"加载扩展词汇失败: {ex.Message}");
                return new List<VocabularyItem>();
            }
        }
        
        // 新增：获取所有可用词库类型
        public List<string> GetAvailableVocabularyTypes()
        {
            var types = new List<string>();
            
            // 检查年级词库
            for (int i = 1; i <= 9; i++)
            {
                var pinyinPath = Path.Combine(_vocabularyPath, "pinyin", $"grade_{i}.json");
                var englishPath = Path.Combine(_vocabularyPath, "english", $"grade_{i}.json");
                
                if (File.Exists(pinyinPath)) types.Add($"拼音-年级{i}");
                if (File.Exists(englishPath)) types.Add($"英语-年级{i}");
            }
            
            // 检查扩展词库
            var tangshiPath = Path.Combine(_vocabularyPath, "pinyin", "tangshi_300.json");
            var songciPath = Path.Combine(_vocabularyPath, "pinyin", "songci_300.json");
            var phrasesPath = Path.Combine(_vocabularyPath, "english", "phrases.json");
            
            if (File.Exists(tangshiPath)) types.Add("唐诗三百首");
            if (File.Exists(songciPath)) types.Add("宋词三百首");
            if (File.Exists(phrasesPath)) types.Add("英语常用短语");
            
            return types;
        }
        
        public List<VocabularyItem> GetRandomWords(string mode, int grade, int count)
        {
            var allWords = GetVocabulary(mode, grade);
            
            if (allWords.Count == 0) return new List<VocabularyItem>();
            if (allWords.Count <= count) return allWords;
            
            var random = new Random();
            return allWords.OrderBy(x => random.Next()).Take(count).ToList();
        }
        
        // 新增：从扩展词库随机获取
        public List<VocabularyItem> GetRandomWordsFromExtended(string type, int count)
        {
            var allWords = GetExtendedVocabulary(type);
            
            if (allWords.Count == 0) return new List<VocabularyItem>();
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
