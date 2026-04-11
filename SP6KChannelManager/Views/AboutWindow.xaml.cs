using SP6KChannelManager.ViewModels;
using System.Windows;

namespace SP6KChannelManager.Views
{
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
            DataContext = new AboutViewModel();
        }
    }
}
