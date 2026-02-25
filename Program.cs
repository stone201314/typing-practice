using System;
using System.Windows;

namespace TypingPractice
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("正在启动青少年打字练习软件...");
                Console.WriteLine(".NET 版本: " + Environment.Version);
                Console.WriteLine("操作系统: " + Environment.OSVersion);
                
                var app = new App();
                app.InitializeComponent();
                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine("========== 错误 ==========");
                Console.WriteLine(ex.Message);
                Console.WriteLine("");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine("");
                Console.WriteLine("按任意键退出...");
                Console.ReadKey();
            }
        }
    }
}
