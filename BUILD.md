# 青少年打字练习软件 - 编译说明

## 项目结构

```
TypingPractice/
├── TypingPractice.csproj      # 项目配置文件
├── App.xaml                   # 应用程序定义
├── App.xaml.cs               # 应用程序入口
├── MainWindow.xaml           # 主窗口界面
├── MainWindow.xaml.cs        # 主窗口逻辑
├── PracticeWindow.xaml       # 练习窗口界面
├── PracticeWindow.xaml.cs    # 练习窗口逻辑
├── VocabularyBookWindow.xaml # 生词本窗口
├── VocabularyBookWindow.xaml.cs
├── ReportWindow.xaml         # 学习报告窗口
├── ReportWindow.xaml.cs
├── SettingsWindow.xaml       # 设置窗口
├── SettingsWindow.xaml.cs
├── Models/
│   └── Models.cs             # 数据模型
├── Core/
│   ├── TypingEngine.cs       # 打字引擎
│   ├── VocabularyService.cs  # 词汇服务
│   ├── StatisticsService.cs  # 统计服务
│   └── DatabaseService.cs    # 数据库服务
├── ViewModels/
│   ├── MainViewModel.cs      # 主视图模型
│   └── PracticeViewModel.cs  # 练习视图模型
├── Converters/
│   └── Converters.cs         # 值转换器
└── Data/
    └── Vocabulary/
        ├── pinyin/           # 拼音词库
        └── english/          # 英语词库
```

## 编译环境要求

- **操作系统**: Windows 10/11
- **开发工具**: Visual Studio 2022
- **.NET 版本**: .NET 8.0 SDK
- **工作负载**: .NET 桌面开发

## 编译步骤

### 方法一：使用 Visual Studio

1. 安装 Visual Studio 2022
   - 下载地址：https://visualstudio.microsoft.com/
   - 安装时选择 ".NET 桌面开发" 工作负载

2. 打开项目
   - 启动 Visual Studio 2022
   - 选择 "打开本地文件夹"
   - 选择 `TypingPractice` 文件夹

3. 还原 NuGet 包
   - 右键点击解决方案
   - 选择 "还原 NuGet 包"

4. 编译项目
   - 按 `Ctrl+Shift+B` 或选择 "生成" → "生成解决方案"

5. 运行项目
   - 按 `F5` 运行调试
   - 或按 `Ctrl+F5` 运行（不调试）

### 方法二：使用命令行

1. 安装 .NET 8.0 SDK
   - 下载地址：https://dotnet.microsoft.com/download/dotnet/8.0

2. 打开命令提示符，进入项目目录

3. 还原依赖
   ```bash
   dotnet restore
   ```

4. 编译项目
   ```bash
   dotnet build --configuration Release
   ```

5. 运行项目
   ```bash
   dotnet run --configuration Release
   ```

## 打包发布

### 创建发布版本

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

参数说明：
- `-c Release`: 使用 Release 配置
- `-r win-x64`: 目标平台为 Windows x64
- `--self-contained true`: 包含 .NET 运行时
- `-p:PublishSingleFile=true`: 打包为单个可执行文件

### 输出位置

发布后的文件位于：
```
bin/Release/net8.0-windows/win-x64/publish/
```

### 创建安装包

可以使用以下工具创建安装包：

1. **Inno Setup** (推荐)
   - 下载：https://jrsoftware.org/isdl.php
   - 创建安装脚本，打包 publish 文件夹

2. **NSIS**
   - 下载：https://nsis.sourceforge.io/
   - 创建安装脚本

3. **Advanced Installer**
   - 下载：https://www.advancedinstaller.com/
   - 图形化界面，易于使用

## 词库说明

词库文件位于 `Data/Vocabulary/` 目录：
- `pinyin/grade_1.json` ~ `grade_9.json`: 拼音词库（1-9年级）
- `english/grade_1.json` ~ `grade_9.json`: 英语词库（1-9年级）

### 词库格式

```json
[
  {
    "word": "yi",
    "display": "一",
    "pinyin": "yī",
    "definition": "数字，表示最小的正整数",
    "example": "我有一本书。",
    "difficulty": 1,
    "category": "数字"
  }
]
```

### 添加新词库

1. 创建新的 JSON 文件，如 `grade_10.json`
2. 按照上述格式添加词汇
3. 将文件放入对应的 `pinyin` 或 `english` 目录
4. 重启应用程序即可使用新词库

## 常见问题

### Q: 编译时提示找不到 NuGet 包
A: 确保网络连接正常，然后执行 `dotnet restore`

### Q: 运行时提示找不到词库文件
A: 确保词库 JSON 文件的 "复制到输出目录" 属性设置为 "如果较新则复制"

### Q: 如何修改界面样式
A: 编辑 `App.xaml` 中的样式资源，或修改各窗口的 XAML 文件

### Q: 如何添加新功能
A: 参考 MVVM 模式，在 ViewModels 中添加逻辑，在 Views 中添加界面

## 技术栈

- **框架**: WPF (.NET 8)
- **语言**: C# + XAML
- **数据库**: SQLite (Microsoft.Data.Sqlite)
- **MVVM 框架**: CommunityToolkit.Mvvm
- **JSON 解析**: Newtonsoft.Json
- **图表**: LiveChartsCore.SkiaSharpView.WPF (可选)

## 许可证

本项目仅供学习和个人使用。
