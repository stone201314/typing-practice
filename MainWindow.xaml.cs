using System.Windows;
using TypingPractice.Core;
using TypingPractice.ViewModels;

namespace TypingPractice
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        private readonly DatabaseService _dbService;
        private readonly VocabularyService _vocabService;
        
        public MainWindow(MainViewModel viewModel, DatabaseService dbService, VocabularyService vocabService)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _dbService = dbService;
            _vocabService = vocabService;
            DataContext = _viewModel;
        }
        
        private void OnUserSelected(object sender, RoutedEventArgs e)
        {
            if (UsersList.SelectedItem is Models.User user)
            {
                _viewModel.SelectUserCommand.Execute(user);
            }
        }
        
        private void OnCreateUser(object sender, RoutedEventArgs e)
        {
            var nickname = NewUserNickname.Text.Trim();
            if (!string.IsNullOrEmpty(nickname))
            {
                _viewModel.CreateUserCommand.Execute(nickname);
                NewUserNickname.Text = string.Empty;
            }
        }
        
        private void OnStartPractice(object sender, RoutedEventArgs e)
        {
            var practiceWindow = new PracticeWindow(_viewModel.CurrentUser!, _viewModel.CurrentMode, 
                _viewModel.Settings, _dbService, _vocabService);
            practiceWindow.Owner = this;
            practiceWindow.ShowDialog();
            
            _viewModel.RefreshProgress();
        }
        
        private void OnOpenVocabularyBook(object sender, RoutedEventArgs e)
        {
            var vocabWindow = new VocabularyBookWindow(_viewModel.CurrentUser!, _dbService, _vocabService);
            vocabWindow.Owner = this;
            vocabWindow.ShowDialog();
        }
        
        private void OnOpenReport(object sender, RoutedEventArgs e)
        {
            var reportWindow = new ReportWindow(_viewModel.CurrentUser!, _dbService);
            reportWindow.Owner = this;
            reportWindow.ShowDialog();
        }
        
        private void OnOpenSettings(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow(_viewModel);
            settingsWindow.Owner = this;
            settingsWindow.ShowDialog();
        }
        
        private void OnLogout(object sender, RoutedEventArgs e)
        {
            _viewModel.LogoutCommand.Execute(null);
        }
        
        private void OnSwitchMode(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.RadioButton radio && radio.Content is string mode)
            {
                _viewModel.SwitchModeCommand.Execute(mode.ToLower());
            }
        }
    }
}
