using SP6KChannelManager.Commands;
using SP6KChannelManager.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace SP6KChannelManager.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        internal ErrorHandler ErrorHandler { get; set; } = new();

        public string WindowTitle { get; set => SetProperty(ref field, value); } = "";
        public int WindowHeight { get; set => SetProperty(ref field, value); } = 600;
        public int WindowWidth { get; set => SetProperty(ref field, value); } = 1100;
        public string Status { get; set => SetProperty(ref field, value); } = "Not initialized";

        public ObservableCollection<Group> Groups { get; set => SetProperty(ref field, value); } = [];
        public Group? SelectedGroup { get; set { IsGroupSelected = value != null; SetProperty(ref field, value); } } = null;
        public bool IsGroupSelected { get; private set => SetProperty(ref field, value); } = false;
        public bool CanMoveUpGroup => IsGroupSelected && Groups.IndexOf(SelectedGroup!) > 0;
        public bool CanMoveDownGroup => IsGroupSelected && Groups.IndexOf(SelectedGroup!) < Groups.Count - 1;
        public int ChannelsCount => Groups.Sum(group => group.Channels.Count);
        public bool CanMoveUpChannel => SelectedGroup?.IsChannelSelected == true && SelectedGroup.Channels.IndexOf(SelectedGroup.SelectedChannel!) > 0;
        public bool CanMoveDownChannel => SelectedGroup?.IsChannelSelected == true && SelectedGroup.Channels.IndexOf(SelectedGroup.SelectedChannel!) < SelectedGroup.Channels.Count - 1;
        public bool IsAddingOrEditingChannel { get; set => SetProperty(ref field, value); } = false;
        public bool IsAddingChannel { get; set => SetProperty(ref field, value); } = false;
        public int ToneIndex { get; set => SetProperty(ref field, value); } = -1;

        public static List<string> Timeslots => ["TS1", "TS2"];
        public static List<int> Ccs => [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
        public static List<string> Bandwidths => ["Narrow", "Wide"];
        public static List<string> Tones => ["Off", "Tone", "TSQL"];
        public static List<decimal> CtcssTones => [67.0m, 69.3m, 71.9m, 74.4m, 77.0m, 79.7m, 82.5m, 85.4m, 88.5m, 91.5m, 94.8m, 97.4m, 100.0m, 103.5m, 107.2m, 110.9m, 114.8m, 118.8m, 123.0m, 127.3m, 131.8m, 136.5m, 141.3m, 146.2m, 151.4m, 156.7m, 159.8m, 162.2m, 165.5m, 167.9m, 171.3m, 173.8m, 177.3m, 179.9m, 183.5m, 186.2m, 189.9m, 192.8m, 196.6m, 199.5m, 203.5m, 206.5m, 210.7m, 218.1m, 225.7m, 229.1m, 233.6m, 241.8m, 250.3m, 254.1m];

        public RelayCommand NewProjectCommand { get; }
        public RelayCommand OpenProjectCommand { get; }
        public RelayCommand SaveProjectCommand { get; }
        public RelayCommand SaveAsProjectCommand { get; }
        public RelayCommand AddGroupCommand { get; }
        public RelayCommand EditGroupCommand { get; }
        public RelayCommand RemoveGroupCommand { get; }
        public RelayCommand CloneGroupCommand { get; }
        public RelayCommand SortGroupByNameCommand { get; }
        public RelayCommand MoveUpGroupCommand { get; }
        public RelayCommand MoveDownGroupCommand { get; }
        public RelayCommand AddChannelCommand { get; }
        public RelayCommand EditChannelCommand { get; }
        public RelayCommand RemoveChannelCommand { get; }
        public RelayCommand CloneChannelCommand { get; }
        public RelayCommand SortChannelByNameCommand { get; }
        public RelayCommand SortChannelByFrequencyCommand { get; }
        public RelayCommand MoveUpChannelCommand { get; }
        public RelayCommand MoveDownChannelCommand { get; }
        public RelayCommand ShowAboutCommand { get; }
        public RelayCommand SaveChannelChangesCommand { get; }
        public RelayCommand DiscardChannelChangesCommand { get; }

        public MainViewModel()
        {
            NewProjectCommand = new(NewProject, () => !IsAddingOrEditingChannel);
            OpenProjectCommand = new(OpenProject, () => !IsAddingOrEditingChannel);
            SaveProjectCommand = new(SaveProject, () => !IsAddingOrEditingChannel);
            SaveAsProjectCommand = new(SaveAsProject, () => !IsAddingOrEditingChannel);

            AddGroupCommand = new(AddGroup, () => !IsAddingOrEditingChannel);
            EditGroupCommand = new(EditGroup, () => !IsAddingOrEditingChannel && IsGroupSelected);
            RemoveGroupCommand = new(RemoveGroup, () => !IsAddingOrEditingChannel && IsGroupSelected);
            CloneGroupCommand = new(CloneGroup, () => !IsAddingOrEditingChannel && IsGroupSelected);
            SortGroupByNameCommand = new(SortGroupByName, () => !IsAddingOrEditingChannel && (Groups.Count > 1));
            MoveUpGroupCommand = new(MoveUpGroup, () => !IsAddingOrEditingChannel && CanMoveUpGroup);
            MoveDownGroupCommand = new(MoveDownGroup, () => !IsAddingOrEditingChannel && CanMoveDownGroup);

            AddChannelCommand = new(AddChannel, () => !IsAddingOrEditingChannel && IsGroupSelected);
            EditChannelCommand = new(EditChannel, () => !IsAddingOrEditingChannel && (SelectedGroup?.IsChannelSelected ?? false));
            RemoveChannelCommand = new(RemoveChannel, () => !IsAddingOrEditingChannel && (SelectedGroup?.IsChannelSelected ?? false));
            CloneChannelCommand = new(CloneChannel, () => !IsAddingOrEditingChannel && (SelectedGroup?.IsChannelSelected ?? false));
            SortChannelByNameCommand = new(SortChannelByName, () => !IsAddingOrEditingChannel && (SelectedGroup?.Channels.Count > 1));
            SortChannelByFrequencyCommand = new(SortChannelByFrequency, () => !IsAddingOrEditingChannel && (SelectedGroup?.Channels.Count > 1));
            MoveUpChannelCommand = new(MoveUpChannel, () => !IsAddingOrEditingChannel && CanMoveUpChannel);
            MoveDownChannelCommand = new(MoveDownChannel, () => !IsAddingOrEditingChannel && CanMoveDownChannel);

            ShowAboutCommand = new(ShowAbout);
            SaveChannelChangesCommand = new(SaveChannelChanges, () => IsAddingOrEditingChannel);
            DiscardChannelChangesCommand = new(DiscardChannelChanges);
        }


        private void NewProject()
        {
            ErrorHandler.NotImplemented();
        }

        private void OpenProject()
        {
            ErrorHandler.NotImplemented();
        }

        private void SaveProject()
        {
            ErrorHandler.NotImplemented();
        }

        private void SaveAsProject()
        {
            ErrorHandler.NotImplemented();
        }

        private void AddGroup()
        {
            ErrorHandler.NotImplemented();
            Groups.Add(new Group { Name = $"Group {Groups.Count + 1}" });
            SelectedGroup = Groups.Last();
        }

        private void EditGroup()
        {
            ErrorHandler.NotImplemented();
        }

        private void RemoveGroup()
        {
            if (MessageBox.Show($"Are you sure you want to remove the group '{SelectedGroup!.Name}'?", "Confirm Group Removal", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Groups.Remove(SelectedGroup!);
                OnPropertyChanged(nameof(ChannelsCount));
            }
        }

        private void CloneGroup()
        {
            ErrorHandler.NotImplemented();
        }

        private void SortGroupByName()
        {
            if (MessageBox.Show("Are you sure you want to sort the groups by name? This action cannot be undone.", "Confirm Group Sorting", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var sortedGroups = Groups.OrderBy(group => group.Name).ToList();
                Groups.Clear();
                foreach (var group in sortedGroups)
                {
                    Groups.Add(group);
                }
            }
        }

        private void MoveUpGroup()
        {
            int index = Groups.IndexOf(SelectedGroup!);
            Groups.Move(index, index - 1);
        }

        private void MoveDownGroup()
        {
            int index = Groups.IndexOf(SelectedGroup!);
            Groups.Move(index, index + 1);
        }

        private void AddChannel()
        {
            SelectedGroup!.ChannelDetails = new();
            IsAddingChannel = true;
            IsAddingOrEditingChannel = true;
        }

        private void EditChannel()
        {
            SelectedGroup!.ChannelDetails = new(SelectedGroup.SelectedChannel!);
            IsAddingOrEditingChannel = true;
        }

        private void RemoveChannel()
        {
            if (MessageBox.Show($"Are you sure you want to remove the channel '{SelectedGroup!.SelectedChannel!.Name}'?", "Confirm Channel Removal", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                int index = SelectedGroup!.Channels.IndexOf(SelectedGroup.SelectedChannel!);
                SelectedGroup!.Channels.Remove(SelectedGroup.SelectedChannel!);
                if (SelectedGroup.Channels.Count > 0)
                {
                    if (index == SelectedGroup.Channels.Count)
                    {
                        SelectedGroup!.SelectedChannel = SelectedGroup.Channels.Last();
                    }
                    else
                    {
                        SelectedGroup!.SelectedChannel = SelectedGroup.Channels[index];
                    }
                }
                OnPropertyChanged(nameof(ChannelsCount));
            }
        }

        private void CloneChannel()
        {
            ErrorHandler.NotImplemented();
        }

        private void SortChannelByName()
        {
            if (MessageBox.Show("Are you sure you want to sort the channels by name? This action cannot be undone.", "Confirm Channel Sorting", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var sortedChannels = SelectedGroup!.Channels.OrderBy(channel => channel.Name).ToList();
                SelectedGroup.Channels.Clear();
                foreach (var channel in sortedChannels)
                {
                    SelectedGroup.Channels.Add(channel);
                }
            }
        }

        private void SortChannelByFrequency()
        {
            if (MessageBox.Show("Are you sure you want to sort the channels by frequency? This action cannot be undone.", "Confirm Channel Sorting", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var sortedChannels = SelectedGroup!.Channels.OrderBy(channel => channel.Frequency).ToList();
                SelectedGroup.Channels.Clear();
                foreach (var channel in sortedChannels)
                {
                    SelectedGroup.Channels.Add(channel);
                }
            }
        }

        private void MoveUpChannel()
        {
            int index = SelectedGroup!.Channels.IndexOf(SelectedGroup.SelectedChannel!);
            SelectedGroup.Channels.Move(index, index - 1);
        }

        private void MoveDownChannel()
        {
            int index = SelectedGroup!.Channels.IndexOf(SelectedGroup.SelectedChannel!);
            SelectedGroup.Channels.Move(index, index + 1);
        }

        private void ShowAbout()
        {
            ErrorHandler.NotImplemented();
        }

        private void SaveChannelChanges()
        {
            ErrorHandler.NotImplemented();
            if (IsAddingChannel)
            {
                SelectedGroup!.Channels.Add(new(SelectedGroup.ChannelDetails!));
                OnPropertyChanged(nameof(ChannelsCount));
            }
            else
            {
                if(MessageBox.Show($"Are you sure you want to save the changes to the channel '{SelectedGroup!.SelectedChannel!.Name}'?", "Confirm Channel Changes", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int index = SelectedGroup!.Channels.IndexOf(SelectedGroup.SelectedChannel!);
                    SelectedGroup.Channels[index] = new(SelectedGroup.ChannelDetails!);
                }
            }
            IsAddingChannel = false;
            IsAddingOrEditingChannel = false;
            SelectedGroup!.ChannelDetails = null;
        }

        private void DiscardChannelChanges()
        {
            ErrorHandler.NotImplemented();
            IsAddingChannel = false;
            IsAddingOrEditingChannel = false;
            SelectedGroup!.ChannelDetails = null;
        }
    }
}
