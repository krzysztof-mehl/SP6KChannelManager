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

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                DataContext = new MainViewModel();
            }
        }
    }
}