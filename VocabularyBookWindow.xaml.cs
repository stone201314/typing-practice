using System.Windows;
using TypingPractice.Core;
using TypingPractice.Models;

namespace TypingPractice
{
    public partial class VocabularyBookWindow : Window
    {
        private readonly User _user;
        private readonly DatabaseService _dbService;
        private readonly VocabularyService _vocabService;
        
        public VocabularyBookWindow(User user, DatabaseService dbService, VocabularyService vocabService)
        {
            InitializeComponent();
            _user = user;
            _dbService = dbService;
            _vocabService = vocabService;
            
            LoadVocabularyBook();
        }
        
        private void LoadVocabularyBook()
        {
            var items = _dbService.GetVocabularyBook(_user.Id);
            VocabularyList.ItemsSource = items;
            
            CountText.Text = $"共 {items.Count} 个生词";
        }
        
        private void OnDelete(object sender, RoutedEventArgs e)
        {
            if (VocabularyList.SelectedItem is VocabularyBookItem item)
            {
                var result = MessageBox.Show("确定要删除这个生词吗？", "确认", 
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                if (result == MessageBoxResult.Yes)
                {
                    _dbService.RemoveFromVocabularyBook(item.Id);
                    LoadVocabularyBook();
                }
            }
        }
        
        private void OnPractice(object sender, RoutedEventArgs e)
        {
            // TODO: 从生词本练习
            MessageBox.Show("功能开发中...");
        }
        
        private void OnClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
