using System.Windows;
using TypingPractice.Core;
using TypingPractice.ViewModels;

namespace TypingPractice
{
    public partial class App : Application
    {
        public static string BasePath { get; private set; } = string.Empty;
        
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            BasePath = AppDomain.CurrentDomain.BaseDirectory;
            
            try
            {
                // 初始化服务
                var dbService = new DatabaseService(BasePath);
                var vocabService = new VocabularyService();
                var statsService = new StatisticsService();
                
                // 创建主 ViewModel
                var mainViewModel = new MainViewModel(dbService, vocabService, statsService);
                
                // 创建并显示主窗口
                var mainWindow = new MainWindow(mainViewModel, dbService, vocabService);
                mainWindow.Show();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"启动失败: {ex.Message}\n\n{ex.StackTrace}", "错误", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }
    }
}
