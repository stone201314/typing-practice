using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TypingPractice
{
    public partial class PracticeWindow : Window
    {
        private List<WordItem> _words;
        private int _currentIndex = 0;
        private int _correctCount = 0;
        private bool _showingResult = false;
        private readonly string _mode;
        private readonly string _difficulty;
        private readonly int _count;
        
        public PracticeWindow(string mode, string difficulty, int count)
        {
            InitializeComponent();
            
            _mode = mode;
            _difficulty = difficulty;
            _count = count;
            
            // 获取词库并随机选择
            var allWords = GetVocabulary(mode, difficulty);
            var random = new Random();
            var shuffled = new List<WordItem>(allWords);
            shuffled.Sort((a, b) => random.Next() - random.Next());
            
            _words = new List<WordItem>();
            for (int i = 0; i < Math.Min(count, shuffled.Count); i++)
            {
                _words.Add(shuffled[i]);
            }
            
            // 更新提示文字
            PromptText.Text = mode switch
            {
                "pinyin" => difficulty == "hard" ? "请输入整句拼音：" : "请输入对应的拼音：",
                "english" => difficulty == "hard" ? "请输入整句英文：" : "请输入对应的英文：",
                "poetry" => "请输入对应的拼音：",
                _ => "请输入："
            };
            
            ShowCurrentWord();
        }
        
        private List<WordItem> GetVocabulary(string mode, string difficulty)
        {
            var words = new List<WordItem>();
            
            if (mode == "pinyin")
            {
                if (difficulty == "easy")
                {
                    // 单字
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
                        new WordItem("hong", "红", "红色"),
                        new WordItem("huang", "黄", "黄色"),
                        new WordItem("lan", "蓝", "蓝色"),
                        new WordItem("lv", "绿", "绿色"),
                        new WordItem("bai", "白", "白色"),
                        new WordItem("hei", "黑", "黑色"),
                        new WordItem("ma", "马", "动物"),
                        new WordItem("niu", "牛", "动物"),
                        new WordItem("yang", "羊", "动物"),
                        new WordItem("zhu", "猪", "动物"),
                        new WordItem("gou", "狗", "动物"),
                        new WordItem("mao", "猫", "动物"),
                        new WordItem("ji", "鸡", "动物"),
                        new WordItem("ya", "鸭", "动物"),
                        new WordItem("yu", "鱼", "动物"),
                        new WordItem("niao", "鸟", "动物"),
                        new WordItem("hu", "虎", "动物"),
                        new WordItem("long", "龙", "动物"),
                        new WordItem("she", "蛇", "动物"),
                        new WordItem("shu", "鼠", "动物"),
                        new WordItem("tu", "兔", "动物"),
                        new WordItem("cao", "草", "植物"),
                        new WordItem("hua", "花", "植物"),
                        new WordItem("shu", "树", "植物"),
                        new WordItem("ye", "叶", "植物"),
                        new WordItem("guo", "果", "植物"),
                        new WordItem("tou", "头", "身体"),
                        new WordItem("shou", "手", "身体"),
                        new WordItem("jiao", "脚", "身体"),
                        new WordItem("mu", "目", "身体"),
                        new WordItem("er", "耳", "身体"),
                        new WordItem("kou", "口", "身体"),
                        new WordItem("bi", "鼻", "身体"),
                        new WordItem("xin", "心", "身体"),
                        new WordItem("ya", "牙", "身体"),
                        new WordItem("tian", "天", "自然"),
                        new WordItem("di", "地", "自然"),
                        new WordItem("ri", "日", "自然"),
                        new WordItem("yue", "月", "自然"),
                        new WordItem("xing", "星", "自然"),
                        new WordItem("yun", "云", "自然"),
                        new WordItem("feng", "风", "自然"),
                        new WordItem("yu", "雨", "自然"),
                        new WordItem("xue", "雪", "自然"),
                        new WordItem("shan", "山", "自然"),
                        new WordItem("he", "河", "自然"),
                        new WordItem("hai", "海", "自然"),
                        new WordItem("huo", "火", "自然"),
                        new WordItem("shui", "水", "自然"),
                        new WordItem("dong", "东", "方位"),
                        new WordItem("xi", "西", "方位"),
                        new WordItem("nan", "南", "方位"),
                        new WordItem("bei", "北", "方位"),
                        new WordItem("shang", "上", "方位"),
                        new WordItem("xia", "下", "方位"),
                        new WordItem("zuo", "左", "方位"),
                        new WordItem("you", "右", "方位"),
                        new WordItem("qian", "前", "方位"),
                        new WordItem("hou", "后", "方位"),
                        new WordItem("nian", "年", "时间"),
                        new WordItem("yue", "月", "时间"),
                        new WordItem("ri", "日", "时间"),
                        new WordItem("shi", "时", "时间"),
                        new WordItem("fen", "分", "时间"),
                        new WordItem("miao", "秒", "时间"),
                        new WordItem("zao", "早", "时间"),
                        new WordItem("wan", "晚", "时间"),
                        new WordItem("chun", "春", "季节"),
                        new WordItem("xia", "夏", "季节"),
                        new WordItem("qiu", "秋", "季节"),
                        new WordItem("dong", "冬", "季节"),
                        new WordItem("chi", "吃", "动作"),
                        new WordItem("he", "喝", "动作"),
                        new WordItem("shui", "睡", "动作"),
                        new WordItem("zuo", "坐", "动作"),
                        new WordItem("zhan", "站", "动作"),
                        new WordItem("zou", "走", "动作"),
                        new WordItem("pao", "跑", "动作"),
                        new WordItem("tiao", "跳", "动作"),
                        new WordItem("fei", "飞", "动作"),
                        new WordItem("kan", "看", "动作"),
                        new WordItem("ting", "听", "动作"),
                        new WordItem("shuo", "说", "动作"),
                        new WordItem("du", "读", "动作"),
                        new WordItem("xie", "写", "动作"),
                        new WordItem("hua", "画", "动作"),
                        new WordItem("chang", "唱", "动作"),
                        new WordItem("wan", "玩", "动作"),
                        new WordItem("xue", "学", "动作"),
                    });
                }
                else if (difficulty == "medium")
                {
                    // 词组
                    words.AddRange(new[] {
                        new WordItem("nihao", "你好", "问候语"),
                        new WordItem("xiexie", "谢谢", "感谢"),
                        new WordItem("duibuqi", "对不起", "道歉"),
                        new WordItem("zaijian", "再见", "告别"),
                        new WordItem("zaochen", "早晨", "时间"),
                        new WordItem("zhongwu", "中午", "时间"),
                        new WordItem("wanshang", "晚上", "时间"),
                        new WordItem("jintian", "今天", "时间"),
                        new WordItem("mingtian", "明天", "时间"),
                        new WordItem("zuotian", "昨天", "时间"),
                        new WordItem("xianzai", "现在", "时间"),
                        new WordItem("yihou", "以后", "时间"),
                        new WordItem("xuesheng", "学生", "身份"),
                        new WordItem("laoshi", "老师", "身份"),
                        new WordItem("baba", "爸爸", "家庭"),
                        new WordItem("mama", "妈妈", "家庭"),
                        new WordItem("gege", "哥哥", "家庭"),
                        new WordItem("jiejie", "姐姐", "家庭"),
                        new WordItem("didi", "弟弟", "家庭"),
                        new WordItem("meimei", "妹妹", "家庭"),
                        new WordItem("yejing", "爷爷", "家庭"),
                        new WordItem("nainai", "奶奶", "家庭"),
                        new WordItem("shuiguo", "水果", "食物"),
                        new WordItem("shucai", "蔬菜", "食物"),
                        new WordItem("mifan", "米饭", "食物"),
                        new WordItem("miantiao", "面条", "食物"),
                        new WordItem("jidan", "鸡蛋", "食物"),
                        new WordItem("niunai", "牛奶", "饮料"),
                        new WordItem("pingguo", "苹果", "水果"),
                        new WordItem("xiangjiao", "香蕉", "水果"),
                        new WordItem("chengzi", "橙子", "水果"),
                        new WordItem("xigua", "西瓜", "水果"),
                        new WordItem("caomei", "草莓", "水果"),
                        new WordItem("fengjing", "风景", "景色"),
                        new WordItem("gongyuan", "公园", "地点"),
                        new WordItem("xuexiao", "学校", "地点"),
                        new WordItem("yiyuan", "医院", "地点"),
                        new WordItem("shangdian", "商店", "地点"),
                        new WordItem("tushuguan", "图书馆", "地点"),
                        new WordItem("dongwuyuan", "动物园", "地点"),
                        new WordItem("huaer", "花儿", "植物"),
                        new WordItem("shumu", "树木", "植物"),
                        new WordItem("caodi", "草地", "植物"),
                        new WordItem("taiyang", "太阳", "自然"),
                        new WordItem("yueliang", "月亮", "自然"),
                        new WordItem("xingxing", "星星", "自然"),
                        new WordItem("yuncai", "云彩", "自然"),
                        new WordItem("xiaoyu", "小雨", "天气"),
                        new WordItem("daxue", "大雪", "天气"),
                        new WordItem("dafeng", "大风", "天气"),
                        new WordItem("kuaille", "快乐", "情绪"),
                        new WordItem("kaixin", "开心", "情绪"),
                        new WordItem("nanguo", "难过", "情绪"),
                        new WordItem("shengqi", "生气", "情绪"),
                        new WordItem("haipa", "害怕", "情绪"),
                        new WordItem("xihuan", "喜欢", "情感"),
                        new WordItem("aixin", "爱心", "情感"),
                        new WordItem("youyi", "友谊", "情感"),
                        new WordItem("meili", "美丽", "形容"),
                        new WordItem("shuaiqi", "帅气", "形容"),
                        new WordItem("congming", "聪明", "形容"),
                        new WordItem("yonggan", "勇敢", "形容"),
                        new WordItem("shanhuliang", "善良", "形容"),
                        new WordItem("renzhen", "认真", "态度"),
                        new WordItem("nuli", "努力", "态度"),
                        new WordItem("jianchi", "坚持", "态度"),
                    });
                }
                else // hard - 句子
                {
                    words.AddRange(new[] {
                        new WordItem("nihaoa", "你好啊", "问候"),
                        new WordItem("xiexieni", "谢谢你", "感谢"),
                        new WordItem("duibuqiwocaicuo", "对不起我错了", "道歉"),
                        new WordItem("womenzuopengyouba", "我们做朋友吧", "交友"),
                        new WordItem("jintiantianqizhenhao", "今天天气真好", "天气"),
                        new WordItem("woxihuanchifan", "我喜欢吃饭", "爱好"),
                        new WordItem("woainiwodejia", "我爱我的家", "家庭"),
                        new WordItem("xuexiaizhenmeili", "学校真美丽", "学校"),
                        new WordItem("laoshixinkule", "老师辛苦了", "感谢老师"),
                        new WordItem("womaidepingguo", "我买的苹果", "日常"),
                        new WordItem("taiyangchulaile", "太阳出来了", "自然"),
                        new WordItem("yueliangyuanyuan", "月亮圆圆", "自然"),
                        new WordItem("xingxingliangshan", "星星闪闪", "自然"),
                        new WordItem("xiaoyuhuahua", "小雨哗哗", "天气"),
                        new WordItem("daxuemanman", "大雪漫漫", "天气"),
                        new WordItem("chunnuanhuakai", "春暖花开", "季节"),
                        new WordItem("xiarizhizhao", "夏日之朝", "季节"),
                        new WordItem("qiugaoqishuang", "秋高气爽", "季节"),
                        new WordItem("dongrixuepiao", "冬日雪飘", "季节"),
                        new WordItem("haizimenwanshua", "孩子们玩耍", "活动"),
                        new WordItem("niaoerfeixiang", "鸟儿飞翔", "动物"),
                        new WordItem("yuerzaiyouyong", "鱼儿在游泳", "动物"),
                        new WordItem("huaduocongkai", "花朵盛开", "植物"),
                        new WordItem("shumuzhifan", "树木枝繁", "植物"),
                        new WordItem("woyaoduhaoshu", "我要读好书", "学习"),
                        new WordItem("xuexishirenwujin", "学习使人进步", "学习"),
                        new WordItem("laodongzuiaguangrong", "劳动最光荣", "价值观"),
                        new WordItem("tuanjiujiushiliang", "团结就是力量", "价值观"),
                        new WordItem("chengxinshiweibaode", "诚信是为人本的", "价值观"),
                        new WordItem("youaizhujirenyue", "友爱助人愉悦", "价值观"),
                    });
                }
            }
            else if (mode == "english")
            {
                if (difficulty == "easy")
                {
                    // 单词
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
                else if (difficulty == "medium")
                {
                    // 短语
                    words.AddRange(new[] {
                        new WordItem("good morning", "早上好", "问候"),
                        new WordItem("good afternoon", "下午好", "问候"),
                        new WordItem("good evening", "晚上好", "问候"),
                        new WordItem("good night", "晚安", "告别"),
                        new WordItem("thank you", "谢谢你", "感谢"),
                        new WordItem("excuse me", "打扰一下", "礼貌"),
                        new WordItem("im sorry", "对不起", "道歉"),
                        new WordItem("nice to meet you", "很高兴见到你", "问候"),
                        new WordItem("how are you", "你好吗", "问候"),
                        new WordItem("im fine", "我很好", "回答"),
                        new WordItem("see you", "再见", "告别"),
                        new WordItem("see you tomorrow", "明天见", "告别"),
                        new WordItem("good luck", "祝你好运", "祝福"),
                        new WordItem("happy birthday", "生日快乐", "祝福"),
                        new WordItem("merry christmas", "圣诞快乐", "节日"),
                        new WordItem("happy new year", "新年快乐", "节日"),
                        new WordItem("sit down", "坐下", "指令"),
                        new WordItem("stand up", "站起来", "指令"),
                        new WordItem("open your book", "打开你的书", "指令"),
                        new WordItem("close your book", "合上你的书", "指令"),
                        new WordItem("listen to me", "听我说", "指令"),
                        new WordItem("look at me", "看着我", "指令"),
                        new WordItem("read after me", "跟我读", "指令"),
                        new WordItem("write it down", "写下来", "指令"),
                        new WordItem("very good", "很好", "鼓励"),
                        new WordItem("well done", "做得好", "鼓励"),
                        new WordItem("try again", "再试一次", "鼓励"),
                        new WordItem("dont worry", "别担心", "安慰"),
                        new WordItem("its okay", "没关系", "安慰"),
                        new WordItem("can i help you", "我能帮你吗", "帮助"),
                        new WordItem("here you are", "给你", "给予"),
                        new WordItem("this way", "这边走", "指引"),
                        new WordItem("of course", "当然", "肯定"),
                        new WordItem("wait a minute", "等一下", "请求"),
                        new WordItem("have a good day", "祝你今天愉快", "祝福"),
                        new WordItem("have fun", "玩得开心", "祝福"),
                        new WordItem("lets go", "走吧", "提议"),
                        new WordItem("come here", "过来", "指令"),
                        new WordItem("go away", "走开", "指令"),
                        new WordItem("i love you", "我爱你", "情感"),
                        new WordItem("i like it", "我喜欢它", "情感"),
                        new WordItem("i dont know", "我不知道", "表达"),
                        new WordItem("thats right", "对", "肯定"),
                        new WordItem("thats wrong", "错", "否定"),
                        new WordItem("what is this", "这是什么", "疑问"),
                        new WordItem("where is it", "它在哪里", "疑问"),
                        new WordItem("who is he", "他是谁", "疑问"),
                        new WordItem("how old are you", "你几岁了", "疑问"),
                        new WordItem("what time is it", "几点了", "疑问"),
                    });
                }
                else // hard - 句子
                {
                    words.AddRange(new[] {
                        new WordItem("good morning teacher", "老师早上好", "问候"),
                        new WordItem("how are you today", "你今天好吗", "问候"),
                        new WordItem("im fine thank you", "我很好谢谢", "回答"),
                        new WordItem("nice to meet you too", "我也很高兴见到你", "问候"),
                        new WordItem("thank you very much", "非常感谢", "感谢"),
                        new WordItem("you are welcome", "不客气", "礼貌"),
                        new WordItem("excuse me can you help me", "打扰一下你能帮我吗", "请求"),
                        new WordItem("im sorry im late", "对不起我迟到了", "道歉"),
                        new WordItem("its okay dont worry", "没关系别担心", "安慰"),
                        new WordItem("what is your name", "你叫什么名字", "问候"),
                        new WordItem("my name is tom", "我叫汤姆", "介绍"),
                        new WordItem("how old are you", "你几岁了", "询问"),
                        new WordItem("im ten years old", "我十岁了", "回答"),
                        new WordItem("where do you live", "你住在哪里", "询问"),
                        new WordItem("i live in beijing", "我住在北京", "回答"),
                        new WordItem("what do you like", "你喜欢什么", "询问"),
                        new WordItem("i like playing basketball", "我喜欢打篮球", "回答"),
                        new WordItem("do you have any brothers", "你有兄弟吗", "询问"),
                        new WordItem("i have one brother", "我有一个哥哥", "回答"),
                        new WordItem("what is your favorite color", "你最喜欢的颜色是什么", "询问"),
                        new WordItem("my favorite color is blue", "我最喜欢的颜色是蓝色", "回答"),
                        new WordItem("what did you do yesterday", "你昨天做了什么", "询问"),
                        new WordItem("i went to the park", "我去了公园", "回答"),
                        new WordItem("will you come to my party", "你会来我的派对吗", "邀请"),
                        new WordItem("yes i will come", "是的我会来", "回答"),
                        new WordItem("lets play together", "让我们一起玩吧", "邀请"),
                        new WordItem("that sounds great", "听起来很棒", "赞同"),
                        new WordItem("i have to go now", "我得走了", "告别"),
                        new WordItem("see you next time", "下次见", "告别"),
                        new WordItem("have a nice weekend", "周末愉快", "祝福"),
                    });
                }
            }
            else if (mode == "poetry")
            {
                if (difficulty == "easy")
                {
                    // 单字
                    words.AddRange(new[] {
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
                        new WordItem("bai", "白", "白日依山尽"),
                        new WordItem("ri", "日", "白日依山尽"),
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
                    });
                }
                else if (difficulty == "medium")
                {
                    // 词组
                    words.AddRange(new[] {
                        new WordItem("chuangqian", "床前", "床前明月光"),
                        new WordItem("mingyue", "明月", "床前明月光"),
                        new WordItem("yueguang", "月光", "床前明月光"),
                        new WordItem("disha", "地上", "疑是地上霜"),
                        new WordItem("dishang", "地上", "疑是地上霜"),
                        new WordItem("jutou", "举头", "举头望明月"),
                        new WordItem("wangming", "望明", "举头望明月"),
                        new WordItem("ditou", "低头", "低头思故乡"),
                        new WordItem("siguxiang", "思故乡", "低头思故乡"),
                        new WordItem("guxiang", "故乡", "低头思故乡"),
                        new WordItem("bairi", "白日", "白日依山尽"),
                        new WordItem("yishanjin", "依山尽", "白日依山尽"),
                        new WordItem("huanghe", "黄河", "黄河入海流"),
                        new WordItem("ruhailiu", "入海流", "黄河入海流"),
                        new WordItem("yuqiong", "欲穷", "欲穷千里目"),
                        new WordItem("qianlimu", "千里目", "欲穷千里目"),
                        new WordItem("qianli", "千里", "欲穷千里目"),
                        new WordItem("gengshang", "更上", "更上一层楼"),
                        new WordItem("cenglou", "层楼", "更上一层楼"),
                        new WordItem("chunmian", "春眠", "春眠不觉晓"),
                        new WordItem("bujue", "不觉", "春眠不觉晓"),
                        new WordItem("chuchu", "处处", "处处闻啼鸟"),
                        new WordItem("wenti", "闻啼", "处处闻啼鸟"),
                        new WordItem("tinao", "啼鸟", "处处闻啼鸟"),
                        new WordItem("yelai", "夜来", "夜来风雨声"),
                        new WordItem("fengyu", "风雨", "夜来风雨声"),
                        new WordItem("yusheng", "雨声", "夜来风雨声"),
                        new WordItem("hualuo", "花落", "花落知多少"),
                        new WordItem("zhiduo", "知多", "花落知多少"),
                        new WordItem("shaonshao", "多少", "花落知多少"),
                        new WordItem("cimu", "慈母", "慈母手中线"),
                        new WordItem("shouzhong", "手中", "慈母手中线"),
                        new WordItem("youzi", "游子", "游子身上衣"),
                        new WordItem("shenshang", "身上", "游子身上衣"),
                        new WordItem("linxing", "临行", "临行密密缝"),
                        new WordItem("mifeng", "密缝", "临行密密缝"),
                        new WordItem("chichigui", "迟迟归", "意恐迟迟归"),
                        new WordItem("cuncao", "寸草", "谁言寸草心"),
                        new WordItem("caoxin", "草心", "谁言寸草心"),
                        new WordItem("sanchun", "三春", "报得三春晖"),
                        new WordItem("chunhui", "春晖", "报得三春晖"),
                    });
                }
                else // hard - 诗句
                {
                    words.AddRange(new[] {
                        new WordItem("chuangqianmingyueguang", "床前明月光", "李白《静夜思》"),
                        new WordItem("yishidishangshuang", "疑是地上霜", "李白《静夜思》"),
                        new WordItem("jutouwangmingyue", "举头望明月", "李白《静夜思》"),
                        new WordItem("ditousiguxiang", "低头思故乡", "李白《静夜思》"),
                        new WordItem("bairiyishanjin", "白日依山尽", "王之涣《登鹳雀楼》"),
                        new WordItem("huangheruhailiu", "黄河入海流", "王之涣《登鹳雀楼》"),
                        new WordItem("yuqiongqianlimu", "欲穷千里目", "王之涣《登鹳雀楼》"),
                        new WordItem("gengshangyicenglou", "更上一层楼", "王之涣《登鹳雀楼》"),
                        new WordItem("chunmianbujue", "春眠不觉晓", "孟浩然《春晓》"),
                        new WordItem("chuchuwenti", "处处闻啼鸟", "孟浩然《春晓》"),
                        new WordItem("yelaifengyusheng", "夜来风雨声", "孟浩然《春晓》"),
                        new WordItem("hualuozhiduoshao", "花落知多少", "孟浩然《春晓》"),
                        new WordItem("cimushouzhongxian", "慈母手中线", "孟郊《游子吟》"),
                        new WordItem("youzishenshangyi", "游子身上衣", "孟郊《游子吟》"),
                        new WordItem("linxingmimifeng", "临行密密缝", "孟郊《游子吟》"),
                        new WordItem("yikongchichigui", "意恐迟迟归", "孟郊《游子吟》"),
                        new WordItem("shuiyancuncaoxin", "谁言寸草心", "孟郊《游子吟》"),
                        new WordItem("baodesanchunhui", "报得三春晖", "孟郊《游子吟》"),
                        new WordItem("hongdoushengnanguo", "红豆生南国", "王维《相思》"),
                        new WordItem("chunlafajizhi", "春来发几枝", "王维《相思》"),
                        new WordItem("yuanjunduocaixie", "愿君多采撷", "王维《相思》"),
                        new WordItem("ciwuzuixiangsi", "此物最相思", "王维《相思》"),
                    });
                }
            }
            
            return words;
        }
        
        private void ShowCurrentWord()
        {
            if (_currentIndex >= _words.Count)
            {
                // 练习完成
                WordText.Text = "🎉";
                WordText.FontSize = 72;
                MeaningText.Text = $"正确率：{_correctCount}/{_words.Count}";
                InputBox.Visibility = Visibility.Collapsed;
                HintText.Visibility = Visibility.Collapsed;
                ResultText.Text = "练习完成！";
                ResultText.Foreground = Brushes.Green;
                
                // 显示继续/返回按钮
                ContinueBtn.Visibility = Visibility.Visible;
                BackBtn.Visibility = Visibility.Visible;
                CloseBtn.Visibility = Visibility.Collapsed;
                return;
            }
            
            var word = _words[_currentIndex];
            
            // 根据难度调整字体大小
            WordText.Text = word.Display;
            WordText.FontSize = _difficulty == "hard" ? 36 : 64;
            
            MeaningText.Text = word.Meaning;
            ProgressText.Text = $"进度：{_currentIndex + 1}/{_words.Count}";
            InputBox.Text = "";
            InputBox.IsEnabled = true;
            InputBox.Visibility = Visibility.Visible;
            HintText.Visibility = Visibility.Visible;
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
                var input = InputBox.Text.Trim().ToLower().Replace(" ", "");
                var correct = word.Word.ToLower().Replace(" ", "");
                
                if (string.IsNullOrEmpty(input))
                {
                    ResultText.Text = "❌ 请先输入！";
                    ResultText.Foreground = Brushes.Red;
                    return;
                }
                
                bool isCorrect = input == correct;
                
                if (isCorrect)
                {
                    _correctCount++;
                    ResultText.Text = "✅ 正确！按 Enter 继续";
                    ResultText.Foreground = Brushes.Green;
                }
                else
                {
                    ResultText.Text = $"❌ 答案是：{word.Word}";
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
        
        private void OnContinue(object sender, RoutedEventArgs e)
        {
            // 重新开始练习
            var random = new Random();
            var allWords = GetVocabulary(_mode, _difficulty);
            var shuffled = new List<WordItem>(allWords);
            shuffled.Sort((a, b) => random.Next() - random.Next());
            
            _words = new List<WordItem>();
            for (int i = 0; i < Math.Min(_count, shuffled.Count); i++)
            {
                _words.Add(shuffled[i]);
            }
            
            _currentIndex = 0;
            _correctCount = 0;
            
            // 重置界面
            ContinueBtn.Visibility = Visibility.Collapsed;
            BackBtn.Visibility = Visibility.Collapsed;
            CloseBtn.Visibility = Visibility.Visible;
            
            ShowCurrentWord();
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
