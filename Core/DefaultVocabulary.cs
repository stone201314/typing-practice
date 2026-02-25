using System.Collections.Generic;
using TypingPractice.Models;

namespace TypingPractice.Core
{
    public static class DefaultVocabulary
    {
        public static List<VocabularyItem> GetPinyinGrade1() => new()
        {
            new() { Word = "yi", Display = "一", Pinyin = "yī", Definition = "数字，表示最小的正整数", Example = "我有一本书。", Difficulty = 1, Category = "数字" },
            new() { Word = "er", Display = "二", Pinyin = "èr", Definition = "数字，表示一加一", Example = "我有两只手。", Difficulty = 1, Category = "数字" },
            new() { Word = "san", Display = "三", Pinyin = "sān", Definition = "数字，表示二加一", Example = "我家有三口人。", Difficulty = 1, Category = "数字" },
            new() { Word = "si", Display = "四", Pinyin = "sì", Definition = "数字4", Example = "四本书。", Difficulty = 1, Category = "数字" },
            new() { Word = "wu", Display = "五", Pinyin = "wǔ", Definition = "数字5", Example = "五只猫。", Difficulty = 1, Category = "数字" },
            new() { Word = "liu", Display = "六", Pinyin = "liù", Definition = "数字6", Example = "六个苹果。", Difficulty = 1, Category = "数字" },
            new() { Word = "qi", Display = "七", Pinyin = "qī", Definition = "数字7", Example = "七天。", Difficulty = 1, Category = "数字" },
            new() { Word = "ba", Display = "八", Pinyin = "bā", Definition = "数字8", Example = "八个人。", Difficulty = 1, Category = "数字" },
            new() { Word = "jiu", Display = "九", Pinyin = "jiǔ", Definition = "数字9", Example = "九个同学。", Difficulty = 1, Category = "数字" },
            new() { Word = "shi", Display = "十", Pinyin = "shí", Definition = "数字，表示九加一", Example = "我有十个手指。", Difficulty = 1, Category = "数字" },
            new() { Word = "da", Display = "大", Pinyin = "dà", Definition = "体积、面积等超过一般", Example = "这是一只大象。", Difficulty = 1, Category = "形容" },
            new() { Word = "xiao", Display = "小", Pinyin = "xiǎo", Definition = "体积、面积等低于一般", Example = "这是一只小鸟。", Difficulty = 1, Category = "形容" },
            new() { Word = "gao", Display = "高", Pinyin = "gāo", Definition = "从下到上距离大", Example = "这棵树很高。", Difficulty = 1, Category = "形容" },
            new() { Word = "chang", Display = "长", Pinyin = "cháng", Definition = "两点之间距离大", Example = "这条河很长。", Difficulty = 1, Category = "形容" },
            new() { Word = "duan", Display = "短", Pinyin = "duǎn", Definition = "两点之间距离小", Example = "这支笔很短。", Difficulty = 1, Category = "形容" },
            new() { Word = "tian", Display = "天", Pinyin = "tiān", Definition = "在地面以上的高空", Example = "天上有白云。", Difficulty = 1, Category = "自然" },
            new() { Word = "di", Display = "地", Pinyin = "dì", Definition = "地面", Example = "猫在屋子里。", Difficulty = 1, Category = "自然" },
            new() { Word = "ren", Display = "人", Pinyin = "rén", Definition = "人类", Example = "我是一个人。", Difficulty = 1, Category = "人物" },
            new() { Word = "shui", Display = "水", Pinyin = "shuǐ", Definition = "无色无味的液体", Example = "我喝水。", Difficulty = 1, Category = "自然" },
            new() { Word = "huo", Display = "火", Pinyin = "huǒ", Definition = "燃烧产生的光和热", Example = "不要玩火。", Difficulty = 1, Category = "自然" },
            new() { Word = "shan", Display = "山", Pinyin = "shān", Definition = "地面上高起的部分", Example = "远处有一座山。", Difficulty = 1, Category = "自然" },
            new() { Word = "ma", Display = "马", Pinyin = "mǎ", Definition = "一种家畜，可以骑", Example = "这是一匹马。", Difficulty = 1, Category = "动物" },
            new() { Word = "niao", Display = "鸟", Pinyin = "niǎo", Definition = "有羽毛会飞的动物", Example = "小鸟在树上。", Difficulty = 1, Category = "动物" },
            new() { Word = "yu", Display = "鱼", Pinyin = "yú", Definition = "生活在水中的动物", Example = "鱼在水里游。", Difficulty = 1, Category = "动物" },
            new() { Word = "cao", Display = "草", Pinyin = "cǎo", Definition = "草本植物的统称", Example = "草是绿色的。", Difficulty = 1, Category = "植物" },
            new() { Word = "hua", Display = "花", Pinyin = "huā", Definition = "植物的花朵", Example = "花很漂亮。", Difficulty = 1, Category = "植物" },
            new() { Word = "mu", Display = "木", Pinyin = "mù", Definition = "树木的统称", Example = "这是一棵木。", Difficulty = 1, Category = "自然" },
            new() { Word = "yue", Display = "月", Pinyin = "yuè", Definition = "月亮，地球的卫星", Example = "晚上看月亮。", Difficulty = 1, Category = "自然" },
            new() { Word = "ri", Display = "日", Pinyin = "rì", Definition = "太阳，白天", Example = "日出来了。", Difficulty = 1, Category = "自然" },
            new() { Word = "shou", Display = "手", Pinyin = "shǒu", Definition = "人体上肢前端能拿东西的部分", Example = "我有两只手。", Difficulty = 1, Category = "身体" },
        };
        
