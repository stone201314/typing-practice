using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TypingPractice
{
    public partial class PracticeWindow : Window
    {
        private readonly List<WordItem> _words;
        private int _currentIndex = 0;
        private int _correctCount = 0;
        private bool _showingResult = false;
        
        public PracticeWindow()
        {
            InitializeComponent();
            
            // 完整词库
            _words = GetFullVocabulary();
            
            ShowCurrentWord();
        }
        
        private List<WordItem> GetFullVocabulary()
        {
            return new List<WordItem>
            {
                // 数字
                new() { Word = "yi", Display = "一", Meaning = "数字1" },
                new() { Word = "er", Display = "二", Meaning = "数字2" },
                new() { Word = "san", Display = "三", Meaning = "数字3" },
                new() { Word = "si", Display = "四", Meaning = "数字4" },
                new() { Word = "wu", Display = "五", Meaning = "数字5" },
                new() { Word = "liu", Display = "六", Meaning = "数字6" },
                new() { Word = "qi", Display = "七", Meaning = "数字7" },
                new() { Word = "ba", Display = "八", Meaning = "数字8" },
                new() { Word = "jiu", Display = "九", Meaning = "数字9" },
                new() { Word = "shi", Display = "十", Meaning = "数字10" },
                new() { Word = "bai", Display = "百", Meaning = "数字100" },
                new() { Word = "qian", Display = "千", Meaning = "数字1000" },
                new() { Word = "wan", Display = "万", Meaning = "数字10000" },
                
                // 颜色
                new() { Word = "hong", Display = "红", Meaning = "红色" },
                new() { Word = "huang", Display = "黄", Meaning = "黄色" },
                new() { Word = "lan", Display = "蓝", Meaning = "蓝色" },
                new() { Word = "lv", Display = "绿", Meaning = "绿色" },
                new() { Word = "bai", Display = "白", Meaning = "白色" },
                new() { Word = "hei", Display = "黑", Meaning = "黑色" },
                
                // 动物
                new() { Word = "ma", Display = "马", Meaning = "动物：马" },
                new() { Word = "niu", Display = "牛", Meaning = "动物：牛" },
                new() { Word = "yang", Display = "羊", Meaning = "动物：羊" },
                new() { Word = "zhu", Display = "猪", Meaning = "动物：猪" },
                new() { Word = "gou", Display = "狗", Meaning = "动物：狗" },
                new() { Word = "mao", Display = "猫", Meaning = "动物：猫" },
                new() { Word = "ji", Display = "鸡", Meaning = "动物：鸡" },
                new() { Word = "ya", Display = "鸭", Meaning = "动物：鸭" },
                new() { Word = "yu", Display = "鱼", Meaning = "动物：鱼" },
                new() { Word = "niao", Display = "鸟", Meaning = "动物：鸟" },
                new() { Word = "chong", Display = "虫", Meaning = "动物：虫" },
                new() { Word = "hu", Display = "虎", Meaning = "动物：老虎" },
                new() { Word = "long", Display = "龙", Meaning = "动物：龙" },
                new() { Word = "she", Display = "蛇", Meaning = "动物：蛇" },
                new() { Word = "shu", Display = "鼠", Meaning = "动物：老鼠" },
                new() { Word = "tu", Display = "兔", Meaning = "动物：兔子" },
                
                // 植物
                new() { Word = "cao", Display = "草", Meaning = "植物：草" },
                new() { Word = "hua", Display = "花", Meaning = "植物：花" },
                new() { Word = "shu", Display = "树", Meaning = "植物：树" },
                new() { Word = "ye", Display = "叶", Meaning = "植物：叶子" },
                new() { Word = "guo", Display = "果", Meaning = "植物：水果" },
                new() { Word = "mi", Display = "米", Meaning = "食物：米" },
                new() { Word = "mian", Display = "面", Meaning = "食物：面" },
                
                // 身体
                new() { Word = "tou", Display = "头", Meaning = "身体：头" },
                new() { Word = "shou", Display = "手", Meaning = "身体：手" },
                new() { Word = "zu", Display = "足", Meaning = "身体：脚" },
                new() { Word = "mu", Display = "目", Meaning = "身体：眼睛" },
                new() { Word = "er", Display = "耳", Meaning = "身体：耳朵" },
                new() { Word = "kou", Display = "口", Meaning = "身体：嘴巴" },
                new() { Word = "bi", Display = "鼻", Meaning = "身体：鼻子" },
                new() { Word = "xin", Display = "心", Meaning = "身体：心脏" },
                
                // 家庭
                new() { Word = "ren", Display = "人", Meaning = "人物：人" },
                new() { Word = "nan", Display = "男", Meaning = "性别：男" },
                new() { Word = "nv", Display = "女", Meaning = "性别：女" },
                new() { Word = "fu", Display = "父", Meaning = "家庭：父亲" },
                new() { Word = "mu", Display = "母", Meaning = "家庭：母亲" },
                new() { Word = "zi", Display = "子", Meaning = "家庭：儿子" },
                new() { Word = "nv", Display = "女", Meaning = "家庭：女儿" },
                new() { Word = "xiong", Display = "兄", Meaning = "家庭：哥哥" },
                new() { Word = "di", Display = "弟", Meaning = "家庭：弟弟" },
                new() { Word = "jie", Display = "姐", Meaning = "家庭：姐姐" },
                new() { Word = "mei", Display = "妹", Meaning = "家庭：妹妹" },
                
                // 自然
                new() { Word = "tian", Display = "天", Meaning = "自然：天空" },
                new() { Word = "di", Display = "地", Meaning = "自然：大地" },
                new() { Word = "ri", Display = "日", Meaning = "自然：太阳" },
                new() { Word = "yue", Display = "月", Meaning = "自然：月亮" },
                new() { Word = "xing", Display = "星", Meaning = "自然：星星" },
                new() { Word = "yun", Display = "云", Meaning = "自然：云" },
                new() { Word = "feng", Display = "风", Meaning = "自然：风" },
                new() { Word = "yu", Display = "雨", Meaning = "自然：雨" },
                new() { Word = "xue", Display = "雪", Meaning = "自然：雪" },
                new() { Word = "shan", Display = "山", Meaning = "自然：山" },
                new() { Word = "he", Display = "河", Meaning = "自然：河流" },
                new() { Word = "hai", Display = "海", Meaning = "自然：大海" },
                new() { Word = "huo", Display = "火", Meaning = "自然：火" },
                new() { Word = "shui", Display = "水", Meaning = "自然：水" },
                new() { Word = "tu", Display = "土", Meaning = "自然：土地" },
                new() { Word = "jin", Display = "金", Meaning = "自然：金属" },
                new() { Word = "mu", Display = "木", Meaning = "自然：木头" },
                new() { Word = "shi", Display = "石", Meaning = "自然：石头" },
                
                // 方位
                new() { Word = "dong", Display = "东", Meaning = "方位：东方" },
                new() { Word = "xi", Display = "西", Meaning = "方位：西方" },
                new() { Word = "nan", Display = "南", Meaning = "方位：南方" },
                new() { Word = "bei", Display = "北", Meaning = "方位：北方" },
                new() { Word = "shang", Display = "上", Meaning = "方位：上面" },
                new() { Word = "xia", Display = "下", Meaning = "方位：下面" },
                new() { Word = "zuo", Display = "左", Meaning = "方位：左边" },
                new() { Word = "you", Display = "右", Meaning = "方位：右边" },
                new() { Word = "qian", Display = "前", Meaning = "方位：前面" },
                new() { Word = "hou", Display = "后", Meaning = "方位：后面" },
                new() { Word = "li", Display = "里", Meaning = "方位：里面" },
                new() { Word = "wai", Display = "外", Meaning = "方位：外面" },
                
                // 时间
                new() { Word = "nian", Display = "年", Meaning = "时间：年" },
                new() { Word = "yue", Display = "月", Meaning = "时间：月" },
                new() { Word = "ri", Display = "日", Meaning = "时间：日" },
                new() { Word = "shi", Display = "时", Meaning = "时间：时" },
                new() { Word = "fen", Display = "分", Meaning = "时间：分" },
                new() { Word = "miao", Display = "秒", Meaning = "时间：秒" },
                new() { Word = "zao", Display = "早", Meaning = "时间：早晨" },
                new() { Word = "wan", Display = "晚", Meaning = "时间：晚上" },
                new() { Word = "jin", Display = "今", Meaning = "时间：今天" },
                new() { Word = "ming", Display = "明", Meaning = "时间：明天" },
                new() { Word = "zuo", Display = "昨", Meaning = "时间：昨天" },
                new() { Word = "chun", Display = "春", Meaning = "季节：春天" },
                new() { Word = "xia", Display = "夏", Meaning = "季节：夏天" },
                new() { Word = "qiu", Display = "秋", Meaning = "季节：秋天" },
                new() { Word = "dong", Display = "冬", Meaning = "季节：冬天" },
                
                // 常用动词
                new() { Word = "chi", Display = "吃", Meaning = "动作：吃" },
                new() { Word = "he", Display = "喝", Meaning = "动作：喝" },
                new() { Word = "shui", Display = "睡", Meaning = "动作：睡觉" },
                new() { Word = "zuo", Display = "坐", Meaning = "动作：坐" },
                new() { Word = "zhan", Display = "站", Meaning = "动作：站" },
                new() { Word = "zou", Display = "走", Meaning = "动作：走" },
                new() { Word = "pao", Display = "跑", Meaning = "动作：跑" },
                new() { Word = "tiao", Display = "跳", Meaning = "动作：跳" },
                new() { Word = "fei", Display = "飞", Meaning = "动作：飞" },
                new() { Word = "you", Display = "游", Meaning = "动作：游泳" },
                new() { Word = "kan", Display = "看", Meaning = "动作：看" },
                new() { Word = "ting", Display = "听", Meaning = "动作：听" },
                new() { Word = "shuo", Display = "说", Meaning = "动作：说" },
                new() { Word = "du", Display = "读", Meaning = "动作：读" },
                new() { Word = "xie", Display = "写", Meaning = "动作：写" },
                new() { Word = "hua", Display = "画", Meaning = "动作：画" },
                new() { Word = "chang", Display = "唱", Meaning = "动作：唱" },
                new() { Word = "wan", Display = "玩", Meaning = "动作：玩" },
                new() { Word = "xue", Display = "学", Meaning = "动作：学习" },
                new() { Word = "jiao", Display = "教", Meaning = "动作：教" },
            };
        }
        
        private void ShowCurrentWord()
        {
            if (_currentIndex >= _words.Count)
            {
                // 练习完成
                WordText.Text = "🎉";
                WordText.FontSize = 72;
                MeaningText.Text = "恭喜！全部完成！";
                InputBox.Text = "";
                InputBox.IsEnabled = false;
                HintText.Text = $"正确率：{_correctCount}/{_words.Count}";
                ResultText.Text = "点击「返回主界面」退出";
                ResultText.Foreground = Brushes.Green;
                return;
            }
            
            var word = _words[_currentIndex];
            WordText.Text = word.Display;
            WordText.FontSize = 72;
            MeaningText.Text = word.Meaning;
            ProgressText.Text = $"进度：{_currentIndex + 1}/{_words.Count}";
            InputBox.Text = "";
            InputBox.IsEnabled = true;
            HintText.Text = "输入拼音后按 Enter 确认";
            ResultText.Text = "";
            _showingResult = false;
            
            // 聚焦到输入框
            InputBox.Focus();
        }
        
        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (_currentIndex >= _words.Count) return;
            
            if (e.Key == Key.Enter)
            {
                if (_showingResult)
                {
                    // 正在显示结果，继续下一个
                    _currentIndex++;
                    ShowCurrentWord();
                    return;
                }
                
                var word = _words[_currentIndex];
                var input = InputBox.Text.Trim().ToLower();
                
                if (string.IsNullOrEmpty(input))
                {
                    ResultText.Text = "❌ 请先输入拼音！";
                    ResultText.Foreground = Brushes.Red;
                    return;
                }
                
                // 检查答案
                bool isCorrect = input == word.Word.ToLower();
                
                if (isCorrect)
                {
                    _correctCount++;
                    ResultText.Text = "✅ 正确！按 Enter 继续";
                    ResultText.Foreground = Brushes.Green;
                }
                else
                {
                    ResultText.Text = $"❌ 错误！正确答案：{word.Word}，按 Enter 继续";
                    ResultText.Foreground = Brushes.Red;
                }
                
                _showingResult = true;
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                Close();
            }
        }
        
        private void OnClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
    
    public class WordItem
    {
        public string Word { get; set; } = "";      // 正确的拼音
        public string Display { get; set; } = "";   // 显示的汉字
        public string Meaning { get; set; } = "";   // 含义
    }
}
