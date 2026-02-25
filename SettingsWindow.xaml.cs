using System.Windows;
using TypingPractice.ViewModels;

namespace TypingPractice
{
    public partial class SettingsWindow : Window
    {
        private readonly MainViewModel _viewModel;
        
        public SettingsWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel.Settings;
        }
        
        private void OnSave(object sender, RoutedEventArgs e)
        {
            _viewModel.Settings.DailyGoal = int.Parse(DailyGoalTextBox.Text);
            _viewModel.UpdateSettings(_viewModel.Settings);
            MessageBox.Show("设置已保存");
            Close();
        }
        
        private void OnCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
