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
            if (mode.StartsWith("chinese"))
            {
                PromptText.Text = "请输入对应的拼音：";
            }
            else
            {
                PromptText.Text = "请输入对应的英文：";
            }
            
            ShowCurrentWord();
        }
        
        private List<WordItem> GetVocabulary(string mode, string difficulty)
        {
            var words = new List<WordItem>();
            
            // 中文词组练习
            if (mode == "chinese_type1")
            {
                if (difficulty == "easy")
                {
                    words.AddRange(new[] {
                        // 数字
                        new WordItem("yi", "一", "数字1"), new WordItem("er", "二", "数字2"),
                        new WordItem("san", "三", "数字3"), new WordItem("si", "四", "数字4"),
                        new WordItem("wu", "五", "数字5"), new WordItem("liu", "六", "数字6"),
                        new WordItem("qi", "七", "数字7"), new WordItem("ba", "八", "数字8"),
                        new WordItem("jiu", "九", "数字9"), new WordItem("shi", "十", "数字10"),
                        new WordItem("bai", "百", "数字100"), new WordItem("qian", "千", "数字1000"),
                        new WordItem("wan", "万", "数字10000"),
                        // 颜色
                        new WordItem("hong", "红", "红色"), new WordItem("huang", "黄", "黄色"),
                        new WordItem("lan", "蓝", "蓝色"), new WordItem("lv", "绿", "绿色"),
                        new WordItem("bai", "白", "白色"), new WordItem("hei", "黑", "黑色"),
                        new WordItem("zi", "紫", "紫色"), new WordItem("cheng", "橙", "橙色"),
                        // 动物
                        new WordItem("ma", "马", "马匹"), new WordItem("niu", "牛", "牛"),
                        new WordItem("yang", "羊", "绵羊"), new WordItem("zhu", "猪", "猪"),
                        new WordItem("gou", "狗", "狗"), new WordItem("mao", "猫", "猫"),
                        new WordItem("ji", "鸡", "鸡"), new WordItem("ya", "鸭", "鸭"),
                        new WordItem("yu", "鱼", "鱼"), new WordItem("niao", "鸟", "鸟"),
                        new WordItem("hu", "虎", "老虎"), new WordItem("long", "龙", "龙"),
                        new WordItem("she", "蛇", "蛇"), new WordItem("shu", "鼠", "老鼠"),
                        new WordItem("tu", "兔", "兔子"), new WordItem("lang", "狼", "狼"),
                        new WordItem("xiong", "熊", "熊"), new WordItem("xiang", "象", "大象"),
                        // 植物
                        new WordItem("cao", "草", "草"), new WordItem("hua", "花", "花"),
                        new WordItem("shu", "树", "树"), new WordItem("ye", "叶", "叶子"),
                        new WordItem("guo", "果", "水果"), new WordItem("dou", "豆", "豆子"),
                        // 身体
                        new WordItem("tou", "头", "头"), new WordItem("shou", "手", "手"),
                        new WordItem("jiao", "脚", "脚"), new WordItem("mu", "目", "眼睛"),
                        new WordItem("er", "耳", "耳朵"), new WordItem("kou", "口", "嘴巴"),
                        new WordItem("bi", "鼻", "鼻子"), new WordItem("ya", "牙", "牙齿"),
                        // 自然
                        new WordItem("tian", "天", "天空"), new WordItem("di", "地", "大地"),
                        new WordItem("ri", "日", "太阳"), new WordItem("yue", "月", "月亮"),
                        new WordItem("xing", "星", "星星"), new WordItem("yun", "云", "云"),
                        new WordItem("feng", "风", "风"), new WordItem("yu", "雨", "雨"),
                        new WordItem("xue", "雪", "雪"), new WordItem("shan", "山", "山"),
                        new WordItem("he", "河", "河"), new WordItem("hai", "海", "海"),
                        // 方位
                        new WordItem("dong", "东", "东方"), new WordItem("xi", "西", "西方"),
                        new WordItem("nan", "南", "南方"), new WordItem("bei", "北", "北方"),
                        new WordItem("shang", "上", "上面"), new WordItem("xia", "下", "下面"),
                        new WordItem("zuo", "左", "左边"), new WordItem("you", "右", "右边"),
                        // 时间
                        new WordItem("nian", "年", "年"), new WordItem("yue", "月", "月"),
                        new WordItem("ri", "日", "日"), new WordItem("shi", "时", "时"),
                        new WordItem("zao", "早", "早"), new WordItem("wan", "晚", "晚"),
                        // 动作
                        new WordItem("chi", "吃", "吃"), new WordItem("he", "喝", "喝"),
                        new WordItem("zou", "走", "走"), new WordItem("pao", "跑", "跑"),
                        new WordItem("kan", "看", "看"), new WordItem("ting", "听", "听"),
                        new WordItem("shuo", "说", "说"), new WordItem("xie", "写", "写"),
                    });
                }
                else if (difficulty == "medium")
                {
                    words.AddRange(new[] {
                        // 问候
                        new WordItem("nihao", "你好", "问候"), new WordItem("zaijian", "再见", "告别"),
                        new WordItem("xiexie", "谢谢", "感谢"), new WordItem("duibuqi", "对不起", "道歉"),
                        new WordItem("meiguanxi", "没关系", "原谅"), new WordItem("bukeqi", "不客气", "礼貌"),
                        new WordItem("zaoshanghao", "早上好", "问候"), new WordItem("wanshanghao", "晚上好", "问候"),
                        // 时间
                        new WordItem("jintian", "今天", "时间"), new WordItem("mingtian", "明天", "时间"),
                        new WordItem("zuotian", "昨天", "时间"), new WordItem("houtian", "后天", "时间"),
                        new WordItem("qiantian", "前天", "时间"), new WordItem("meitian", "每天", "时间"),
                        new WordItem("zaochen", "早晨", "时间"), new WordItem("zhongwu", "中午", "时间"),
                        new WordItem("bangwan", "傍晚", "时间"), new WordItem("shangwu", "上午", "时间"),
                        new WordItem("xiawu", "下午", "时间"), new WordItem("yejian", "夜间", "时间"),
                        // 家庭
                        new WordItem("baba", "爸爸", "家庭"), new WordItem("mama", "妈妈", "家庭"),
                        new WordItem("gege", "哥哥", "家庭"), new WordItem("jiejie", "姐姐", "家庭"),
                        new WordItem("didi", "弟弟", "家庭"), new WordItem("meimei", "妹妹", "家庭"),
                        new WordItem("yeye", "爷爷", "家庭"), new WordItem("nainai", "奶奶", "家庭"),
                        new WordItem("shushu", "叔叔", "家庭"), new WordItem("ayi", "阿姨", "家庭"),
                        // 学校
                        new WordItem("xuesheng", "学生", "身份"), new WordItem("laoshi", "老师", "身份"),
                        new WordItem("xuexiao", "学校", "地点"), new WordItem("jiaoshi", "教室", "地点"),
                        new WordItem("ketang", "课堂", "地点"), new WordItem("caochang", "操场", "地点"),
                        new WordItem("tushuguan", "图书馆", "地点"), new WordItem("shitang", "食堂", "地点"),
                        // 食物
                        new WordItem("mifan", "米饭", "食物"), new WordItem("miantiao", "面条", "食物"),
                        new WordItem("jiaozi", "饺子", "食物"), new WordItem("baozi", "包子", "食物"),
                        new WordItem("mantou", "馒头", "食物"), new WordItem("zhou", "粥", "食物"),
                        new WordItem("shuiguo", "水果", "食物"), new WordItem("shucai", "蔬菜", "食物"),
                        new WordItem("niunai", "牛奶", "饮料"), new WordItem("guozhi", "果汁", "饮料"),
                        new WordItem("kuaile", "快乐", "情绪"), new WordItem("kaixin", "开心", "情绪"),
                        new WordItem("nanguo", "难过", "情绪"), new WordItem("shengqi", "生气", "情绪"),
                        new WordItem("haipa", "害怕", "情绪"), new WordItem("jinzhang", "紧张", "情绪"),
                        // 学习
                        new WordItem("xuexi", "学习", "动词"), new WordItem("zuoye", "作业", "名词"),
                        new WordItem("kaoshi", "考试", "名词"), new WordItem("fuxi", "复习", "动词"),
                        new WordItem("yuedu", "阅读", "动词"), new WordItem("xiezi", "写字", "动词"),
                        new WordItem("sikao", "思考", "动词"), new WordItem("lixiang", "理想", "名词"),
                        // 自然
                        new WordItem("taiyang", "太阳", "自然"), new WordItem("yueliang", "月亮", "自然"),
                        new WordItem("xingxing", "星星", "自然"), new WordItem("yuncai", "云彩", "自然"),
                        new WordItem("xiaoyu", "小雨", "天气"), new WordItem("dayu", "大雨", "天气"),
                        new WordItem("daxue", "大雪", "天气"), new WordItem("dafeng", "大风", "天气"),
                        // 地点
                        new WordItem("gongyuan", "公园", "地点"), new WordItem("yiyuan", "医院", "地点"),
                        new WordItem("shangdian", "商店", "地点"), new WordItem("chaoji", "超市", "地点"),
                        new WordItem("yinhang", "银行", "地点"), new WordItem("youju", "邮局", "地点"),
                    });
                }
                else // hard - 成语词组
                {
                    words.AddRange(new[] {
                        new WordItem("chunnuanhuakai", "春暖花开", "形容春天美好"),
                        new WordItem("qiugaoqishuang", "秋高气爽", "形容秋天晴朗"),
                        new WordItem("xiaoruzhuhuo", "夏日如火", "形容夏天炎热"),
                        new WordItem("dongrinuanyang", "冬日暖阳", "形容冬天温暖"),
                        new WordItem("niaoyuhuaxiang", "鸟语花香", "形容春天美景"),
                        new WordItem("fenghecili", "风和日丽", "形容天气好"),
                        new WordItem("dianingshuixiu", "山清水秀", "形容风景美"),
                        new WordItem("haitianyise", "海天一色", "形容海景"),
                        new WordItem("manmianchunfeng", "满面春风", "形容高兴"),
                        new WordItem("xiquyangyang", "喜气洋洋", "形容喜庆"),
                        new WordItem("shencaiyiyi", "神采奕奕", "形容精神好"),
                        new WordItem("rongguanghuanfa", "容光焕发", "形容精神好"),
                        new WordItem("tingtingyuli", "亭亭玉立", "形容女子美"),
                        new WordItem("yibiaorentang", "仪表堂堂", "形容男子美"),
                        new WordItem("xinxuechaofushi", "心潮澎湃", "形容激动"),
                        new WordItem("ganjiwenling", "感激涕零", "形容感激"),
                        new WordItem("xijingleidong", "欣喜若狂", "形容高兴"),
                        new WordItem("beijiyijiao", "悲喜交加", "形容心情复杂"),
                        new WordItem("yixinyiyi", "一心一意", "形容专心"),
                        new WordItem("quanshenquanyi", "全心全意", "形容专心"),
                        new WordItem("zhuanshizhi", "专心致志", "形容专心"),
                        new WordItem("feiqinwangshi", "废寝忘食", "形容努力"),
                        new WordItem("yongbayanqi", "勇往直前", "形容勇敢"),
                        new WordItem("zhizhibuyu", "坚持不懈", "形容坚持"),
                        new WordItem("zilianglisheng", "自强不息", "形容自强"),
                        new WordItem("xueruxuexing", "学而不厌", "形容好学"),
                        new WordItem("weibuxizhiqu", "循循善诱", "形容教学"),
                        new WordItem("haihuiweijuan", "诲人不倦", "形容教学"),
                        new WordItem("jurenbuxi", "锲而不舍", "形容坚持"),
                        new WordItem("chixinyihan", "持之以恒", "形容坚持"),
                        new WordItem("tuanjieliang", "团结友爱", "形容团结"),
                        new WordItem("huxxiangzhu", "互帮互助", "形容互助"),
                        new WordItem("tongxinxieeli", "同心协力", "形容合作"),
                        new WordItem("zhongzhihecheng", "众志成城", "形容团结"),
                        new WordItem("chengxinxiai", "诚信待人", "形容诚信"),
                        new WordItem("yanyouxin", "言而有信", "形容守信"),
                        new WordItem("yiyuweiding", "一言为定", "形容守信"),
                        new WordItem("jingshengqingxiang", "敬仰勤学", "形容学习"),
                        new WordItem("zunjingshiang", "尊师重道", "形容尊敬"),
                    });
                }
            }
            // 中文古诗词练习
            else if (mode == "chinese_type2")
            {
                if (difficulty == "easy")
                {
                    words.AddRange(new[] {
                        // 静夜思
                        new WordItem("chuang", "床", "床前明月光"),
                        new WordItem("qian", "前", "床前明月光"),
                        new WordItem("ming", "明", "明月"),
                        new WordItem("yue", "月", "月亮"),
                        new WordItem("guang", "光", "月光"),
                        new WordItem("yi", "疑", "怀疑"),
                        new WordItem("shi", "是", "疑是"),
                        new WordItem("di", "地", "地上"),
                        new WordItem("shuang", "霜", "白霜"),
                        new WordItem("ju", "举", "举起"),
                        new WordItem("tou", "头", "抬头"),
                        new WordItem("wang", "望", "仰望"),
                        new WordItem("si", "思", "思念"),
                        new WordItem("gu", "故", "故乡"),
                        new WordItem("xiang", "乡", "家乡"),
                        // 登鹳雀楼
                        new WordItem("bai", "白", "白日"),
                        new WordItem("ri", "日", "太阳"),
                        new WordItem("yi", "依", "依靠"),
                        new WordItem("shan", "山", "高山"),
                        new WordItem("jin", "尽", "尽头"),
                        new WordItem("huang", "黄", "黄河"),
                        new WordItem("he", "河", "河流"),
                        new WordItem("ru", "入", "流入"),
                        new WordItem("hai", "海", "大海"),
                        new WordItem("liu", "流", "流水"),
                        new WordItem("yu", "欲", "想要"),
                        new WordItem("qiong", "穷", "穷尽"),
                        new WordItem("qian", "千", "千里"),
                        new WordItem("mu", "目", "眼睛"),
                        new WordItem("geng", "更", "更加"),
                        new WordItem("ceng", "层", "一层"),
                        new WordItem("lou", "楼", "高楼"),
                        // 春晓
                        new WordItem("chun", "春", "春天"),
                        new WordItem("mian", "眠", "睡眠"),
                        new WordItem("bu", "不", "没有"),
                        new WordItem("jue", "觉", "感觉"),
                        new WordItem("xiao", "晓", "天亮"),
                        new WordItem("chu", "处", "到处"),
                        new WordItem("wen", "闻", "听见"),
                        new WordItem("ti", "啼", "啼叫"),
                        new WordItem("niao", "鸟", "小鸟"),
                        new WordItem("ye", "夜", "夜晚"),
                        new WordItem("lai", "来", "来临"),
                        new WordItem("feng", "风", "风雨"),
                        new WordItem("yu", "雨", "雨声"),
                        new WordItem("sheng", "声", "声音"),
                        new WordItem("hua", "花", "花朵"),
                        new WordItem("luo", "落", "飘落"),
                        new WordItem("zhi", "知", "知道"),
                        new WordItem("duo", "多", "多少"),
                        new WordItem("shao", "少", "多少"),
                    });
                }
                else if (difficulty == "medium")
                {
                    words.AddRange(new[] {
                        // 静夜思词组
                        new WordItem("chuangqian", "床前", "床前明月光"),
                        new WordItem("mingyue", "明月", "明月光"),
                        new WordItem("yueguang", "月光", "床前明月光"),
                        new WordItem("yishi", "疑是", "疑是地上霜"),
                        new WordItem("dishang", "地上", "地上霜"),
                        new WordItem("dishuang", "地霜", "地上霜"),
                        new WordItem("jutou", "举头", "举头望明月"),
                        new WordItem("wangmingyue", "望明月", "举头望明月"),
                        new WordItem("ditou", "低头", "低头思故乡"),
                        new WordItem("siguxiang", "思故乡", "低头思故乡"),
                        new WordItem("guxiang", "故乡", "思念故乡"),
                        // 登鹳雀楼词组
                        new WordItem("bairi", "白日", "白日依山尽"),
                        new WordItem("yishanjin", "依山尽", "白日依山尽"),
                        new WordItem("huanghe", "黄河", "黄河入海流"),
                        new WordItem("ruhailiu", "入海流", "黄河入海流"),
                        new WordItem("hailiu", "海流", "流入大海"),
                        new WordItem("yuqiong", "欲穷", "欲穷千里目"),
                        new WordItem("qianlimu", "千里目", "欲穷千里目"),
                        new WordItem("qianli", "千里", "很远"),
                        new WordItem("gengshang", "更上", "更上一层楼"),
                        new WordItem("yicenglou", "一层楼", "更上一层楼"),
                        new WordItem("cenglou", "层楼", "一层楼"),
                        // 春晓词组
                        new WordItem("chunmian", "春眠", "春眠不觉晓"),
                        new WordItem("bujue", "不觉", "不知不觉"),
                        new WordItem("bujue", "不晓", "天不亮"),
                        new WordItem("chuchu", "处处", "到处"),
                        new WordItem("wenti", "闻啼", "听到啼叫"),
                        new WordItem("tinao", "啼鸟", "鸟叫"),
                        new WordItem("yelai", "夜来", "夜里来"),
                        new WordItem("fengyu", "风雨", "风雨"),
                        new WordItem("yusheng", "雨声", "下雨声"),
                        new WordItem("hualuo", "花落", "花落下"),
                        new WordItem("zhiduo", "知多", "知道多少"),
                        new WordItem("duoshao", "多少", "多少花落"),
                        // 游子吟词组
                        new WordItem("cimu", "慈母", "慈祥的母亲"),
                        new WordItem("shouzhong", "手中", "手里面"),
                        new WordItem("shouzhongxian", "手中线", "手里的线"),
                        new WordItem("youzi", "游子", "远行的儿子"),
                        new WordItem("shenshang", "身上", "身体上"),
                        new WordItem("shenshangyi", "身上衣", "身上的衣服"),
                        new WordItem("linxing", "临行", "即将出发"),
                        new WordItem("mimifeng", "密密缝", "缝得很密"),
                        new WordItem("yikong", "意恐", "担心"),
                        new WordItem("chichigui", "迟迟归", "很晚才回"),
                        new WordItem("shuiyan", "谁言", "谁说"),
                        new WordItem("cuncao", "寸草", "小草"),
                        new WordItem("caoxin", "草心", "小草的心"),
                        new WordItem("baode", "报得", "报答"),
                        new WordItem("sanchun", "三春", "春天的三个月"),
                        new WordItem("chunhui", "春晖", "春天的阳光"),
                    });
                }
                else // hard - 完整诗句
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
                        new WordItem("ligongyuanshangcao", "离离原上草", "白居易《草》"),
                        new WordItem("yisuiyikurong", "一岁一枯荣", "白居易《草》"),
                        new WordItem("yehuoshaoibujin", "野火烧不尽", "白居易《草》"),
                        new WordItem("chunfengchuanyousheng", "春风吹又生", "白居易《草》"),
                        new WordItem("baifuyizhangshui", "白毛浮绿水", "骆宾王《咏鹅》"),
                        new WordItem("hongzhangbqingingbo", "红掌拨清波", "骆宾王《咏鹅》"),
                    });
                }
            }
            // 英文单词练习
            else if (mode == "english_type1")
            {
                if (difficulty == "easy")
                {
                    words.AddRange(new[] {
                        // 简单单词
                        new WordItem("cat", "猫", "动物"), new WordItem("dog", "狗", "动物"),
                        new WordItem("pig", "猪", "动物"), new WordItem("cow", "牛", "动物"),
                        new WordItem("sheep", "羊", "动物"), new WordItem("bird", "鸟", "动物"),
                        new WordItem("fish", "鱼", "动物"), new WordItem("hen", "母鸡", "动物"),
                        new WordItem("duck", "鸭子", "动物"), new WordItem("horse", "马", "动物"),
                        new WordItem("apple", "苹果", "水果"), new WordItem("pear", "梨", "水果"),
                        new WordItem("peach", "桃子", "水果"), new WordItem("grape", "葡萄", "水果"),
                        new WordItem("banana", "香蕉", "水果"), new WordItem("orange", "橙子", "水果"),
                        new WordItem("book", "书", "物品"), new WordItem("pen", "钢笔", "物品"),
                        new WordItem("bag", "书包", "物品"), new WordItem("box", "盒子", "物品"),
                        new WordItem("cup", "杯子", "物品"), new WordItem("door", "门", "物品"),
                        new WordItem("red", "红色", "颜色"), new WordItem("blue", "蓝色", "颜色"),
                        new WordItem("green", "绿色", "颜色"), new WordItem("yellow", "黄色", "颜色"),
                        new WordItem("black", "黑色", "颜色"), new WordItem("white", "白色", "颜色"),
                        new WordItem("one", "一", "数字"), new WordItem("two", "二", "数字"),
                        new WordItem("three", "三", "数字"), new WordItem("four", "四", "数字"),
                        new WordItem("five", "五", "数字"), new WordItem("six", "六", "数字"),
                        new WordItem("seven", "七", "数字"), new WordItem("eight", "八", "数字"),
                        new WordItem("nine", "九", "数字"), new WordItem("ten", "十", "数字"),
                        new WordItem("sun", "太阳", "自然"), new WordItem("moon", "月亮", "自然"),
                        new WordItem("star", "星星", "自然"), new WordItem("tree", "树", "自然"),
                        new WordItem("flower", "花", "自然"), new WordItem("water", "水", "自然"),
                        new WordItem("father", "爸爸", "家庭"), new WordItem("mother", "妈妈", "家庭"),
                        new WordItem("sister", "姐妹", "家庭"), new WordItem("brother", "兄弟", "家庭"),
                    });
                }
                else if (difficulty == "medium")
                {
                    words.AddRange(new[] {
                        // 常用单词
                        new WordItem("teacher", "老师", "职业"), new WordItem("student", "学生", "职业"),
                        new WordItem("doctor", "医生", "职业"), new WordItem("nurse", "护士", "职业"),
                        new WordItem("driver", "司机", "职业"), new WordItem("farmer", "农民", "职业"),
                        new WordItem("worker", "工人", "职业"), new WordItem("soldier", "士兵", "职业"),
                        new WordItem("school", "学校", "地点"), new WordItem("hospital", "医院", "地点"),
                        new WordItem("library", "图书馆", "地点"), new WordItem("station", "车站", "地点"),
                        new WordItem("airport", "机场", "地点"), new WordItem("museum", "博物馆", "地点"),
                        new WordItem("breakfast", "早餐", "食物"), new WordItem("lunch", "午餐", "食物"),
                        new WordItem("dinner", "晚餐", "食物"), new WordItem("supper", "晚餐", "食物"),
                        new WordItem("morning", "早上", "时间"), new WordItem("afternoon", "下午", "时间"),
                        new WordItem("evening", "傍晚", "时间"), new WordItem("night", "夜晚", "时间"),
                        new WordItem("monday", "星期一", "时间"), new WordItem("tuesday", "星期二", "时间"),
                        new WordItem("wednesday", "星期三", "时间"), new WordItem("thursday", "星期四", "时间"),
                        new WordItem("friday", "星期五", "时间"), new WordItem("saturday", "星期六", "时间"),
                        new WordItem("sunday", "星期日", "时间"), new WordItem("spring", "春天", "季节"),
                        new WordItem("summer", "夏天", "季节"), new WordItem("autumn", "秋天", "季节"),
                        new WordItem("winter", "冬天", "季节"), new WordItem("weather", "天气", "自然"),
                        new WordItem("cloud", "云", "自然"), new WordItem("rain", "雨", "自然"),
                        new WordItem("snow", "雪", "自然"), new WordItem("wind", "风", "自然"),
                        new WordItem("mountain", "山", "自然"), new WordItem("river", "河", "自然"),
                        new WordItem("sea", "海", "自然"), new WordItem("lake", "湖", "自然"),
                        new WordItem("computer", "电脑", "物品"), new WordItem("telephone", "电话", "物品"),
                        new WordItem("television", "电视", "物品"), new WordItem("window", "窗户", "物品"),
                        new WordItem("kitchen", "厨房", "房间"), new WordItem("bedroom", "卧室", "房间"),
                        new WordItem("bathroom", "浴室", "房间"), new WordItem("classroom", "教室", "房间"),
                    });
                }
                else // hard - 较长单词
                {
                    words.AddRange(new[] {
                        new WordItem("beautiful", "美丽的", "形容词"),
                        new WordItem("wonderful", "精彩的", "形容词"),
                        new WordItem("important", "重要的", "形容词"),
                        new WordItem("different", "不同的", "形容词"),
                        new WordItem("interesting", "有趣的", "形容词"),
                        new WordItem("exciting", "令人兴奋的", "形容词"),
                        new WordItem("dangerous", "危险的", "形容词"),
                        new WordItem("difficult", "困难的", "形容词"),
                        new WordItem("comfortable", "舒适的", "形容词"),
                        new WordItem("expensive", "昂贵的", "形容词"),
                        new WordItem("excellent", "优秀的", "形容词"),
                        new WordItem("intelligent", "聪明的", "形容词"),
                        new WordItem("knowledge", "知识", "名词"),
                        new WordItem("education", "教育", "名词"),
                        new WordItem("experience", "经验", "名词"),
                        new WordItem("information", "信息", "名词"),
                        new WordItem("environment", "环境", "名词"),
                        new WordItem("technology", "技术", "名词"),
                        new WordItem("dictionary", "字典", "名词"),
                        new WordItem("university", "大学", "名词"),
                        new WordItem("restaurant", "餐厅", "名词"),
                        new WordItem("supermarket", "超市", "名词"),
                        new WordItem("vegetable", "蔬菜", "名词"),
                        new WordItem("chocolate", "巧克力", "名词"),
                        new WordItem("sandwich", "三明治", "名词"),
                        new WordItem("tomorrow", "明天", "时间"),
                        new WordItem("yesterday", "昨天", "时间"),
                        new WordItem("afternoon", "下午", "时间"),
                        new WordItem("sometimes", "有时", "副词"),
                        new WordItem("usually", "通常", "副词"),
                        new WordItem("always", "总是", "副词"),
                        new WordItem("never", "从不", "副词"),
                        new WordItem("quickly", "快地", "副词"),
                        new WordItem("slowly", "慢地", "副词"),
                        new WordItem("carefully", "仔细地", "副词"),
                        new WordItem("happily", "快乐地", "副词"),
                        new WordItem("understand", "理解", "动词"),
                        new WordItem("remember", "记住", "动词"),
                        new WordItem("practice", "练习", "动词"),
                        new WordItem("continue", "继续", "动词"),
                        new WordItem("discover", "发现", "动词"),
                        new WordItem("imagine", "想象", "动词"),
                        new WordItem("celebrate", "庆祝", "动词"),
                        new WordItem("communicate", "交流", "动词"),
                    });
                }
            }
            // 英文句子练习
            else if (mode == "english_type2")
            {
                if (difficulty == "easy")
                {
                    words.AddRange(new[] {
                        new WordItem("good morning", "早上好", "问候"),
                        new WordItem("good night", "晚安", "告别"),
                        new WordItem("thank you", "谢谢", "感谢"),
                        new WordItem("excuse me", "打扰一下", "礼貌"),
                        new WordItem("im sorry", "对不起", "道歉"),
                        new WordItem("sit down", "坐下", "指令"),
                        new WordItem("stand up", "站起来", "指令"),
                        new WordItem("look at me", "看着我", "指令"),
                        new WordItem("listen to me", "听我说", "指令"),
                        new WordItem("very good", "很好", "鼓励"),
                        new WordItem("well done", "做得好", "鼓励"),
                        new WordItem("try again", "再试一次", "鼓励"),
                        new WordItem("here you are", "给你", "给予"),
                        new WordItem("lets go", "走吧", "提议"),
                        new WordItem("come here", "过来", "指令"),
                        new WordItem("go away", "走开", "指令"),
                        new WordItem("i love you", "我爱你", "表达"),
                        new WordItem("i like it", "我喜欢它", "表达"),
                        new WordItem("good luck", "祝你好运", "祝福"),
                        new WordItem("have fun", "玩得开心", "祝福"),
                        new WordItem("see you", "再见", "告别"),
                        new WordItem("good bye", "再见", "告别"),
                        new WordItem("nice to meet you", "很高兴见到你", "问候"),
                        new WordItem("how are you", "你好吗", "问候"),
                        new WordItem("im fine", "我很好", "回答"),
                        new WordItem("this way", "这边走", "指引"),
                        new WordItem("of course", "当然", "肯定"),
                        new WordItem("wait a minute", "等一下", "请求"),
                        new WordItem("thats right", "对", "肯定"),
                        new WordItem("thats wrong", "错", "否定"),
                    });
                }
                else if (difficulty == "medium")
                {
                    words.AddRange(new[] {
                        new WordItem("good morning teacher", "老师早上好", "问候"),
                        new WordItem("how are you today", "你今天好吗", "问候"),
                        new WordItem("im fine thank you", "我很好谢谢", "回答"),
                        new WordItem("nice to meet you too", "我也很高兴见到你", "回答"),
                        new WordItem("thank you very much", "非常感谢", "感谢"),
                        new WordItem("you are welcome", "不客气", "礼貌"),
                        new WordItem("excuse me can you help me", "打扰一下你能帮我吗", "请求"),
                        new WordItem("im sorry im late", "对不起我迟到了", "道歉"),
                        new WordItem("its okay dont worry", "没关系别担心", "安慰"),
                        new WordItem("what is your name", "你叫什么名字", "询问"),
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
                    });
                }
                else // hard - 较长句子
                {
                    words.AddRange(new[] {
                        new WordItem("what time do you usually get up in the morning", "你通常早上几点起床", "日常"),
                        new WordItem("i usually get up at seven oclock", "我通常七点起床", "日常"),
                        new WordItem("how do you go to school every day", "你每天怎么去学校", "日常"),
                        new WordItem("i go to school by bus", "我坐公共汽车去学校", "日常"),
                        new WordItem("what is your favorite subject at school", "你在学校最喜欢的科目是什么", "学校"),
                        new WordItem("my favorite subject is english", "我最喜欢的科目是英语", "学校"),
                        new WordItem("what do you want to be in the future", "你将来想做什么", "未来"),
                        new WordItem("i want to be a doctor", "我想当医生", "未来"),
                        new WordItem("what did you do last weekend", "你上周末做了什么", "过去"),
                        new WordItem("i visited my grandparents", "我去看望了我的祖父母", "过去"),
                        new WordItem("what are you going to do tomorrow", "你明天打算做什么", "未来"),
                        new WordItem("i am going to visit my friend", "我打算去看望我的朋友", "未来"),
                        new WordItem("can you tell me the way to the library", "你能告诉我去图书馆的路吗", "问路"),
                        new WordItem("go straight and turn left", "直走然后左转", "指引"),
                        new WordItem("how much is this book", "这本书多少钱", "购物"),
                        new WordItem("it costs twenty yuan", "它二十元", "购物"),
                        new WordItem("what is the weather like today", "今天天气怎么样", "天气"),
                        new WordItem("it is sunny and warm", "天气晴朗温暖", "天气"),
                        new WordItem("why do you like summer best", "你为什么最喜欢夏天", "季节"),
                        new WordItem("because i can go swimming", "因为我可以去游泳", "季节"),
                        new WordItem("what should we do to protect the environment", "我们应该做什么来保护环境", "环保"),
                        new WordItem("we should plant more trees", "我们应该种更多的树", "环保"),
                        new WordItem("do you like reading books", "你喜欢读书吗", "爱好"),
                        new WordItem("yes i read books every day", "是的，我每天都读书", "爱好"),
                        new WordItem("happy birthday to you", "祝你生日快乐", "祝福"),
                        new WordItem("thank you very much for coming", "非常感谢你的到来", "感谢"),
                        new WordItem("merry christmas and happy new year", "圣诞快乐新年快乐", "节日"),
                        new WordItem("best wishes for you and your family", "祝你和你家人一切顺利", "祝福"),
                        new WordItem("i hope you have a wonderful time", "希望你玩得愉快", "祝福"),
                        new WordItem("please remember to do your homework", "请记得做作业", "提醒"),
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
                
                ContinueBtn.Visibility = Visibility.Visible;
                BackBtn.Visibility = Visibility.Visible;
                CloseBtn.Visibility = Visibility.Collapsed;
                return;
            }
            
            var word = _words[_currentIndex];
            
            WordText.Text = word.Display;
            WordText.FontSize = _difficulty == "hard" ? 36 : 56;
            
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
                    ResultText.Text = $"❌ 答案：{word.Word}";
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
            
            ContinueBtn.Visibility = Visibility.Collapsed;
            BackBtn.Visibility = Visibility.Collapsed;
            CloseBtn.Visibility = Visibility.Visible;
            
            ShowCurrentWord();
        }
        
        private void OnBackToMain(object sender, RoutedEventArgs e)
        {
            Close();
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
