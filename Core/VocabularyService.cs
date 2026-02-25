using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using TypingPractice.Models;

namespace TypingPractice.Core
{
    public class VocabularyService
    {
        private readonly Dictionary<string, List<VocabularyItem>> _cache = new();
        private readonly Assembly _assembly = Assembly.GetExecutingAssembly();
        
        public List<VocabularyItem> GetVocabulary(string mode, int grade)
        {
            var cacheKey = $"{mode}_{grade}";
            
            if (_cache.ContainsKey(cacheKey))
            {
                return _cache[cacheKey];
            }
            
            var folder = mode.ToLower() == "pinyin" ? "pinyin" : "english";
            var resourceName = $"{folder}grade_{grade}.json";
            
            try
            {
                // 从嵌入式资源加载
                var json = LoadFromResource(resourceName);
                
                if (string.IsNullOrEmpty(json))
                {
                    System.Diagnostics.Debug.WriteLine($"词库资源不存在: {resourceName}");
                    return new List<VocabularyItem>();
                }
                
                var items = JsonConvert.DeserializeObject<List<VocabularyItem>>(json) ?? new List<VocabularyItem>();
                _cache[cacheKey] = items;
                System.Diagnostics.Debug.WriteLine($"加载词库成功: {resourceName}, 共 {items.Count} 词");
                return items;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"加载词汇失败: {ex.Message}");
                return new List<VocabularyItem>();
            }
        }
        
        private string LoadFromResource(string resourceName)
        {
            // 获取所有资源名称
            var resourceNames = _assembly.GetManifestResourceNames();
            
            // 查找匹配的资源
            var fullResourceName = resourceNames.FirstOrDefault(n => n.EndsWith(resourceName, StringComparison.OrdinalIgnoreCase));
            
            if (fullResourceName == null)
            {
                System.Diagnostics.Debug.WriteLine($"找不到资源: {resourceName}");
                System.Diagnostics.Debug.WriteLine($"可用资源: {string.Join(", ", resourceNames)}");
                return null;
            }
            
            using var stream = _assembly.GetManifestResourceStream(fullResourceName);
            if (stream == null) return null;
            
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
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
