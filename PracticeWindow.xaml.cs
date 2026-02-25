using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TypingPractice
{
    public partial class PracticeWindow : Window
    {
        private readonly List<WordItem> _allWords;
        private List<WordItem> _words;
        private int _currentIndex = 0;
        private int _correctCount = 0;
        private bool _showingResult = false;
        private readonly string _mode;
        private readonly string _difficulty;
        
        public PracticeWindow(string mode, string difficulty)
        {
            InitializeComponent();
            
            _mode = mode;
            _difficulty = difficulty;
            
            // 获取词库
            _allWords = GetVocabulary(mode);
            
            // 根据难度选择数量
            int count = difficulty switch
            {
                "easy" => 10,
                "medium" => 20,
                "hard" => 50,
                _ => 20
            };
            
            // 随机选择
            var random = new Random();
            _words = new List<WordItem>();
            var shuffled = new List<WordItem>(_allWords);
            shuffled.Sort((a, b) => random.Next() - random.Next());
            
            for (int i = 0; i < Math.Min(count, shuffled.Count); i++)
            {
                _words.Add(shuffled[i]);
            }
            
            // 更新提示文字
            PromptText.Text = mode switch
            {
                "pinyin" => "请输入对应的拼音：",
                "english" => "请输入对应的英文：",
                "poetry" => "请输入对应的诗句拼音：",
                _ => "请输入："
            };
            
            ShowCurrentWord();
        }
        
        private List<WordItem> GetVocabulary(string mode)
        {
            var words = new List<WordItem>();
            
            if (mode == "pinyin")
            {
                // 数字
                words.AddRange(new[] {
                    new WordItem("yi", "一", "数字1"),
                    new WordItem("er", "二", "数字2"),
                    new WordItem("san", "三", "数字3"),
                    new WordItem("si", "四", "数字4"),
                    new WordItem("wu", "五", "数字5"),
                    new WordItem("liu", "六", "数字6"),
                    new WordItem("qi", "七", "数字7"),
                    new WordItem("ba", "八", "数字8"),
                    new WordItem("jiu", "九", "数字9"),
                    new WordItem("shi", "十", "数字10"),
                    new WordItem("bai", "百", "数字100"),
                    new WordItem("qian", "千", "数字1000"),
                    new WordItem("wan", "万", "数字10000"),
                });
                
                // 颜色
                words.AddRange(new[] {
                    new WordItem("hong", "红", "红色"),
                    new WordItem("huang", "黄", "黄色"),
                    new WordItem("lan", "蓝", "蓝色"),
                    new WordItem("lv", "绿", "绿色"),
                    new WordItem("bai", "白", "白色"),
                    new WordItem("hei", "黑", "黑色"),
                    new WordItem("zi", "紫", "紫色"),
                    new WordItem("cheng", "橙", "橙色"),
                    new WordItem("fen", "粉", "粉色"),
                    new WordItem("hui", "灰", "灰色"),
                });
                
                // 动物
                words.AddRange(new[] {
                    new WordItem("ma", "马", "动物：马"),
                    new WordItem("niu", "牛", "动物：牛"),
                    new WordItem("yang", "羊", "动物：羊"),
                    new WordItem("zhu", "猪", "动物：猪"),
                    new WordItem("gou", "狗", "动物：狗"),
                    new WordItem("mao", "猫", "动物：猫"),
                    new WordItem("ji", "鸡", "动物：鸡"),
                    new WordItem("ya", "鸭", "动物：鸭"),
                    new WordItem("yu", "鱼", "动物：鱼"),
                    new WordItem("niao", "鸟", "动物：鸟"),
                    new WordItem("chong", "虫", "动物：虫"),
                    new WordItem("hu", "虎", "动物：老虎"),
                    new WordItem("long", "龙", "动物：龙"),
                    new WordItem("she", "蛇", "动物：蛇"),
                    new WordItem("shu", "鼠", "动物：老鼠"),
                    new WordItem("tu", "兔", "动物：兔子"),
                    new WordItem("lang", "狼", "动物：狼"),
                    new WordItem("xiong", "熊", "动物：熊"),
                    new WordItem("xiang", "象", "动物：大象"),
                    new WordItem("hou", "猴", "动物：猴子"),
                });
                
                // 植物
                words.AddRange(new[] {
                    new WordItem("cao", "草", "植物：草"),
                    new WordItem("hua", "花", "植物：花"),
                    new WordItem("shu", "树", "植物：树"),
                    new WordItem("ye", "叶", "植物：叶子"),
                    new WordItem("guo", "果", "植物：水果"),
                    new WordItem("mi", "米", "食物：米"),
                    new WordItem("mian", "面", "食物：面"),
                    new WordItem("dou", "豆", "植物：豆"),
                    new WordItem("gua", "瓜", "植物：瓜"),
                    new WordItem("cai", "菜", "植物：蔬菜"),
                });
                
                // 身体
                words.AddRange(new[] {
                    new WordItem("tou", "头", "身体：头"),
                    new WordItem("shou", "手", "身体：手"),
                    new WordItem("jiao", "脚", "身体：脚"),
                    new WordItem("mu", "目", "身体：眼睛"),
                    new WordItem("er", "耳", "身体：耳朵"),
                    new WordItem("kou", "口", "身体：嘴巴"),
                    new WordItem("bi", "鼻", "身体：鼻子"),
                    new WordItem("xin", "心", "身体：心脏"),
                    new WordItem("ya", "牙", "身体：牙齿"),
                    new WordItem("fa", "发", "身体：头发"),
                });
                
                // 家庭
                words.AddRange(new[] {
                    new WordItem("ren", "人", "人物：人"),
                    new WordItem("nan", "男", "性别：男"),
                    new WordItem("nv", "女", "性别：女"),
                    new WordItem("fu", "父", "家庭：父亲"),
                    new WordItem("mu", "母", "家庭：母亲"),
                    new WordItem("zi", "子", "家庭：儿子"),
                    new WordItem("nv", "女", "家庭：女儿"),
                    new WordItem("ge", "哥", "家庭：哥哥"),
                    new WordItem("di", "弟", "家庭：弟弟"),
                    new WordItem("jie", "姐", "家庭：姐姐"),
                    new WordItem("mei", "妹", "家庭：妹妹"),
                    new WordItem("ye", "爷", "家庭：爷爷"),
                    new WordItem("nai", "奶", "家庭：奶奶"),
                });
                
                // 自然
                words.AddRange(new[] {
                    new WordItem("tian", "天", "自然：天空"),
                    new WordItem("di", "地", "自然：大地"),
                    new WordItem("ri", "日", "自然：太阳"),
                    new WordItem("yue", "月", "自然：月亮"),
                    new WordItem("xing", "星", "自然：星星"),
                    new WordItem("yun", "云", "自然：云"),
                    new WordItem("feng", "风", "自然：风"),
                    new WordItem("yu", "雨", "自然：雨"),
                    new WordItem("xue", "雪", "自然：雪"),
                    new WordItem("shan", "山", "自然：山"),
                    new WordItem("he", "河", "自然：河流"),
                    new WordItem("hai", "海", "自然：大海"),
                    new WordItem("huo", "火", "自然：火"),
                    new WordItem("shui", "水", "自然：水"),
                    new WordItem("tu", "土", "自然：土地"),
                    new WordItem("jin", "金", "自然：金属"),
                    new WordItem("mu", "木", "自然：木头"),
                    new WordItem("shi", "石", "自然：石头"),
                });
                
                // 方位
                words.AddRange(new[] {
                    new WordItem("dong", "东", "方位：东方"),
                    new WordItem("xi", "西", "方位：西方"),
                    new WordItem("nan", "南", "方位：南方"),
                    new WordItem("bei", "北", "方位：北方"),
                    new WordItem("shang", "上", "方位：上面"),
                    new WordItem("xia", "下", "方位：下面"),
                    new WordItem("zuo", "左", "方位：左边"),
                    new WordItem("you", "右", "方位：右边"),
                    new WordItem("qian", "前", "方位：前面"),
                    new WordItem("hou", "后", "方位：后面"),
                    new WordItem("li", "里", "方位：里面"),
                    new WordItem("wai", "外", "方位：外面"),
                });
                
                // 时间
                words.AddRange(new[] {
                    new WordItem("nian", "年", "时间：年"),
                    new WordItem("yue", "月", "时间：月"),
                    new WordItem("ri", "日", "时间：日"),
                    new WordItem("shi", "时", "时间：时"),
                    new WordItem("fen", "分", "时间：分"),
                    new WordItem("miao", "秒", "时间：秒"),
                    new WordItem("zao", "早", "时间：早晨"),
                    new WordItem("wan", "晚", "时间：晚上"),
                    new WordItem("jin", "今", "时间：今天"),
                    new WordItem("ming", "明", "时间：明天"),
                    new WordItem("zuo", "昨", "时间：昨天"),
                    new WordItem("chun", "春", "季节：春天"),
                    new WordItem("xia", "夏", "季节：夏天"),
                    new WordItem("qiu", "秋", "季节：秋天"),
                    new WordItem("dong", "冬", "季节：冬天"),
                });
                
                // 动词
                words.AddRange(new[] {
                    new WordItem("chi", "吃", "动作：吃"),
                    new WordItem("he", "喝", "动作：喝"),
                    new WordItem("shui", "睡", "动作：睡觉"),
                    new WordItem("zuo", "坐", "动作：坐"),
                    new WordItem("zhan", "站", "动作：站"),
                    new WordItem("zou", "走", "动作：走"),
                    new WordItem("pao", "跑", "动作：跑"),
                    new WordItem("tiao", "跳", "动作：跳"),
                    new WordItem("fei", "飞", "动作：飞"),
                    new WordItem("kan", "看", "动作：看"),
                    new WordItem("ting", "听", "动作：听"),
                    new WordItem("shuo", "说", "动作：说"),
                    new WordItem("du", "读", "动作：读"),
                    new WordItem("xie", "写", "动作：写"),
                    new WordItem("hua", "画", "动作：画"),
                    new WordItem("chang", "唱", "动作：唱"),
                    new WordItem("wan", "玩", "动作：玩"),
                    new WordItem("xue", "学", "动作：学习"),
                });
            }
            else if (mode == "english")
            {
                // 英语单词
                words.AddRange(new[] {
                    new WordItem("apple", "苹果", "水果"),
                    new WordItem("banana", "香蕉", "水果"),
                    new WordItem("orange", "橙子", "水果"),
                    new WordItem("grape", "葡萄", "水果"),
                    new WordItem("water", "水", "饮料"),
                    new WordItem("milk", "牛奶", "饮料"),
                    new WordItem("bread", "面包", "食物"),
                    new WordItem("rice", "米饭", "食物"),
                    new WordItem("egg", "鸡蛋", "食物"),
                    new WordItem("meat", "肉", "食物"),
                    new WordItem("fish", "鱼", "食物"),
                    new WordItem("cat", "猫", "动物"),
                    new WordItem("dog", "狗", "动物"),
                    new WordItem("bird", "鸟", "动物"),
                    new WordItem("pig", "猪", "动物"),
                    new WordItem("cow", "牛", "动物"),
                    new WordItem("sheep", "羊", "动物"),
                    new WordItem("horse", "马", "动物"),
                    new WordItem("rabbit", "兔子", "动物"),
                    new WordItem("tiger", "老虎", "动物"),
                    new WordItem("lion", "狮子", "动物"),
                    new WordItem("elephant", "大象", "动物"),
                    new WordItem("monkey", "猴子", "动物"),
                    new WordItem("red", "红色", "颜色"),
                    new WordItem("blue", "蓝色", "颜色"),
                    new WordItem("green", "绿色", "颜色"),
                    new WordItem("yellow", "黄色", "颜色"),
                    new WordItem("black", "黑色", "颜色"),
                    new WordItem("white", "白色", "颜色"),
                    new WordItem("pink", "粉色", "颜色"),
                    new WordItem("purple", "紫色", "颜色"),
                    new WordItem("book", "书", "物品"),
                    new WordItem("pen", "钢笔", "物品"),
                    new WordItem("pencil", "铅笔", "物品"),
                    new WordItem("desk", "书桌", "物品"),
                    new WordItem("chair", "椅子", "物品"),
                    new WordItem("door", "门", "物品"),
                    new WordItem("window", "窗户", "物品"),
                    new WordItem("father", "爸爸", "家庭"),
                    new WordItem("mother", "妈妈", "家庭"),
                    new WordItem("brother", "兄弟", "家庭"),
                    new WordItem("sister", "姐妹", "家庭"),
                    new WordItem("teacher", "老师", "职业"),
                    new WordItem("student", "学生", "职业"),
                    new WordItem("doctor", "医生", "职业"),
                    new WordItem("sun", "太阳", "自然"),
                    new WordItem("moon", "月亮", "自然"),
                    new WordItem("star", "星星", "自然"),
                    new WordItem("cloud", "云", "自然"),
                    new WordItem("rain", "雨", "自然"),
                    new WordItem("snow", "雪", "自然"),
                    new WordItem("wind", "风", "自然"),
                    new WordItem("mountain", "山", "自然"),
                    new WordItem("river", "河", "自然"),
                    new WordItem("sea", "海", "自然"),
                    new WordItem("tree", "树", "植物"),
                    new WordItem("flower", "花", "植物"),
                    new WordItem("grass", "草", "植物"),
                    new WordItem("eat", "吃", "动词"),
                    new WordItem("drink", "喝", "动词"),
                    new WordItem("run", "跑", "动词"),
                    new WordItem("walk", "走", "动词"),
                    new WordItem("jump", "跳", "动词"),
                    new WordItem("swim", "游泳", "动词"),
                    new WordItem("fly", "飞", "动词"),
                    new WordItem("read", "读", "动词"),
                    new WordItem("write", "写", "动词"),
                    new WordItem("sing", "唱", "动词"),
                    new WordItem("dance", "跳舞", "动词"),
                    new WordItem("play", "玩", "动词"),
                    new WordItem("study", "学习", "动词"),
                });
            }
            else if (mode == "poetry")
            {
                // 古诗词
                words.AddRange(new[] {
                    // 静夜思
                    new WordItem("chuang", "床", "床前明月光"),
                    new WordItem("qian", "前", "床前明月光"),
                    new WordItem("ming", "明", "床前明月光"),
                    new WordItem("yue", "月", "床前明月光"),
                    new WordItem("guang", "光", "床前明月光"),
                    new WordItem("yi", "疑", "疑是地上霜"),
                    new WordItem("shi", "是", "疑是地上霜"),
                    new WordItem("di", "地", "疑是地上霜"),
                    new WordItem("shuang", "霜", "疑是地上霜"),
                    new WordItem("ju", "举", "举头望明月"),
                    new WordItem("tou", "头", "举头望明月"),
                    new WordItem("wang", "望", "举头望明月"),
                    new WordItem("si", "思", "低头思故乡"),
                    new WordItem("gu", "故", "低头思故乡"),
                    new WordItem("xiang", "乡", "低头思故乡"),
                    // 登鹳雀楼
                    new WordItem("bai", "白", "白日依山尽"),
                    new WordItem("ri", "日", "白日依山尽"),
                    new WordItem("yi", "依", "白日依山尽"),
                    new WordItem("shan", "山", "白日依山尽"),
                    new WordItem("jin", "尽", "白日依山尽"),
                    new WordItem("huang", "黄", "黄河入海流"),
                    new WordItem("he", "河", "黄河入海流"),
                    new WordItem("ru", "入", "黄河入海流"),
                    new WordItem("hai", "海", "黄河入海流"),
                    new WordItem("liu", "流", "黄河入海流"),
                    new WordItem("yu", "欲", "欲穷千里目"),
                    new WordItem("qiong", "穷", "欲穷千里目"),
                    new WordItem("qian", "千", "欲穷千里目"),
                    new WordItem("mu", "目", "欲穷千里目"),
                    new WordItem("geng", "更", "更上一层楼"),
                    new WordItem("ceng", "层", "更上一层楼"),
                    new WordItem("lou", "楼", "更上一层楼"),
                    // 春晓
                    new WordItem("chun", "春", "春眠不觉晓"),
                    new WordItem("mian", "眠", "春眠不觉晓"),
                    new WordItem("bu", "不", "春眠不觉晓"),
                    new WordItem("jue", "觉", "春眠不觉晓"),
                    new WordItem("xiao", "晓", "春眠不觉晓"),
                    new WordItem("chu", "处", "处处闻啼鸟"),
                    new WordItem("wen", "闻", "处处闻啼鸟"),
                    new WordItem("ti", "啼", "处处闻啼鸟"),
                    new WordItem("niao", "鸟", "处处闻啼鸟"),
                    new WordItem("ye", "夜", "夜来风雨声"),
                    new WordItem("lai", "来", "夜来风雨声"),
                    new WordItem("feng", "风", "夜来风雨声"),
                    new WordItem("yu", "雨", "夜来风雨声"),
                    new WordItem("sheng", "声", "夜来风雨声"),
                    new WordItem("hua", "花", "花落知多少"),
                    new WordItem("luo", "落", "花落知多少"),
                    new WordItem("zhi", "知", "花落知多少"),
                    new WordItem("duo", "多", "花落知多少"),
                    new WordItem("shao", "少", "花落知多少"),
                    // 游子吟
                    new WordItem("ci", "慈", "慈母手中线"),
                    new WordItem("mu", "母", "慈母手中线"),
                    new WordItem("shou", "手", "慈母手中线"),
                    new WordItem("zhong", "中", "慈母手中线"),
                    new WordItem("xian", "线", "慈母手中线"),
                    new WordItem("you", "游", "游子身上衣"),
                    new WordItem("zi", "子", "游子身上衣"),
                    new WordItem("shen", "身", "游子身上衣"),
                    new WordItem("yi", "衣", "游子身上衣"),
                    new WordItem("lin", "临", "临行密密缝"),
                    new WordItem("xing", "行", "临行密密缝"),
                    new WordItem("mi", "密", "临行密密缝"),
                    new WordItem("feng", "缝", "临行密密缝"),
                    new WordItem("kong", "恐", "意恐迟迟归"),
                    new WordItem("chi", "迟", "意恐迟迟归"),
                    new WordItem("gui", "归", "意恐迟迟归"),
                    new WordItem("xin", "心", "谁言寸草心"),
                    new WordItem("bao", "报", "报得三春晖"),
                    new WordItem("hui", "晖", "报得三春晖"),
                });
            }
            
            return words;
        }
        
        private void ShowCurrentWord()
        {
            if (_currentIndex >= _words.Count)
            {
                // 练习完成
                WordText.Text = "🎉";
                WordText.FontSize = 80;
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
            WordText.FontSize = 80;
            MeaningText.Text = word.Meaning;
            ProgressText.Text = $"进度：{_currentIndex + 1}/{_words.Count}";
            InputBox.Text = "";
            InputBox.IsEnabled = true;
            HintText.Text = "输入后按 Enter 确认";
            ResultText.Text = "";
            _showingResult = false;
            
            InputBox.Focus();
        }
        
        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (_currentIndex >= _words.Count) return;
            
            if (e.Key == Key.Enter)
            {
                if (_showingResult)
                {
                    _currentIndex++;
                    ShowCurrentWord();
                    return;
                }
                
                var word = _words[_currentIndex];
                var input = InputBox.Text.Trim().ToLower();
                
                if (string.IsNullOrEmpty(input))
                {
                    ResultText.Text = "❌ 请先输入！";
                    ResultText.Foreground = Brushes.Red;
                    return;
                }
                
                bool isCorrect = input == word.Word.ToLower();
                
                if (isCorrect)
                {
                    _correctCount++;
                    ResultText.Text = "✅ 正确！按 Enter 继续";
                    ResultText.Foreground = Brushes.Green;
                }
                else
                {
                    ResultText.Text = $"❌ 错误！答案是 {word.Word}，按 Enter 继续";
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
        public string Word { get; set; } = "";
        public string Display { get; set; } = "";
        public string Meaning { get; set; } = "";
        
        public WordItem(string word, string display, string meaning)
        {
            Word = word;
            Display = display;
            Meaning = meaning;
        }
        
        public WordItem() { }
    }
}
