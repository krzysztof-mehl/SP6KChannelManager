using SP6KChannelManager.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace SP6KChannelManager.Views
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
        }
    }
}