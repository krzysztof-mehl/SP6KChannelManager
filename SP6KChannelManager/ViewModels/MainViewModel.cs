using SP6KChannelManager.Commands;
using SP6KChannelManager.Models;
using System.Collections.ObjectModel;

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
            SortGroupByNameCommand = new(SortGroupByName, () => !IsEditingChannel);
            MoveUpGroupCommand = new(MoveUpGroup, () => !IsEditingChannel && CanMoveUpGroup);
            MoveDownGroupCommand = new(MoveDownGroup, () => !IsEditingChannel && CanMoveDownGroup);
            AddChannelCommand = new(AddChannel, () => !IsEditingChannel && IsGroupSelected);
            EditChannelCommand = new(EditChannel, () => !IsEditingChannel && (SelectedGroup?.IsChannelSelected ?? false));
            RemoveChannelCommand = new(RemoveChannel, () => !IsEditingChannel && (SelectedGroup?.IsChannelSelected ?? false));
            CloneChannelCommand = new(CloneChannel, () => !IsEditingChannel && (SelectedGroup?.IsChannelSelected ?? false));
            SortChannelByNameCommand = new(SortChannelByName, () => !IsEditingChannel);
            SortChannelByFrequencyCommand = new(SortChannelByFrequency, () => !IsEditingChannel);
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

            OnPropertyChanged(nameof(ChannelsCount));
        }

        private void EditChannel()
        {
            ErrorHandler.NotImplemented();
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
            IsEditingChannel = false;
        }

        private void DiscardChannelChanges()
        {
            ErrorHandler.NotImplemented();
            SelectedGroup!.SelectedChannel = null;
            IsEditingChannel = false;
        }
    }
}
