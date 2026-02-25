using System;
using System.Diagnostics;

namespace TypingPractice.Core
{
    public class TypingEngine
    {
        private string _targetWord = string.Empty;
        private string _currentInput = string.Empty;
        private readonly Stopwatch _stopwatch = new();
        
        public int CorrectCount { get; private set; }
        public int ErrorCount { get; private set; }
        public int TotalChars { get; private set; }
        
        public event Action<char, bool>? OnKeyPress;
        public event Action? OnWordComplete;
        public event Action<TypingResult>? OnResultReady;
        
        public string TargetWord => _targetWord;
        public string CurrentInput => _currentInput;
        public double CurrentSpeed => _stopwatch.Elapsed.TotalSeconds > 0 
            ? (_currentInput.Length / _stopwatch.Elapsed.TotalMinutes) 
            : 0;
        
        public void StartWord(string word)
        {
            _targetWord = word.ToLower().Replace(" ", "");
            _currentInput = string.Empty;
            CorrectCount = 0;
            ErrorCount = 0;
            TotalChars = _targetWord.Length;
            _stopwatch.Restart();
        }
        
        public bool HandleKeyPress(char key)
        {
            if (string.IsNullOrEmpty(_targetWord)) return false;
            
            var expectedChar = _targetWord[_currentInput.Length];
            var isCorrect = char.ToLower(key) == expectedChar;
            
            if (isCorrect)
            {
                CorrectCount++;
                _currentInput += key;
            }
            else
            {
                ErrorCount++;
            }
            
            OnKeyPress?.Invoke(key, isCorrect);
            
            // Check if word is complete
            if (_currentInput.Length == _targetWord.Length)
            {
                _stopwatch.Stop();
                OnWordComplete?.Invoke();
                
                var result = CompleteWord();
                OnResultReady?.Invoke(result);
            }
            
            return isCorrect;
        }
        
        public TypingResult CompleteWord()
        {
            return new TypingResult
            {
                Word = _targetWord,
                Input = _currentInput,
                IsCorrect = _currentInput.ToLower() == _targetWord.ToLower(),
                CorrectChars = CorrectCount,
                TotalChars = TotalChars,
                TimeSeconds = _stopwatch.Elapsed.TotalSeconds,
                Speed = _stopwatch.Elapsed.TotalSeconds > 0 
                    ? (TotalChars / _stopwatch.Elapsed.TotalMinutes) 
                    : 0
            };
        }
        
        public void Reset()
        {
            _targetWord = string.Empty;
            _currentInput = string.Empty;
            CorrectCount = 0;
            ErrorCount = 0;
            TotalChars = 0;
            _stopwatch.Reset();
        }
    }
    
    // TypingResult moved to Models.cs
}
