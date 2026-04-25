using Microsoft.VisualBasic;
using Microsoft.Win32;
using SP6KChannelManager.Commands;
using SP6KChannelManager.Helpers;
using SP6KChannelManager.Models;
using SP6KChannelManager.Views;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace SP6KChannelManager.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        public static List<string> ChannelStatuses => ["None", "1750 Hz", "Tone", "TSQL"];
        public static List<string> FmTones => ["None", "1750 Hz", "Tone", "TSQL"];
        public static List<string> DmrTimeslots => ["TS1", "TS2", "TS1 & TS2"];

        public static string WindowTitle
        {
            get
            {
                string product = AssemblyHelper.Product;
                string version = AssemblyHelper.InformationalVersion;
                return $"{product} v{version}";
            }
        }

        public bool IsGroupSelected { get; private set => SetProperty(ref field, value); } = false;
        public bool IsChannelSelected { get; private set => SetProperty(ref field, value); } = false;
        public bool IsEditingChannel { get; private set { SetProperty(ref field, value); OnPropertyChanged(nameof(IsAddingOrEditingChannel)); } } = false;
        public bool IsAddingNewChannel { get; private set { SetProperty(ref field, value); OnPropertyChanged(nameof(IsAddingOrEditingChannel)); } } = false;
        public bool IsAddingOrEditingChannel => IsEditingChannel || IsAddingNewChannel;

        public MainViewModel()
        {
            InitializeCommands();
        }
    }
}
