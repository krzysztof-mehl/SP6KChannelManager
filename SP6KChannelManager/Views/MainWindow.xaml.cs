using SP6KChannelManager.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace SP6KChannelManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
            DataContext = new MainViewModel();
        }

        private void MainWindow_Closing(object? sender, CancelEventArgs e)
        {
            if (DataContext is MainViewModel { IsDataModified: true } &&
                MessageBox.Show(
                    "You have unsaved changes.\nAre you sure you want to close the application?",
                    "Confirm closing",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}