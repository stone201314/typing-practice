# 青少年打字练习软件

帮助 6-15 岁青少年熟悉键盘使用，同时提升词汇量的 Windows 桌面应用。

## 功能特点

- **双模式练习**：拼音模式 + 英语模式
- **分级词库**：1-9 年级，约 600 个词汇
- **释义展示**：打完词语后显示释义和例句
- **生词本**：收藏不熟悉的词汇，定期复习
- **学习统计**：日/周/月报告，进度可视化
- **本地存储**：无需联网，数据安全

## 快速开始

### 环境要求

- Windows 10/11
- .NET 8.0 Runtime（[下载地址](https://dotnet.microsoft.com/download/dotnet/8.0)）

### 编译方法

详见 [BUILD.md](BUILD.md)

1. 安装 Visual Studio 2022
2. 打开 `TypingPractice.csproj`
3. 编译运行

## 项目结构

```
TypingPractice/
├── TypingPractice.csproj      # 项目配置
├── App.xaml                   # 应用定义
├── MainWindow.xaml            # 主窗口
├── PracticeWindow.xaml        # 练习窗口
├── VocabularyBookWindow.xaml  # 生词本
├── ReportWindow.xaml          # 学习报告
├── SettingsWindow.xaml        # 设置
├── Core/                      # 核心逻辑
│   ├── TypingEngine.cs
│   ├── VocabularyService.cs
│   ├── StatisticsService.cs
│   └── DatabaseService.cs
├── Models/                    # 数据模型
├── ViewModels/                # 视图模型
├── Converters/                # 值转换器
└── Data/Vocabulary/           # 词库文件
    ├── pinyin/grade_1-9.json
    └── english/grade_1-9.json
```

## 词库说明

### 词库格式

```json
{
  "word": "yi",
  "display": "一",
  "pinyin": "yī",
  "definition": "数字，表示最小的正整数",
  "example": "我有一本书。",
  "difficulty": 1,
  "category": "数字"
}
```

### 扩展词库

1. 创建新的 JSON 文件
2. 按格式添加词汇
3. 放入 `Data/Vocabulary/pinyin/` 或 `english/` 目录

## 技术栈

- **框架**: WPF (.NET 8)
- **数据库**: SQLite
- **架构**: MVVM
- **依赖库**:
  - CommunityToolkit.Mvvm
  - Microsoft.Data.Sqlite
  - Newtonsoft.Json

## 许可证

MIT License
