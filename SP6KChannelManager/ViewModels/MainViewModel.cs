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
        public List<string> ChannelStatuses { get; set => SetProperty(ref field, value); } = [];
        public static List<string> FmTones => ["None", "1750 Hz", "Tone", "TSQL"];
        public static List<string> DmrTimeslots => ["TS1", "TS2", "TS1 & TS2"];

        public ErrorHandler MainErrorHandler { get; set; } = new();

        public static string WindowTitle
        {
            get
            {
                string product = AssemblyHelper.Product;
                string version = AssemblyHelper.InformationalVersion;
                return $"{product} v{version}";
            }
        }
        public bool IsDataModified { get; set { if (!Equals(field, value)) { SetProperty(ref field, value); OnPropertyChanged(nameof(WindowTitle)); } } } = false;

        public Project CurrentProject { get; set => SetProperty(ref field, value); } = new();

        public Group? SelectedGroup { get; set { SetProperty(ref field, value); OnPropertyChanged(nameof(IsGroupSelected)); } } = null;
        public bool IsGroupSelected => SelectedGroup != null;
        public bool CanMoveUpGroup => IsGroupSelected && CurrentProject.Groups.IndexOf(SelectedGroup!) > 0;
        public bool CanMoveDownGroup => IsGroupSelected && CurrentProject.Groups.IndexOf(SelectedGroup!) < CurrentProject.Groups.Count - 1;

        
        public bool IsChannelSelected => SelectedGroup?.SelectedChannel != null;
        //public bool IsEditingChannel { get; set { SetProperty(ref field, value); OnPropertyChanged(nameof(IsAddingOrEditingChannel)); OnPropertyChanged(nameof(IsChannelDetailsVisible)); } } = false;
        //public bool IsAddingNewChannel { get; set { SetProperty(ref field, value); OnPropertyChanged(nameof(IsAddingOrEditingChannel)); OnPropertyChanged(nameof(IsChannelDetailsVisible)); } } = false;
        //public bool IsAddingOrEditingChannel => IsEditingChannel || IsAddingNewChannel;
        //public bool IsChannelDetailsVisible  => IsAddingOrEditingChannel || IsChannelSelected;
        public int ChannelsCount => CurrentProject.Groups.Sum(group => group.Channels.Count);

        public MainViewModel()
        {
            InitializeCommands();

            ChannelStatuses = CurrentProject.ChannelStatuses;
        }
    }
}
