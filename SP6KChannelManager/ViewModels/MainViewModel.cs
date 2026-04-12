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
    public class MainViewModel : BaseViewModel
    {
        public static List<string> Timeslots => ["TS1", "TS2"];
        public static List<string> Bandwidths => ["Narrow", "Wide"];
        public static List<string> Tones => ["Off", "Tone", "TSQL"];
        public static List<decimal> CtcssTones => [67.0m, 69.3m, 71.9m, 74.4m, 77.0m, 79.7m, 82.5m, 85.4m, 88.5m, 91.5m, 94.8m, 97.4m, 100.0m, 103.5m, 107.2m, 110.9m, 114.8m, 118.8m, 123.0m, 127.3m, 131.8m, 136.5m, 141.3m, 146.2m, 151.4m, 156.7m, 159.8m, 162.2m, 165.5m, 167.9m, 171.3m, 173.8m, 177.3m, 179.9m, 183.5m, 186.2m, 189.9m, 192.8m, 196.6m, 199.5m, 203.5m, 206.5m, 210.7m, 218.1m, 225.7m, 229.1m, 233.6m, 241.8m, 250.3m, 254.1m];
        public static List<int> ColorCodes => [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];

        public ErrorHandler ErrorHandler { get; set; } = new();

        public Project CurrentProject { get; set => SetProperty(ref field, value); } = new();

        public string WindowTitle
        {
            get
            {
                string product = AssemblyHelper.Product;
                string version = AssemblyHelper.InformationalVersion;
                string fileName = CurrentProject.FilePath == "" ? "unsaved" : Path.GetFileNameWithoutExtension(CurrentProject.FilePath);
                string modifiedIndicator = IsDataModified ? "*" : "";
                return $"{product} v{version} - {fileName}{modifiedIndicator}";
            }
        }
        public string Status { get; set => SetProperty(ref field, value); } = "Not initialized";


        public Group? SelectedGroup { get; set { IsGroupSelected = value != null; SetProperty(ref field, value); } } = null;
        public bool IsGroupSelected { get; private set => SetProperty(ref field, value); } = false;
        public bool CanMoveUpGroup => IsGroupSelected && CurrentProject.Groups.IndexOf(SelectedGroup!) > 0;
        public bool CanMoveDownGroup => IsGroupSelected && CurrentProject.Groups.IndexOf(SelectedGroup!) < CurrentProject.Groups.Count - 1;
        public int ChannelsCount => CurrentProject.Groups.Sum(group => group.Channels.Count);
        public bool CanMoveUpChannel => SelectedGroup?.IsChannelSelected == true && SelectedGroup.Channels.IndexOf(SelectedGroup.SelectedChannel!) > 0;
        public bool CanMoveDownChannel => SelectedGroup?.IsChannelSelected == true && SelectedGroup.Channels.IndexOf(SelectedGroup.SelectedChannel!) < SelectedGroup.Channels.Count - 1;
        public bool IsAddingOrEditingChannel { get; set => SetProperty(ref field, value); } = false;
        public bool IsAddingChannel { get; set => SetProperty(ref field, value); } = false;
        public int ToneIndex { get; set => SetProperty(ref field, value); } = -1;
        public bool IsDataModified { get; set { if (!Equals(field, value)) { SetProperty(ref field, value); OnPropertyChanged(nameof(WindowTitle)); } } } = false;
        public Group? SelectedTargetGroup { get; set; } = null;

        public RelayCommand NewProjectCommand { get; }
        public RelayCommand OpenProjectCommand { get; }
        public RelayCommand SaveProjectCommand { get; }
        public RelayCommand SaveAsProjectCommand { get; }
        public RelayCommand ExitCommand { get; }
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
            SaveProjectCommand = new(SaveProject, () => !IsAddingOrEditingChannel && CurrentProject.FilePath != "");
            SaveAsProjectCommand = new(SaveAsProject, () => !IsAddingOrEditingChannel);
            ExitCommand = new(new Action(() => Application.Current.Windows.OfType<Window>().FirstOrDefault(window => window.IsActive)?.Close()));

            AddGroupCommand = new(AddGroup, () => !IsAddingOrEditingChannel);
            EditGroupCommand = new(EditGroup, () => !IsAddingOrEditingChannel && IsGroupSelected);
            RemoveGroupCommand = new(RemoveGroup, () => !IsAddingOrEditingChannel && IsGroupSelected);
            CloneGroupCommand = new(CloneGroup, () => !IsAddingOrEditingChannel && IsGroupSelected);
            SortGroupByNameCommand = new(SortGroupByName, () => !IsAddingOrEditingChannel && (CurrentProject.Groups.Count > 1));
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

            Status = "Ready";
        }

        private void NewProject()
        {
            if (IsDataModified && MessageBox.Show("Are you sure you want to create a new project?\nAny unsaved changes will be lost.", "Confirm New Project", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }
            CurrentProject = new();
            IsDataModified = false;
            OnPropertyChanged(nameof(ChannelsCount));
        }

        private void OpenProject()
        {
            if (IsDataModified && MessageBox.Show("Are you sure you want to open a project?\nAny unsaved changes will be lost.", "Confirm Open Project", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }

            OpenFileDialog openFileDialog = new()
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",

            };
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string projectJson = File.ReadAllText(openFileDialog.FileName);
                    Project project = JsonSerializer.Deserialize<Project>(projectJson) ?? new();
                    if (project.FilePath != "")
                    {
                        if (project.Version != AssemblyHelper.Version)
                        {
                            if (MessageBox.Show($"The project was created with version {project.Version} of the application, while you are using version {AssemblyHelper.Version}.\nOpening the project may cause compatibility issues or data loss.\nDo you want to proceed?", "Project Version Mismatch", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                            {
                                return;
                            }
                            project.Version = AssemblyHelper.Version;
                        }
                        project.FilePath = openFileDialog.FileName;
                        CurrentProject = project;
                        IsDataModified = false;
                        OnPropertyChanged(nameof(ChannelsCount));
                        OnPropertyChanged(nameof(WindowTitle));
                    }
                    else
                    {
                        MessageBox.Show("The selected file does not contain a valid project.\nPlease select a valid project file.", "Invalid Project File", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while opening the project:\n{ex.Message}", "Open Project Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SaveProject()
        {
            if (CurrentProject.RequireProjectSaveConfirmation && MessageBox.Show("Are you sure you want to save the project?", "Confirm Save Project", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    string projectJson = JsonSerializer.Serialize(CurrentProject);
                    File.WriteAllText(CurrentProject.FilePath, projectJson);
                    IsDataModified = false;
                    MessageBox.Show("Project saved successfully.", "Save Project", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while saving the project:\n{ex.Message}", "Save Project Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SaveAsProject()
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                OverwritePrompt = true
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                CurrentProject.FilePath = saveFileDialog.FileName;
                bool confirmSave = CurrentProject.RequireProjectSaveConfirmation;
                CurrentProject.RequireProjectSaveConfirmation = false;
                SaveProject();
                CurrentProject.RequireProjectSaveConfirmation = confirmSave;
            }
        }

        private void AddGroup()
        {
            string name = Interaction.InputBox("Enter the name of the new group:", "Add Group");
            if (name != "")
            {
                if (Group.ValidateName(ErrorHandler, CurrentProject, name))
                {
                    CurrentProject.Groups.Add(new Group { Name = name });
                    SelectedGroup = CurrentProject.Groups.Last();
                    IsDataModified = true;
                }
                else
                {
                    ErrorHandler.ShowErrors(ErrorHandler);
                }
            }
        }

        private void EditGroup()
        {
            string name = Interaction.InputBox($"Enter the new name of the group '{SelectedGroup!.Name}'", "Edit Group", SelectedGroup!.Name);
            if (name != "")
            {
                if (SelectedGroup!.Name == name)
                {
                    MessageBox.Show("The new name is the same as the current name.\nNo changes were made.", "No Changes", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (Group.ValidateName(ErrorHandler, CurrentProject, name))
                    {
                        SelectedGroup!.Name = name;
                        IsDataModified = true;
                    }
                    else
                    {
                        ErrorHandler.ShowErrors(ErrorHandler);
                    }
                }
            }
        }

        private void RemoveGroup()
        {
            if (MessageBox.Show($"Are you sure you want to remove the group '{SelectedGroup!.Name}'?", "Confirm Group Removal", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                CurrentProject.Groups.Remove(SelectedGroup!);
                IsDataModified = true;
                OnPropertyChanged(nameof(ChannelsCount));
            }
        }

        private void CloneGroup()
        {
            string name = Interaction.InputBox($"Enter the new name of the group '{SelectedGroup!.Name}'", "Edit Group", SelectedGroup!.Name);
            if (name != "")
            {
                if (Group.ValidateName(ErrorHandler, CurrentProject, name))
                {
                    CurrentProject.Groups.Add(new Group(SelectedGroup!));
                    SelectedGroup = CurrentProject.Groups.Last();
                    SelectedGroup!.Name = name;
                    IsDataModified = true;
                    OnPropertyChanged(nameof(ChannelsCount));
                }
                else
                {
                    ErrorHandler.ShowErrors(ErrorHandler);
                }
            }
        }

        private void SortGroupByName()
        {
            if (MessageBox.Show("Are you sure you want to sort the groups by name?\nThis action cannot be undone.", "Confirm Group Sorting", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var sortedGroups = CurrentProject.Groups.OrderBy(group => group.Name).ToList();
                CurrentProject.Groups.Clear();
                foreach (var group in sortedGroups)
                {
                    CurrentProject.Groups.Add(group);
                }
                IsDataModified = true;
            }
        }

        private void MoveUpGroup()
        {
            int index = CurrentProject.Groups.IndexOf(SelectedGroup!);
            CurrentProject.Groups.Move(index, index - 1);
            IsDataModified = true;
        }

        private void MoveDownGroup()
        {
            int index = CurrentProject.Groups.IndexOf(SelectedGroup!);
            CurrentProject.Groups.Move(index, index + 1);
            IsDataModified = true;
        }

        private void AddChannel()
        {
            SelectedGroup!.SelectedChannel = null;
            SelectedGroup!.ChannelDetails = new()
            {
                CtcssTone = CurrentProject.DefaultCtcssTone,
                ColorCode = CurrentProject.DefaultColorCode
            };
            if (CurrentProject.DefaultBandwidth != null) SelectedGroup!.ChannelDetails.Bandwidth = CurrentProject.DefaultBandwidth;
            if (CurrentProject.DefaultTone != null) SelectedGroup!.ChannelDetails.Tone = CurrentProject.DefaultTone;
            if (CurrentProject.DefaultTimeslot != null) SelectedGroup!.ChannelDetails.Timeslot = CurrentProject.DefaultTimeslot;
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
                IsDataModified = true;
                OnPropertyChanged(nameof(ChannelsCount));
            }
        }

        private void CloneChannel()
        {
            Channel clonedChannel = new(SelectedGroup!.SelectedChannel!);
            SelectedGroup!.SelectedChannel = null;
            SelectedGroup!.ChannelDetails = clonedChannel;
            IsAddingChannel = true;
            IsAddingOrEditingChannel = true;
        }

        private void MoveChannel(Group newGroup)
        {
            newGroup.Channels.Add(new(SelectedGroup!.SelectedChannel!));
            SelectedGroup!.Channels.Remove(SelectedGroup.SelectedChannel!);
            SelectedGroup = newGroup;
            SelectedGroup.SelectedChannel = SelectedGroup.Channels.Last();
        }

        private void SortChannelByName()
        {
            if (MessageBox.Show("Are you sure you want to sort the channels by name?\nThis action cannot be undone.", "Confirm Channel Sorting", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var sortedChannels = SelectedGroup!.Channels.OrderBy(channel => channel.Name).ToList();
                SelectedGroup.Channels.Clear();
                foreach (var channel in sortedChannels)
                {
                    SelectedGroup.Channels.Add(channel);
                }
                IsDataModified = true;
            }
        }

        private void SortChannelByFrequency()
        {
            if (MessageBox.Show("Are you sure you want to sort the channels by frequency?\nThis action cannot be undone.", "Confirm Channel Sorting", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var sortedChannels = SelectedGroup!.Channels.OrderBy(channel => channel.Frequency).ToList();
                SelectedGroup.Channels.Clear();
                foreach (var channel in sortedChannels)
                {
                    SelectedGroup.Channels.Add(channel);
                }
                IsDataModified = true;
            }
        }

        private void MoveUpChannel()
        {
            int index = SelectedGroup!.Channels.IndexOf(SelectedGroup.SelectedChannel!);
            SelectedGroup.Channels.Move(index, index - 1);
            IsDataModified = true;
        }

        private void MoveDownChannel()
        {
            int index = SelectedGroup!.Channels.IndexOf(SelectedGroup.SelectedChannel!);
            SelectedGroup.Channels.Move(index, index + 1);
            IsDataModified = true;
        }

        private void ShowAbout()
        {
            AboutWindow aboutWindow = new()
            {
                ShowInTaskbar = false
            };
            aboutWindow.ShowDialog();
        }

        private void SaveChannelChanges()
        {
            if (SelectedGroup!.ChannelDetails!.Validate(ErrorHandler, CurrentProject, SelectedGroup))
            {
                if (IsAddingChannel)
                {
                    SelectedGroup!.Channels.Add(new(SelectedGroup.ChannelDetails!));
                    SelectedGroup.SelectedChannel = SelectedGroup.Channels.Last();
                    IsDataModified = true;
                    OnPropertyChanged(nameof(ChannelsCount));
                }
                else
                {
                    if (MessageBox.Show($"Are you sure you want to save the changes to the channel '{SelectedGroup!.SelectedChannel!.Name}'?", "Confirm Channel Changes", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (SelectedGroup!.Name != SelectedTargetGroup!.Name) MoveChannel(SelectedTargetGroup);
                        int index = SelectedGroup!.Channels.IndexOf(SelectedGroup.SelectedChannel!);
                        SelectedGroup.Channels[index] = new(SelectedGroup.ChannelDetails!);
                        SelectedGroup.SelectedChannel = SelectedGroup.Channels[index];
                        IsDataModified = true;
                        IsAddingChannel = false;
                        IsAddingOrEditingChannel = false;
                    }
                }
            }
            else
            {
                ErrorHandler.ShowErrors(ErrorHandler);
            }
        }

        private void DiscardChannelChanges()
        {
            if (IsAddingOrEditingChannel && MessageBox.Show("Are you sure you want to discard the changes to the channel?", "Confirm Discard Changes", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }
            SelectedGroup!.SelectedChannel = null;
            SelectedGroup!.ChannelDetails = null;
            IsAddingChannel = false;
            IsAddingOrEditingChannel = false;

        }
    }
}