        public static List<VocabularyItem> GetEnglishGrade1() => new()
        {
            new() { Word = "apple", Definition = "苹果", Example = "I eat an apple.", Difficulty = 1, Category = "食物" },
            new() { Word = "banana", Definition = "香蕉", Example = "I like bananas.", Difficulty = 1, Category = "食物" },
            new() { Word = "book", Definition = "书", Example = "I read a book.", Difficulty = 1, Category = "物品" },
            new() { Word = "cat", Definition = "猫", Example = "I have a cat.", Difficulty = 1, Category = "动物" },
            new() { Word = "dog", Definition = "狗", Example = "The dog is running.", Difficulty = 1, Category = "动物" },
            new() { Word = "egg", Definition = "鸡蛋", Example = "I have an egg.", Difficulty = 1, Category = "食物" },
            new() { Word = "fish", Definition = "鱼", Example = "The fish is swimming.", Difficulty = 1, Category = "动物" },
            new() { Word = "girl", Definition = "女孩", Example = "She is a girl.", Difficulty = 1, Category = "人物" },
            new() { Word = "hand", Definition = "手", Example = "I have two hands.", Difficulty = 1, Category = "身体" },
            new() { Word = "ice", Definition = "冰", Example = "The ice is cold.", Difficulty = 1, Category = "自然" },
            new() { Word = "jump", Definition = "跳", Example = "I can jump.", Difficulty = 1, Category = "动作" },
            new() { Word = "king", Definition = "国王", Example = "The king is kind.", Difficulty = 1, Category = "人物" },
            new() { Word = "lion", Definition = "狮子", Example = "The lion is strong.", Difficulty = 1, Category = "动物" },
            new() { Word = "milk", Definition = "牛奶", Example = "I drink milk.", Difficulty = 1, Category = "食物" },
            new() { Word = "nose", Definition = "鼻子", Example = "I have a nose.", Difficulty = 1, Category = "身体" },
            new() { Word = "orange", Definition = "橙子", Example = "The orange is sweet.", Difficulty = 1, Category = "食物" },
            new() { Word = "pen", Definition = "钢笔", Example = "I write with a pen.", Difficulty = 1, Category = "物品" },
            new() { Word = "queen", Definition = "女王", Example = "The queen is beautiful.", Difficulty = 1, Category = "人物" },
            new() { Word = "run", Definition = "跑", Example = "I run fast.", Difficulty = 1, Category = "动作" },
            new() { Word = "sun", Definition = "太阳", Example = "The sun is bright.", Difficulty = 1, Category = "自然" },
            new() { Word = "tree", Definition = "树", Example = "The tree is tall.", Difficulty = 1, Category = "植物" },
            new() { Word = "water", Definition = "水", Example = "I drink water.", Difficulty = 1, Category = "食物" },
            new() { Word = "box", Definition = "盒子", Example = "The box is big.", Difficulty = 1, Category = "物品" },
            new() { Word = "yellow", Definition = "黄色", Example = "The banana is yellow.", Difficulty = 1, Category = "颜色" },
            new() { Word = "zoo", Definition = "动物园", Example = "We go to the zoo.", Difficulty = 1, Category = "地点" },
            new() { Word = "bird", Definition = "鸟", Example = "The bird can fly.", Difficulty = 1, Category = "动物" },
            new() { Word = "cake", Definition = "蛋糕", Example = "I like cake.", Difficulty = 1, Category = "食物" },
            new() { Word = "door", Definition = "门", Example = "Open the door.", Difficulty = 1, Category = "物品" },
            new() { Word = "eye", Definition = "眼睛", Example = "I have two eyes.", Difficulty = 1, Category = "身体" },
            new() { Word = "foot", Definition = "脚", Example = "I have two feet.", Difficulty = 1, Category = "身体" },
        };
    }
}
