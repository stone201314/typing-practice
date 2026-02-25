using System;
using System.Collections.Generic;
using System.Linq;
using TypingPractice.Models;

namespace TypingPractice.Core
{
    public class VocabularyService
    {
        private readonly Dictionary<string, List<VocabularyItem>> _cache = new();
        
        public List<VocabularyItem> GetVocabulary(string mode, int grade)
        {
            var cacheKey = $"{mode}_{grade}";
            
            if (_cache.ContainsKey(cacheKey))
            {
                return _cache[cacheKey];
            }
            
            // 使用内置词库
            List<VocabularyItem> items;
            
            if (mode.ToLower() == "pinyin")
            {
                items = grade switch
                {
                    1 => DefaultVocabulary.GetPinyinGrade1(),
                    _ => DefaultVocabulary.GetPinyinGrade1() // 默认返回一年级词库
                };
            }
            else
            {
                items = grade switch
                {
                    1 => DefaultVocabulary.GetEnglishGrade1(),
                    _ => DefaultVocabulary.GetEnglishGrade1() // 默认返回一年级词库
                };
            }
            
            _cache[cacheKey] = items;
            return items;
        }
        
        public List<VocabularyItem> GetRandomWords(string mode, int grade, int count)
        {
            var allWords = GetVocabulary(mode, grade);
            
            if (allWords.Count == 0)
            {
                return new List<VocabularyItem>();
            }
            
            if (allWords.Count <= count) return allWords;
            
            var random = new Random();
            return allWords.OrderBy(x => random.Next()).Take(count).ToList();
        }
        
        public void ClearCache()
        {
            _cache.Clear();
        }
    }
}
