using SP6KChannelManager.Commands;
using SP6KChannelManager.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

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
        public bool IsEditingChannel { get; set => SetProperty(ref field, value); } = false;
        public bool IsAddingChannel { get; set => SetProperty(ref field, value); } = false;
        public int ToneIndex { get; set => SetProperty(ref field, value); } = -1;
        public static List<string> Timeslots => ["TS1", "TS2"];
        public static List<int> Ccs => [0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15];
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
            NewProjectCommand = new(NewProject, () => !IsEditingChannel);
            OpenProjectCommand = new(OpenProject, () => !IsEditingChannel);
            SaveProjectCommand = new(SaveProject, () => !IsEditingChannel);
            SaveAsProjectCommand = new(SaveAsProject, () => !IsEditingChannel);

            AddGroupCommand = new(AddGroup, () => !IsEditingChannel);
            EditGroupCommand = new(EditGroup, () => !IsEditingChannel && IsGroupSelected);
            RemoveGroupCommand = new(RemoveGroup, () => !IsEditingChannel && IsGroupSelected);
            CloneGroupCommand = new(CloneGroup, () => !IsEditingChannel && IsGroupSelected);
            SortGroupByNameCommand = new(SortGroupByName, () => !IsEditingChannel && (Groups.Count > 1));
            MoveUpGroupCommand = new(MoveUpGroup, () => !IsEditingChannel && CanMoveUpGroup);
            MoveDownGroupCommand = new(MoveDownGroup, () => !IsEditingChannel && CanMoveDownGroup);

            AddChannelCommand = new(AddChannel, () => !IsEditingChannel && IsGroupSelected);
            EditChannelCommand = new(EditChannel, () => !IsEditingChannel && (SelectedGroup?.IsChannelSelected ?? false));
            RemoveChannelCommand = new(RemoveChannel, () => !IsEditingChannel && (SelectedGroup?.IsChannelSelected ?? false));
            CloneChannelCommand = new(CloneChannel, () => !IsEditingChannel && (SelectedGroup?.IsChannelSelected ?? false));
            SortChannelByNameCommand = new(SortChannelByName, () => !IsEditingChannel && (SelectedGroup?.Channels.Count > 1));
            SortChannelByFrequencyCommand = new(SortChannelByFrequency, () => !IsEditingChannel && (SelectedGroup?.Channels.Count > 1));
            MoveUpChannelCommand = new(MoveUpChannel, () => !IsEditingChannel && CanMoveUpChannel);
            MoveDownChannelCommand = new(MoveDownChannel, () => !IsEditingChannel && CanMoveDownChannel);

            ShowAboutCommand = new(ShowAbout);
            SaveChannelChangesCommand = new(SaveChannelChanges, () => IsEditingChannel);
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
            ErrorHandler.NotImplemented();
            Groups.Remove(SelectedGroup!);

            OnPropertyChanged(nameof(ChannelsCount));
        }

        private void CloneGroup()
        {
            ErrorHandler.NotImplemented();
        }

        private void SortGroupByName()
        {
            ErrorHandler.NotImplemented();
        }

        private void MoveUpGroup()
        {
            ErrorHandler.NotImplemented();
            int index = Groups.IndexOf(SelectedGroup!);

            Groups.Move(index, index - 1);
        }

        private void MoveDownGroup()
        {
            ErrorHandler.NotImplemented();
            int index = Groups.IndexOf(SelectedGroup!);

            Groups.Move(index, index + 1);
        }

        private void AddChannel()
        {
            ErrorHandler.NotImplemented();
            SelectedGroup?.Channels.Add(new Channel { Name = $"Channel {SelectedGroup.Channels.Count + 1}" });
            IsAddingChannel = true;
        }

        private void EditChannel()
        {
            ErrorHandler.NotImplemented();
            //SelectedGroup!.SelectedChannel = new(SelectedGroup.SelectedChannel!);
            IsEditingChannel = true;
        }

        private void RemoveChannel()
        {
            ErrorHandler.NotImplemented();
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

        private void CloneChannel()
        {
            ErrorHandler.NotImplemented();
        }

        private void SortChannelByName()
        {
            ErrorHandler.NotImplemented();
        }

        private void SortChannelByFrequency()
        {
            ErrorHandler.NotImplemented();
        }

        private void MoveUpChannel()
        {
            ErrorHandler.NotImplemented();
            int index = SelectedGroup!.Channels.IndexOf(SelectedGroup.SelectedChannel!);

            SelectedGroup.Channels.Move(index, index - 1);
        }

        private void MoveDownChannel()
        {
            ErrorHandler.NotImplemented();
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
        }

        private void DiscardChannelChanges()
        {
            ErrorHandler.NotImplemented();
        }
    }
}
