using System.Windows;
using TypingPractice.Core;
using TypingPractice.ViewModels;

namespace TypingPractice
{
    public partial class App : Application
    {
        public static string BasePath { get; private set; } = string.Empty;
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            BasePath = AppDomain.CurrentDomain.BaseDirectory;
            
            // 初始化服务
            var dbService = new DatabaseService(BasePath);
            var vocabService = new VocabularyService(BasePath);
            var statsService = new StatisticsService();
            
            // 创建主 ViewModel
            var mainViewModel = new MainViewModel(dbService, vocabService, statsService);
            
            // 创建并显示主窗口
            var mainWindow = new MainWindow(mainViewModel, dbService, vocabService);
            mainWindow.Show();
        }
    }
}
