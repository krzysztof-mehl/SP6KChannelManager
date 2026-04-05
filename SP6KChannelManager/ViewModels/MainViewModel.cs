using Microsoft.VisualBasic;
using SP6KChannelManager.Commands;
using SP6KChannelManager.Helpers;
using SP6KChannelManager.Models;
using SP6KChannelManager.Services;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace SP6KChannelManager.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public Project CurrentProject { get; set => SetProperty(ref field, value); } = new();

        public static string WindowTitle
        {
            get
            {
                string product = AssemblyHelper.Product;
                string version = AssemblyHelper.InformationalVersion;
                return $"{product} v{version}";
            }
        }

        public string Status { get; set; } = "";

        public bool IsGroupSelected => CurrentProject.GroupCollection.SelectedGroup != null;
        public bool CanMoveUpGroup => CurrentProject.GroupCollection.SelectedGroup != null && CurrentProject.GroupCollection.Groups.IndexOf(CurrentProject.GroupCollection.SelectedGroup) > 0;
        public bool CanMoveDownGroup => CurrentProject.GroupCollection.SelectedGroup != null && CurrentProject.GroupCollection.Groups.IndexOf(CurrentProject.GroupCollection.SelectedGroup) < CurrentProject.GroupCollection.Groups.Count - 1;

        public int ChannelsCount => CurrentProject.GroupCollection.ChannelsCount;
        public bool IsChannelSelected => CurrentProject.GroupCollection.SelectedGroup?.SelectedChannel != null;
        public bool CanMoveUpChannel => CurrentProject.GroupCollection.SelectedGroup?.SelectedChannel != null && CurrentProject.GroupCollection.SelectedGroup.Channels.IndexOf(CurrentProject.GroupCollection.SelectedGroup.SelectedChannel) > 0;
        public bool CanMoveDownChannel => CurrentProject.GroupCollection.SelectedGroup?.SelectedChannel != null && CurrentProject.GroupCollection.SelectedGroup.Channels.IndexOf(CurrentProject.GroupCollection.SelectedGroup.SelectedChannel) < CurrentProject.GroupCollection.SelectedGroup.Channels.Count - 1;




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
            NewProjectCommand = new(NewProject);
            OpenProjectCommand = new(OpenProject);
            SaveProjectCommand = new(SaveProject);
            SaveAsProjectCommand = new(SaveAsProject);
            AddGroupCommand = new(AddGroup);
            EditGroupCommand = new(EditGroup, () => IsGroupSelected);
            RemoveGroupCommand = new(RemoveGroup, () => IsGroupSelected);
            CloneGroupCommand = new(CloneGroup, () => IsGroupSelected);
            SortGroupByNameCommand = new(SortGroupByName, () => IsGroupSelected);
            MoveUpGroupCommand = new(MoveUpGroup, () => CanMoveUpGroup);
            MoveDownGroupCommand = new(MoveDownGroup, () => CanMoveDownGroup);
            AddChannelCommand = new(AddChannel, () => IsGroupSelected);
            EditChannelCommand = new(EditChannel, () => IsChannelSelected);
            RemoveChannelCommand = new(RemoveChannel, () => IsChannelSelected);
            CloneChannelCommand = new(CloneChannel, () => IsChannelSelected);
            SortChannelByNameCommand = new(SortChannelByName, () => IsChannelSelected);
            SortChannelByFrequencyCommand = new(SortChannelByFrequency, () => IsChannelSelected);
            MoveUpChannelCommand = new(MoveUpChannel, () => CanMoveUpChannel);
            MoveDownChannelCommand = new(MoveDownChannel, () => CanMoveDownChannel);
            ShowAboutCommand = new(ShowAbout);
            SaveChannelChangesCommand = new(SaveChannelChanges);
            DiscardChannelChangesCommand = new(DiscardChannelChanges);
        }

        private void NewProject()
        {
            ErrorHandlerService.NotImplemented();
        }

        private void OpenProject()
        {
            ErrorHandlerService.NotImplemented();

            OnPropertyChanged(nameof(ChannelsCount));
        }

        private void SaveProject()
        {
            ErrorHandlerService.NotImplemented();
        }

        private void SaveAsProject()
        {
            ErrorHandlerService.NotImplemented();
        }

        private void AddGroup()
        {
            string name = Interaction.InputBox("Enter group name:", "Add Group", "New Group");

            if (GroupCollectionService.AddGroupService(CurrentProject, name))
            {
                CurrentProject.GroupCollection.SelectedGroup = CurrentProject.GroupCollection.Groups.Last();

                OnPropertyChanged(nameof(CanMoveUpGroup));
                OnPropertyChanged(nameof(CanMoveDownGroup));
            }
            else
            {
                ErrorHandlerService.ShowErrors(CurrentProject);
            }
        }

        private void EditGroup()
        {
            string name = Interaction.InputBox("Enter new group name:", "Edit Group", CurrentProject.GroupCollection.SelectedGroup!.Name);

            if (!GroupCollectionService.EditGroupService(CurrentProject,name))
            {
                ErrorHandlerService.ShowErrors(CurrentProject);
            }
        }

        private void RemoveGroup()
        {
            if (MessageBox.Show($"Are you sure you want to remove group '{CurrentProject.GroupCollection.SelectedGroup!.Name}'?", "Remove Group", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                GroupCollectionService.RemoveGroupService(CurrentProject);

                OnPropertyChanged(nameof(CanMoveUpGroup));
                OnPropertyChanged(nameof(CanMoveDownGroup));
                OnPropertyChanged(nameof(ChannelsCount));
            }
        }

        private void CloneGroup()
        {
            string name = Interaction.InputBox("Enter group name:", "Clone Group", $"{CurrentProject.GroupCollection.SelectedGroup!.Name} Copy");

            if (GroupCollectionService.CloneGroupService(CurrentProject, name))
            {
                OnPropertyChanged(nameof(CanMoveUpGroup));
                OnPropertyChanged(nameof(CanMoveDownGroup));
                OnPropertyChanged(nameof(ChannelsCount));
            }
            else
            {
                ErrorHandlerService.ShowErrors(CurrentProject);
            }
        }

        private void SortGroupByName()
        {
            ErrorHandlerService.NotImplemented();

            OnPropertyChanged(nameof(CanMoveUpGroup));
            OnPropertyChanged(nameof(CanMoveDownGroup));
        }

        private void MoveUpGroup()
        {
            GroupCollectionService.MoveUpGroupService(CurrentProject);

            OnPropertyChanged(nameof(CanMoveUpGroup));
            OnPropertyChanged(nameof(CanMoveDownGroup));
        }

        private void MoveDownGroup()
        {
            GroupCollectionService.MoveDownGroupService(CurrentProject);

            OnPropertyChanged(nameof(CanMoveUpGroup));
            OnPropertyChanged(nameof(CanMoveDownGroup));
        }

        private void AddChannel()
        {
            string name = Interaction.InputBox("Enter channel name:", "Add Channel", "New Channel");

            if (GroupCollectionService.AddChannelService(CurrentProject, name))
            {
                CurrentProject.GroupCollection.SelectedGroup!.SelectedChannel = CurrentProject.GroupCollection.SelectedGroup.Channels.Last();

                OnPropertyChanged(nameof(ChannelsCount));
            }
            else
            {
                ErrorHandlerService.ShowErrors(CurrentProject);
            }
        }

        private void EditChannel()
        {
            string name = Interaction.InputBox("Enter new channel name:", "Edit Channel", CurrentProject.GroupCollection.SelectedGroup!.SelectedChannel!.Name);

            if (!GroupCollectionService.EditChannelService(CurrentProject, name))
            {
                ErrorHandlerService.ShowErrors(CurrentProject);
            }
        }

        private void RemoveChannel()
        {
            if (MessageBox.Show($"Are you sure you want to remove channel '{CurrentProject.GroupCollection.SelectedGroup!.SelectedChannel!.Name}'?", "Remove Channel", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                GroupCollectionService.RemoveChannelService(CurrentProject);

                OnPropertyChanged(nameof(ChannelsCount));
            }
        }

        private void CloneChannel()
        {
            string name = Interaction.InputBox("Enter channel name:", "Clone Channel", $"{CurrentProject.GroupCollection.SelectedGroup!.SelectedChannel!.Name} Copy");

            if (GroupCollectionService.CloneChannelService(CurrentProject, name))
            {
                OnPropertyChanged(nameof(ChannelsCount));
            }
            else
            {
                ErrorHandlerService.ShowErrors(CurrentProject);
            }
        }

        private void SortChannelByName()
        {
            ErrorHandlerService.NotImplemented();
        }

        private void SortChannelByFrequency()
        {
            ErrorHandlerService.NotImplemented();
        }

        private void MoveUpChannel()
        {
            GroupCollectionService.MoveUpChannelService(CurrentProject);

            OnPropertyChanged(nameof(CanMoveUpChannel));
            OnPropertyChanged(nameof(CanMoveDownChannel));
        }

        private void MoveDownChannel()
        {
            GroupCollectionService.MoveDownChannelService(CurrentProject);

            OnPropertyChanged(nameof(CanMoveUpChannel));
            OnPropertyChanged(nameof(CanMoveDownChannel));
        }

        private void ShowAbout()
        {
            ErrorHandlerService.NotImplemented();
        }

        private void SaveChannelChanges()
        {
            ErrorHandlerService.NotImplemented();
        }

        private void DiscardChannelChanges()
        {
            ErrorHandlerService.NotImplemented();
        }
    }
}
