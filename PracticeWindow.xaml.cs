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
                PromptText.Text = "请输入对应的汉字：";
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
            
            // 中文词组练习 - 显示含义/拼音，输入汉字
            if (mode == "chinese_type1")
            {
                if (difficulty == "easy")
                {
                    // 显示含义，输入单字
                    words.AddRange(new[] {
                        new WordItem("一", "数字1 (yī)", "数字"),
                        new WordItem("二", "数字2 (èr)", "数字"),
                        new WordItem("三", "数字3 (sān)", "数字"),
                        new WordItem("四", "数字4 (sì)", "数字"),
                        new WordItem("五", "数字5 (wǔ)", "数字"),
                        new WordItem("六", "数字6 (liù)", "数字"),
                        new WordItem("七", "数字7 (qī)", "数字"),
                        new WordItem("八", "数字8 (bā)", "数字"),
                        new WordItem("九", "数字9 (jiǔ)", "数字"),
                        new WordItem("十", "数字10 (shí)", "数字"),
                        new WordItem("百", "数字100 (bǎi)", "数字"),
                        new WordItem("千", "数字1000 (qiān)", "数字"),
                        new WordItem("万", "数字10000 (wàn)", "数字"),
                        new WordItem("红", "红色 (hóng)", "颜色"),
                        new WordItem("黄", "黄色 (huáng)", "颜色"),
                        new WordItem("蓝", "蓝色 (lán)", "颜色"),
                        new WordItem("绿", "绿色 (lǜ)", "颜色"),
                        new WordItem("白", "白色 (bái)", "颜色"),
                        new WordItem("黑", "黑色 (hēi)", "颜色"),
                        new WordItem("紫", "紫色 (zǐ)", "颜色"),
                        new WordItem("橙", "橙色 (chéng)", "颜色"),
                        new WordItem("马", "动物：马 (mǎ)", "动物"),
                        new WordItem("牛", "动物：牛 (niú)", "动物"),
                        new WordItem("羊", "动物：羊 (yáng)", "动物"),
                        new WordItem("猪", "动物：猪 (zhū)", "动物"),
                        new WordItem("狗", "动物：狗 (gǒu)", "动物"),
                        new WordItem("猫", "动物：猫 (māo)", "动物"),
                        new WordItem("鸡", "动物：鸡 (jī)", "动物"),
                        new WordItem("鸭", "动物：鸭 (yā)", "动物"),
                        new WordItem("鱼", "动物：鱼 (yú)", "动物"),
                        new WordItem("鸟", "动物：鸟 (niǎo)", "动物"),
                        new WordItem("虎", "动物：老虎 (hǔ)", "动物"),
                        new WordItem("龙", "动物：龙 (lóng)", "动物"),
                        new WordItem("蛇", "动物：蛇 (shé)", "动物"),
                        new WordItem("鼠", "动物：老鼠 (shǔ)", "动物"),
                        new WordItem("兔", "动物：兔子 (tù)", "动物"),
                        new WordItem("草", "植物：草 (cǎo)", "植物"),
                        new WordItem("花", "植物：花 (huā)", "植物"),
                        new WordItem("树", "植物：树 (shù)", "植物"),
                        new WordItem("叶", "植物：叶子 (yè)", "植物"),
                        new WordItem("果", "植物：水果 (guǒ)", "植物"),
                        new WordItem("头", "身体：头 (tóu)", "身体"),
                        new WordItem("手", "身体：手 (shǒu)", "身体"),
                        new WordItem("脚", "身体：脚 (jiǎo)", "身体"),
                        new WordItem("目", "身体：眼睛 (mù)", "身体"),
                        new WordItem("耳", "身体：耳朵 (ěr)", "身体"),
                        new WordItem("口", "身体：嘴巴 (kǒu)", "身体"),
                        new WordItem("鼻", "身体：鼻子 (bí)", "身体"),
                        new WordItem("牙", "身体：牙齿 (yá)", "身体"),
                        new WordItem("天", "自然：天空 (tiān)", "自然"),
                        new WordItem("地", "自然：大地 (dì)", "自然"),
                        new WordItem("日", "自然：太阳 (rì)", "自然"),
                        new WordItem("月", "自然：月亮 (yuè)", "自然"),
                        new WordItem("星", "自然：星星 (xīng)", "自然"),
                        new WordItem("云", "自然：云 (yún)", "自然"),
                        new WordItem("风", "自然：风 (fēng)", "自然"),
                        new WordItem("雨", "自然：雨 (yǔ)", "自然"),
                        new WordItem("雪", "自然：雪 (xuě)", "自然"),
                        new WordItem("山", "自然：山 (shān)", "自然"),
                        new WordItem("河", "自然：河 (hé)", "自然"),
                        new WordItem("海", "自然：海 (hǎi)", "自然"),
                        new WordItem("东", "方位：东方 (dōng)", "方位"),
                        new WordItem("西", "方位：西方 (xī)", "方位"),
                        new WordItem("南", "方位：南方 (nán)", "方位"),
                        new WordItem("北", "方位：北方 (běi)", "方位"),
                        new WordItem("上", "方位：上面 (shàng)", "方位"),
                        new WordItem("下", "方位：下面 (xià)", "方位"),
                        new WordItem("左", "方位：左边 (zuǒ)", "方位"),
                        new WordItem("右", "方位：右边 (yòu)", "方位"),
                        new WordItem("年", "时间：年 (nián)", "时间"),
                        new WordItem("月", "时间：月 (yuè)", "时间"),
                        new WordItem("日", "时间：日 (rì)", "时间"),
                        new WordItem("时", "时间：时 (shí)", "时间"),
                        new WordItem("早", "时间：早 (zǎo)", "时间"),
                        new WordItem("晚", "时间：晚 (wǎn)", "时间"),
                        new WordItem("吃", "动作：吃 (chī)", "动作"),
                        new WordItem("喝", "动作：喝 (hē)", "动作"),
                        new WordItem("走", "动作：走 (zǒu)", "动作"),
                        new WordItem("跑", "动作：跑 (pǎo)", "动作"),
                        new WordItem("看", "动作：看 (kàn)", "动作"),
                        new WordItem("听", "动作：听 (tīng)", "动作"),
                        new WordItem("说", "动作：说 (shuō)", "动作"),
                        new WordItem("写", "动作：写 (xiě)", "动作"),
                    });
                }
                else if (difficulty == "medium")
                {
                    // 显示含义，输入词组
                    words.AddRange(new[] {
                        new WordItem("你好", "问候语 (nǐ hǎo)", "问候"),
                        new WordItem("再见", "告别语 (zài jiàn)", "告别"),
                        new WordItem("谢谢", "感谢 (xiè xiè)", "感谢"),
                        new WordItem("对不起", "道歉 (duì bu qǐ)", "道歉"),
                        new WordItem("没关系", "原谅 (méi guān xi)", "原谅"),
                        new WordItem("不客气", "礼貌用语 (bù kè qi)", "礼貌"),
                        new WordItem("早上好", "早上问候 (zǎo shàng hǎo)", "问候"),
                        new WordItem("晚上好", "晚上问候 (wǎn shàng hǎo)", "问候"),
                        new WordItem("今天", "时间：今天 (jīn tiān)", "时间"),
                        new WordItem("明天", "时间：明天 (míng tiān)", "时间"),
                        new WordItem("昨天", "时间：昨天 (zuó tiān)", "时间"),
                        new WordItem("后天", "时间：后天 (hòu tiān)", "时间"),
                        new WordItem("前天", "时间：前天 (qián tiān)", "时间"),
                        new WordItem("每天", "时间：每天 (měi tiān)", "时间"),
                        new WordItem("早晨", "时间：早晨 (zǎo chén)", "时间"),
                        new WordItem("中午", "时间：中午 (zhōng wǔ)", "时间"),
                        new WordItem("傍晚", "时间：傍晚 (bàng wǎn)", "时间"),
                        new WordItem("上午", "时间：上午 (shàng wǔ)", "时间"),
                        new WordItem("下午", "时间：下午 (xià wǔ)", "时间"),
                        new WordItem("爸爸", "家庭：爸爸 (bà ba)", "家庭"),
                        new WordItem("妈妈", "家庭：妈妈 (mā ma)", "家庭"),
                        new WordItem("哥哥", "家庭：哥哥 (gē ge)", "家庭"),
                        new WordItem("姐姐", "家庭：姐姐 (jiě jie)", "家庭"),
                        new WordItem("弟弟", "家庭：弟弟 (dì di)", "家庭"),
                        new WordItem("妹妹", "家庭：妹妹 (mèi mei)", "家庭"),
                        new WordItem("爷爷", "家庭：爷爷 (yé ye)", "家庭"),
                        new WordItem("奶奶", "家庭：奶奶 (nǎi nai)", "家庭"),
                        new WordItem("学生", "身份：学生 (xué sheng)", "身份"),
                        new WordItem("老师", "身份：老师 (lǎo shī)", "身份"),
                        new WordItem("学校", "地点：学校 (xué xiào)", "地点"),
                        new WordItem("教室", "地点：教室 (jiào shì)", "地点"),
                        new WordItem("课堂", "地点：课堂 (kè táng)", "地点"),
                        new WordItem("操场", "地点：操场 (cāo chǎng)", "地点"),
                        new WordItem("图书馆", "地点：图书馆 (tú shū guǎn)", "地点"),
                        new WordItem("食堂", "地点：食堂 (shí táng)", "地点"),
                        new WordItem("米饭", "食物：米饭 (mǐ fàn)", "食物"),
                        new WordItem("面条", "食物：面条 (miàn tiáo)", "食物"),
                        new WordItem("饺子", "食物：饺子 (jiǎo zi)", "食物"),
                        new WordItem("包子", "食物：包子 (bāo zi)", "食物"),
                        new WordItem("馒头", "食物：馒头 (mán tou)", "食物"),
                        new WordItem("水果", "食物：水果 (shuǐ guǒ)", "食物"),
                        new WordItem("蔬菜", "食物：蔬菜 (shū cài)", "食物"),
                        new WordItem("牛奶", "饮料：牛奶 (niú nǎi)", "饮料"),
                        new WordItem("果汁", "饮料：果汁 (guǒ zhī)", "饮料"),
                        new WordItem("快乐", "情绪：快乐 (kuài lè)", "情绪"),
                        new WordItem("开心", "情绪：开心 (kāi xīn)", "情绪"),
                        new WordItem("难过", "情绪：难过 (nán guò)", "情绪"),
                        new WordItem("生气", "情绪：生气 (shēng qì)", "情绪"),
                        new WordItem("害怕", "情绪：害怕 (hài pà)", "情绪"),
                        new WordItem("学习", "动词：学习 (xué xí)", "学习"),
                        new WordItem("作业", "名词：作业 (zuò yè)", "学习"),
                        new WordItem("考试", "名词：考试 (kǎo shì)", "学习"),
                        new WordItem("复习", "动词：复习 (fù xí)", "学习"),
                        new WordItem("阅读", "动词：阅读 (yuè dú)", "学习"),
                        new WordItem("写字", "动词：写字 (xiě zì)", "学习"),
                        new WordItem("思考", "动词：思考 (sī kǎo)", "学习"),
                        new WordItem("太阳", "自然：太阳 (tài yáng)", "自然"),
                        new WordItem("月亮", "自然：月亮 (yuè liang)", "自然"),
                        new WordItem("星星", "自然：星星 (xīng xing)", "自然"),
                        new WordItem("云彩", "自然：云彩 (yún cai)", "自然"),
                        new WordItem("小雨", "天气：小雨 (xiǎo yǔ)", "天气"),
                        new WordItem("大雨", "天气：大雨 (dà yǔ)", "天气"),
                        new WordItem("大雪", "天气：大雪 (dà xuě)", "天气"),
                        new WordItem("大风", "天气：大风 (dà fēng)", "天气"),
                        new WordItem("公园", "地点：公园 (gōng yuán)", "地点"),
                        new WordItem("医院", "地点：医院 (yī yuàn)", "地点"),
                        new WordItem("商店", "地点：商店 (shāng diàn)", "地点"),
                        new WordItem("超市", "地点：超市 (chāo shì)", "地点"),
                        new WordItem("银行", "地点：银行 (yín háng)", "地点"),
                        new WordItem("邮局", "地点：邮局 (yóu jú)", "地点"),
                    });
                }
                else // hard - 成语词组
                {
                    words.AddRange(new[] {
                        new WordItem("春暖花开", "形容春天美好，万物复苏", "成语"),
                        new WordItem("秋高气爽", "形容秋天晴朗，空气清新", "成语"),
                        new WordItem("夏日如火", "形容夏天天气炎热", "成语"),
                        new WordItem("冬日暖阳", "形容冬天温暖美好", "成语"),
                        new WordItem("鸟语花香", "形容春天美好景象", "成语"),
                        new WordItem("风和日丽", "形容天气晴朗美好", "成语"),
                        new WordItem("山清水秀", "形容风景优美", "成语"),
                        new WordItem("海天一色", "形容大海与天空相连", "成语"),
                        new WordItem("满面春风", "形容心情愉快", "成语"),
                        new WordItem("喜气洋洋", "形容喜庆欢乐", "成语"),
                        new WordItem("神采奕奕", "形容精神饱满", "成语"),
                        new WordItem("容光焕发", "形容精神焕发", "成语"),
                        new WordItem("亭亭玉立", "形容女子身材修长美丽", "成语"),
                        new WordItem("仪表堂堂", "形容男子相貌端正", "成语"),
                        new WordItem("心潮澎湃", "形容心情激动", "成语"),
                        new WordItem("感激涕零", "形容非常感激", "成语"),
                        new WordItem("欣喜若狂", "形容非常高兴", "成语"),
                        new WordItem("悲喜交加", "形容心情复杂", "成语"),
                        new WordItem("一心一意", "形容专心致志", "成语"),
                        new WordItem("全心全意", "形容投入全部精力", "成语"),
                        new WordItem("专心致志", "形容一心一意", "成语"),
                        new WordItem("废寝忘食", "形容非常努力", "成语"),
                        new WordItem("勇往直前", "形容勇敢前进", "成语"),
                        new WordItem("坚持不懈", "形容持续努力", "成语"),
                        new WordItem("自强不息", "形容不断努力", "成语"),
                        new WordItem("学而不厌", "形容好学不倦", "成语"),
                        new WordItem("循循善诱", "形容善于教导", "成语"),
                        new WordItem("诲人不倦", "形容耐心教导", "成语"),
                        new WordItem("锲而不舍", "形容坚持不放弃", "成语"),
                        new WordItem("持之以恒", "形容长期坚持", "成语"),
                        new WordItem("团结友爱", "形容和睦相处", "成语"),
                        new WordItem("互帮互助", "形容相互帮助", "成语"),
                        new WordItem("同心协力", "形容共同努力", "成语"),
                        new WordItem("众志成城", "形容团结力量大", "成语"),
                        new WordItem("诚信待人", "形容诚实守信", "成语"),
                        new WordItem("言而有信", "形容说话算数", "成语"),
                        new WordItem("一言为定", "形容说话算数", "成语"),
                    });
                }
            }
            // 中文古诗词练习 - 显示含义/出处，输入诗句
            else if (mode == "chinese_type2")
            {
                if (difficulty == "easy")
                {
                    words.AddRange(new[] {
                        new WordItem("床", "睡觉用的家具 - 出自《静夜思》", "诗词字"),
                        new WordItem("前", "方位，前面 - 出自《静夜思》", "诗词字"),
                        new WordItem("明", "明亮 - 出自《静夜思》", "诗词字"),
                        new WordItem("月", "月亮 - 出自《静夜思》", "诗词字"),
                        new WordItem("光", "光线 - 出自《静夜思》", "诗词字"),
                        new WordItem("疑", "怀疑 - 出自《静夜思》", "诗词字"),
                        new WordItem("是", "表示判断 - 出自《静夜思》", "诗词字"),
                        new WordItem("地", "地面 - 出自《静夜思》", "诗词字"),
                        new WordItem("霜", "白色的冰晶 - 出自《静夜思》", "诗词字"),
                        new WordItem("举", "抬起 - 出自《静夜思》", "诗词字"),
                        new WordItem("头", "头部 - 出自《静夜思》", "诗词字"),
                        new WordItem("望", "看 - 出自《静夜思》", "诗词字"),
                        new WordItem("思", "思念 - 出自《静夜思》", "诗词字"),
                        new WordItem("故", "故乡 - 出自《静夜思》", "诗词字"),
                        new WordItem("乡", "家乡 - 出自《静夜思》", "诗词字"),
                        new WordItem("白", "白色 - 出自《登鹳雀楼》", "诗词字"),
                        new WordItem("日", "太阳 - 出自《登鹳雀楼》", "诗词字"),
                        new WordItem("依", "靠着 - 出自《登鹳雀楼》", "诗词字"),
                        new WordItem("山", "高山 - 出自《登鹳雀楼》", "诗词字"),
                        new WordItem("尽", "尽头 - 出自《登鹳雀楼》", "诗词字"),
                        new WordItem("黄", "黄色 - 出自《登鹳雀楼》", "诗词字"),
                        new WordItem("河", "河流 - 出自《登鹳雀楼》", "诗词字"),
                        new WordItem("入", "流入 - 出自《登鹳雀楼》", "诗词字"),
                        new WordItem("海", "大海 - 出自《登鹳雀楼》", "诗词字"),
                        new WordItem("流", "流动 - 出自《登鹳雀楼》", "诗词字"),
                        new WordItem("欲", "想要 - 出自《登鹳雀楼》", "诗词字"),
                        new WordItem("穷", "穷尽 - 出自《登鹳雀楼》", "诗词字"),
                        new WordItem("千", "数字1000 - 出自《登鹳雀楼》", "诗词字"),
                        new WordItem("目", "眼睛 - 出自《登鹳雀楼》", "诗词字"),
                        new WordItem("更", "更加 - 出自《登鹳雀楼》", "诗词字"),
                        new WordItem("层", "层次 - 出自《登鹳雀楼》", "诗词字"),
                        new WordItem("楼", "高楼 - 出自《登鹳雀楼》", "诗词字"),
                        new WordItem("春", "春天 - 出自《春晓》", "诗词字"),
                        new WordItem("眠", "睡眠 - 出自《春晓》", "诗词字"),
                        new WordItem("不", "否定 - 出自《春晓》", "诗词字"),
                        new WordItem("觉", "感觉 - 出自《春晓》", "诗词字"),
                        new WordItem("晓", "天亮 - 出自《春晓》", "诗词字"),
                        new WordItem("处", "地方 - 出自《春晓》", "诗词字"),
                        new WordItem("闻", "听到 - 出自《春晓》", "诗词字"),
                        new WordItem("啼", "叫 - 出自《春晓》", "诗词字"),
                        new WordItem("鸟", "鸟类 - 出自《春晓》", "诗词字"),
                        new WordItem("夜", "夜晚 - 出自《春晓》", "诗词字"),
                        new WordItem("来", "来临 - 出自《春晓》", "诗词字"),
                        new WordItem("风", "风 - 出自《春晓》", "诗词字"),
                        new WordItem("雨", "雨 - 出自《春晓》", "诗词字"),
                        new WordItem("声", "声音 - 出自《春晓》", "诗词字"),
                        new WordItem("花", "花朵 - 出自《春晓》", "诗词字"),
                        new WordItem("落", "飘落 - 出自《春晓》", "诗词字"),
                        new WordItem("知", "知道 - 出自《春晓》", "诗词字"),
                        new WordItem("多", "多少 - 出自《春晓》", "诗词字"),
                        new WordItem("少", "多少 - 出自《春晓》", "诗词字"),
                    });
                }
                else if (difficulty == "medium")
                {
                    words.AddRange(new[] {
                        new WordItem("床前", "床的前面 - 《静夜思》李白", "诗句词组"),
                        new WordItem("明月", "明亮的月亮 - 《静夜思》李白", "诗句词组"),
                        new WordItem("月光", "月亮的光 - 《静夜思》李白", "诗句词组"),
                        new WordItem("疑是", "怀疑是 - 《静夜思》李白", "诗句词组"),
                        new WordItem("地上", "地面上 - 《静夜思》李白", "诗句词组"),
                        new WordItem("地上霜", "地上的霜 - 《静夜思》李白", "诗句词组"),
                        new WordItem("举头", "抬起头 - 《静夜思》李白", "诗句词组"),
                        new WordItem("望明月", "看着明月 - 《静夜思》李白", "诗句词组"),
                        new WordItem("低头", "低下头 - 《静夜思》李白", "诗句词组"),
                        new WordItem("思故乡", "思念故乡 - 《静夜思》李白", "诗句词组"),
                        new WordItem("故乡", "家乡 - 《静夜思》李白", "诗句词组"),
                        new WordItem("白日", "明亮的太阳 - 《登鹳雀楼》王之涣", "诗句词组"),
                        new WordItem("依山尽", "靠着山消失 - 《登鹳雀楼》王之涣", "诗句词组"),
                        new WordItem("黄河", "黄色的河 - 《登鹳雀楼》王之涣", "诗句词组"),
                        new WordItem("入海流", "流入大海 - 《登鹳雀楼》王之涣", "诗句词组"),
                        new WordItem("海流", "流入海中 - 《登鹳雀楼》王之涣", "诗句词组"),
                        new WordItem("欲穷", "想要穷尽 - 《登鹳雀楼》王之涣", "诗句词组"),
                        new WordItem("千里目", "看千里远 - 《登鹳雀楼》王之涣", "诗句词组"),
                        new WordItem("千里", "很远 - 《登鹳雀楼》王之涣", "诗句词组"),
                        new WordItem("更上", "再上一层 - 《登鹳雀楼》王之涣", "诗句词组"),
                        new WordItem("一层楼", "一层高楼 - 《登鹳雀楼》王之涣", "诗句词组"),
                        new WordItem("层楼", "高楼的层 - 《登鹳雀楼》王之涣", "诗句词组"),
                        new WordItem("春眠", "春天的睡眠 - 《春晓》孟浩然", "诗句词组"),
                        new WordItem("不觉晓", "不知不觉天亮 - 《春晓》孟浩然", "诗句词组"),
                        new WordItem("处处", "到处 - 《春晓》孟浩然", "诗句词组"),
                        new WordItem("闻啼", "听到叫声 - 《春晓》孟浩然", "诗句词组"),
                        new WordItem("啼鸟", "鸟的叫声 - 《春晓》孟浩然", "诗句词组"),
                        new WordItem("夜来", "夜里来临 - 《春晓》孟浩然", "诗句词组"),
                        new WordItem("风雨", "风和雨 - 《春晓》孟浩然", "诗句词组"),
                        new WordItem("雨声", "下雨的声音 - 《春晓》孟浩然", "诗句词组"),
                        new WordItem("花落", "花朵飘落 - 《春晓》孟浩然", "诗句词组"),
                        new WordItem("知多", "知道有多少 - 《春晓》孟浩然", "诗句词组"),
                        new WordItem("多少", "数量 - 《春晓》孟浩然", "诗句词组"),
                        new WordItem("慈母", "慈祥的母亲 - 《游子吟》孟郊", "诗句词组"),
                        new WordItem("手中", "手里面 - 《游子吟》孟郊", "诗句词组"),
                        new WordItem("手中线", "手里的线 - 《游子吟》孟郊", "诗句词组"),
                        new WordItem("游子", "远行的儿子 - 《游子吟》孟郊", "诗句词组"),
                        new WordItem("身上", "身体上 - 《游子吟》孟郊", "诗句词组"),
                        new WordItem("身上衣", "身上的衣服 - 《游子吟》孟郊", "诗句词组"),
                        new WordItem("临行", "即将出发 - 《游子吟》孟郊", "诗句词组"),
                        new WordItem("密密缝", "缝得很密 - 《游子吟》孟郊", "诗句词组"),
                        new WordItem("意恐", "担心 - 《游子吟》孟郊", "诗句词组"),
                        new WordItem("迟迟归", "很晚才回 - 《游子吟》孟郊", "诗句词组"),
                        new WordItem("谁言", "谁说 - 《游子吟》孟郊", "诗句词组"),
                        new WordItem("寸草", "小草 - 《游子吟》孟郊", "诗句词组"),
                        new WordItem("草心", "小草的心 - 《游子吟》孟郊", "诗句词组"),
                        new WordItem("报得", "报答 - 《游子吟》孟郊", "诗句词组"),
                        new WordItem("三春", "春天的三个月 - 《游子吟》孟郊", "诗句词组"),
                        new WordItem("春晖", "春天的阳光 - 《游子吟》孟郊", "诗句词组"),
                    });
                }
                else // hard - 完整诗句
                {
                    words.AddRange(new[] {
                        new WordItem("床前明月光", "李白《静夜思》- 月光照在床前", "五言诗"),
                        new WordItem("疑是地上霜", "李白《静夜思》- 好像地上的霜", "五言诗"),
                        new WordItem("举头望明月", "李白《静夜思》- 抬头看明月", "五言诗"),
                        new WordItem("低头思故乡", "李白《静夜思》- 低头思念家乡", "五言诗"),
                        new WordItem("白日依山尽", "王之涣《登鹳雀楼》- 太阳靠着山消失", "五言诗"),
                        new WordItem("黄河入海流", "王之涣《登鹳雀楼》- 黄河奔流入海", "五言诗"),
                        new WordItem("欲穷千里目", "王之涣《登鹳雀楼》- 想要看到更远", "五言诗"),
                        new WordItem("更上一层楼", "王之涣《登鹳雀楼》- 再上一层楼", "五言诗"),
                        new WordItem("春眠不觉晓", "孟浩然《春晓》- 春天睡得香", "五言诗"),
                        new WordItem("处处闻啼鸟", "孟浩然《春晓》- 到处听到鸟叫", "五言诗"),
                        new WordItem("夜来风雨声", "孟浩然《春晓》- 昨夜风雨声", "五言诗"),
                        new WordItem("花落知多少", "孟浩然《春晓》- 不知花落多少", "五言诗"),
                        new WordItem("慈母手中线", "孟郊《游子吟》- 母亲手里的线", "五言诗"),
                        new WordItem("游子身上衣", "孟郊《游子吟》- 儿子身上的衣服", "五言诗"),
                        new WordItem("临行密密缝", "孟郊《游子吟》- 出发前密密缝制", "五言诗"),
                        new WordItem("意恐迟迟归", "孟郊《游子吟》- 担心儿子晚归", "五言诗"),
                        new WordItem("谁言寸草心", "孟郊《游子吟》- 谁说小草的心", "五言诗"),
                        new WordItem("报得三春晖", "孟郊《游子吟》- 能报答春天的阳光", "五言诗"),
                        new WordItem("红豆生南国", "王维《相思》- 红豆长在南方", "五言诗"),
                        new WordItem("春来发几枝", "王维《相思》- 春天发几枝", "五言诗"),
                        new WordItem("愿君多采撷", "王维《相思》- 希望你多采摘", "五言诗"),
                        new WordItem("此物最相思", "王维《相思》- 这东西最让人想念", "五言诗"),
                        new WordItem("离离原上草", "白居易《草》- 草原上的草", "五言诗"),
                        new WordItem("一岁一枯荣", "白居易《草》- 每年枯萎又繁荣", "五言诗"),
                        new WordItem("野火烧不尽", "白居易《草》- 野火烧不完", "五言诗"),
                        new WordItem("春风吹又生", "白居易《草》- 春风吹又生长", "五言诗"),
                        new WordItem("白毛浮绿水", "骆宾王《咏鹅》- 白毛浮在绿水上", "五言诗"),
                        new WordItem("红掌拨清波", "骆宾王《咏鹅》- 红掌拨动清波", "五言诗"),
                    });
                }
            }
            // 英文单词练习 - 显示中文，输入英文
            else if (mode == "english_type1")
            {
                if (difficulty == "easy")
                {
                    words.AddRange(new[] {
                        new WordItem("cat", "猫", "动物"),
                        new WordItem("dog", "狗", "动物"),
                        new WordItem("pig", "猪", "动物"),
                        new WordItem("cow", "牛", "动物"),
                        new WordItem("sheep", "羊", "动物"),
                        new WordItem("bird", "鸟", "动物"),
                        new WordItem("fish", "鱼", "动物"),
                        new WordItem("hen", "母鸡", "动物"),
                        new WordItem("duck", "鸭子", "动物"),
                        new WordItem("horse", "马", "动物"),
                        new WordItem("apple", "苹果", "水果"),
                        new WordItem("pear", "梨", "水果"),
                        new WordItem("peach", "桃子", "水果"),
                        new WordItem("grape", "葡萄", "水果"),
                        new WordItem("banana", "香蕉", "水果"),
                        new WordItem("orange", "橙子", "水果"),
                        new WordItem("book", "书", "物品"),
                        new WordItem("pen", "钢笔", "物品"),
                        new WordItem("bag", "书包", "物品"),
                        new WordItem("box", "盒子", "物品"),
                        new WordItem("cup", "杯子", "物品"),
                        new WordItem("door", "门", "物品"),
                        new WordItem("red", "红色", "颜色"),
                        new WordItem("blue", "蓝色", "颜色"),
                        new WordItem("green", "绿色", "颜色"),
                        new WordItem("yellow", "黄色", "颜色"),
                        new WordItem("black", "黑色", "颜色"),
                        new WordItem("white", "白色", "颜色"),
                        new WordItem("one", "一", "数字"),
                        new WordItem("two", "二", "数字"),
                        new WordItem("three", "三", "数字"),
                        new WordItem("four", "四", "数字"),
                        new WordItem("five", "五", "数字"),
                        new WordItem("six", "六", "数字"),
                        new WordItem("seven", "七", "数字"),
                        new WordItem("eight", "八", "数字"),
                        new WordItem("nine", "九", "数字"),
                        new WordItem("ten", "十", "数字"),
                        new WordItem("sun", "太阳", "自然"),
                        new WordItem("moon", "月亮", "自然"),
                        new WordItem("star", "星星", "自然"),
                        new WordItem("tree", "树", "自然"),
                        new WordItem("flower", "花", "自然"),
                        new WordItem("water", "水", "自然"),
                        new WordItem("father", "爸爸", "家庭"),
                        new WordItem("mother", "妈妈", "家庭"),
                        new WordItem("sister", "姐妹", "家庭"),
                        new WordItem("brother", "兄弟", "家庭"),
                    });
                }
                else if (difficulty == "medium")
                {
                    words.AddRange(new[] {
                        new WordItem("teacher", "老师", "职业"),
                        new WordItem("student", "学生", "职业"),
                        new WordItem("doctor", "医生", "职业"),
                        new WordItem("nurse", "护士", "职业"),
                        new WordItem("driver", "司机", "职业"),
                        new WordItem("farmer", "农民", "职业"),
                        new WordItem("worker", "工人", "职业"),
                        new WordItem("soldier", "士兵", "职业"),
                        new WordItem("school", "学校", "地点"),
                        new WordItem("hospital", "医院", "地点"),
                        new WordItem("library", "图书馆", "地点"),
                        new WordItem("station", "车站", "地点"),
                        new WordItem("airport", "机场", "地点"),
                        new WordItem("museum", "博物馆", "地点"),
                        new WordItem("breakfast", "早餐", "食物"),
                        new WordItem("lunch", "午餐", "食物"),
                        new WordItem("dinner", "晚餐", "食物"),
                        new WordItem("supper", "晚餐", "食物"),
                        new WordItem("morning", "早上", "时间"),
                        new WordItem("afternoon", "下午", "时间"),
                        new WordItem("evening", "傍晚", "时间"),
                        new WordItem("night", "夜晚", "时间"),
                        new WordItem("monday", "星期一", "时间"),
                        new WordItem("tuesday", "星期二", "时间"),
                        new WordItem("wednesday", "星期三", "时间"),
                        new WordItem("thursday", "星期四", "时间"),
                        new WordItem("friday", "星期五", "时间"),
                        new WordItem("saturday", "星期六", "时间"),
                        new WordItem("sunday", "星期日", "时间"),
                        new WordItem("spring", "春天", "季节"),
                        new WordItem("summer", "夏天", "季节"),
                        new WordItem("autumn", "秋天", "季节"),
                        new WordItem("winter", "冬天", "季节"),
                        new WordItem("weather", "天气", "自然"),
                        new WordItem("cloud", "云", "自然"),
                        new WordItem("rain", "雨", "自然"),
                        new WordItem("snow", "雪", "自然"),
                        new WordItem("wind", "风", "自然"),
                        new WordItem("mountain", "山", "自然"),
                        new WordItem("river", "河", "自然"),
                        new WordItem("sea", "海", "自然"),
                        new WordItem("lake", "湖", "自然"),
                        new WordItem("computer", "电脑", "物品"),
                        new WordItem("telephone", "电话", "物品"),
                        new WordItem("television", "电视", "物品"),
                        new WordItem("window", "窗户", "物品"),
                        new WordItem("kitchen", "厨房", "房间"),
                        new WordItem("bedroom", "卧室", "房间"),
                        new WordItem("bathroom", "浴室", "房间"),
                        new WordItem("classroom", "教室", "房间"),
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
            
            // 中文练习：显示含义（大字），用户输入汉字
            // 英文练习：显示中文（大字），用户输入英文
            WordText.Text = word.Display;
            WordText.FontSize = _difficulty == "hard" ? 32 : 44;
            MeaningText.Visibility = Visibility.Collapsed;
            
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
                var input = InputBox.Text.Trim();
                var correct = word.Word;
                
                if (string.IsNullOrEmpty(input))
                {
                    ResultText.Text = "❌ 请先输入！";
                    ResultText.Foreground = Brushes.Red;
                    return;
                }
                
                bool isCorrect = _mode.StartsWith("english") 
                    ? input.ToLower() == correct.ToLower() 
                    : input == correct;
                
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
        public string Word { get; set; } = "";      // 正确答案
        public string Display { get; set; } = "";   // 显示内容
        public string Meaning { get; set; } = "";   // 含义/提示
        
        public WordItem(string word, string display, string meaning)
        {
            Word = word;
            Display = display;
            Meaning = meaning;
        }
        
        public WordItem() { }
    }
}
