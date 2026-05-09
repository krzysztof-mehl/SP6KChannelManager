using Microsoft.VisualBasic;
using SP6KChannelManager.Commands;
using SP6KChannelManager.Models;
using SP6KChannelManager.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SP6KChannelManager.ViewModels
{
    public partial class MainViewModel
    {
        public RelayCommand? NewProjectCommand { get; private set; }
        public RelayCommand? OpenProjectCommand { get; private set; }
        public RelayCommand? SaveProjectCommand { get; private set; }
        public RelayCommand? SaveAsProjectCommand { get; private set; }
        public RelayCommand? ExitCommand { get; private set; }
        public RelayCommand? AddGroupCommand { get; private set; }
        public RelayCommand? EditGroupCommand { get; private set; }
        public RelayCommand? RemoveGroupCommand { get; private set; }
        public RelayCommand? CloneGroupCommand { get; private set; }
        public RelayCommand? SortGroupByNameCommand { get; private set; }
        public RelayCommand? MoveUpGroupCommand { get; private set; }
        public RelayCommand? MoveDownGroupCommand { get; private set; }
        public RelayCommand? AddChannelCommand { get; private set; }
        public RelayCommand? EditChannelCommand { get; private set; }
        public RelayCommand? RemoveChannelCommand { get; private set; }
        public RelayCommand? CloneChannelCommand { get; private set; }
        public RelayCommand? SortChannelByNameCommand { get; private set; }
        public RelayCommand? SortChannelByCallsignCommand { get; private set; }
        public RelayCommand? SortChannelByFrequencyCommand { get; private set; }
        public RelayCommand? MoveUpChannelCommand { get; private set; }
        public RelayCommand? MoveDownChannelCommand { get; private set; }
        public RelayCommand? SaveChannelCommand { get; private set; }
        public RelayCommand? CloseChannelCommand { get; private set; }
        public RelayCommand? ShowAboutCommand { get; private set; }

        private void InitializeCommands()
        {
            NewProjectCommand = new(NewProject);
            OpenProjectCommand = new(OpenProject);
            SaveProjectCommand = new(SaveProject);
            SaveAsProjectCommand = new(SaveAsProject);
            ExitCommand = new(new Action(() => Application.Current.Windows.OfType<Window>().FirstOrDefault(window => window.IsActive)?.Close()));

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
            SortChannelByCallsignCommand = new(SortChannelByCallsign, () => IsChannelSelected);
            SortChannelByFrequencyCommand = new(SortChannelByFrequency, () => IsChannelSelected);
            MoveUpChannelCommand = new(MoveUpChannel);
            MoveDownChannelCommand = new(MoveDownChannel);
            SaveChannelCommand = new(SaveChannel);
            CloseChannelCommand = new(CloseChannel);

            ShowAboutCommand = new(ShowAbout);
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
            string name = Interaction.InputBox("Enter the name of the new group:", "Add Group");
            if (name != "")
            {
                if (Group.ValidateName(MainErrorHandler, CurrentProject, name))
                {
                    CurrentProject.Groups.Add(new Group { Name = name });
                    SelectedGroup = CurrentProject.Groups.Last();
                    IsDataModified = true;
                }
                else
                {
                    ErrorHandler.ShowErrors(MainErrorHandler);
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
                    if (Group.ValidateName(MainErrorHandler, CurrentProject, name))
                    {
                        SelectedGroup!.Name = name;
                        IsDataModified = true;
                    }
                    else
                    {
                        ErrorHandler.ShowErrors(MainErrorHandler);
                    }
                }
            }
        }

        private void RemoveGroup()
        {
            if (MessageBox.Show($"Are you sure you want to remove the group '{SelectedGroup!.Name}'?", "Confirm Group Removal", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                int index = CurrentProject.Groups.IndexOf(SelectedGroup!);
                CurrentProject.Groups.Remove(SelectedGroup!);
                
                if (CurrentProject.Groups.Count > 0)
                {
                    if (index >= CurrentProject.Groups.Count)
                    {
                        index = CurrentProject.Groups.Count - 1;
                    }
                    SelectedGroup = CurrentProject.Groups[index];
                }
                else
                {
                    SelectedGroup = null;
                }
                IsDataModified = true;
                OnPropertyChanged(nameof(ChannelsCount));
            }
        }

        private void CloneGroup()
        {
            ErrorHandler.NotImplemented();
            string name = Interaction.InputBox($"Enter the new name of the group '{SelectedGroup!.Name}'", "Edit Group", SelectedGroup!.Name);
            if (name != "")
            {
                if (Group.ValidateName(MainErrorHandler, CurrentProject, name))
                {
                    CurrentProject.Groups.Add(new Group(SelectedGroup!));
                    SelectedGroup = CurrentProject.Groups.Last();
                    SelectedGroup!.Name = name;
                    IsDataModified = true;
                    OnPropertyChanged(nameof(ChannelsCount));
                }
                else
                {
                    ErrorHandler.ShowErrors(MainErrorHandler);
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
            ErrorHandler.NotImplemented();
        }

        private void EditChannel()
        {
            ErrorHandler.NotImplemented();
        }

        private void RemoveChannel()
        {
            ErrorHandler.NotImplemented();
        }

        private void CloneChannel()
        {
            ErrorHandler.NotImplemented();
        }

        private void SortChannelByName()
        {
            ErrorHandler.NotImplemented();
        }

        private void SortChannelByCallsign()
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
        }

        private void MoveDownChannel()
        {
            ErrorHandler.NotImplemented();
        }

        private void ShowAbout()
        {
            AboutWindow aboutWindow = new()
            {
                ShowInTaskbar = false
            };
            aboutWindow.ShowDialog();
        }

        private void SaveChannel()
        {
            ErrorHandler.NotImplemented();
        }

        private void CloseChannel()
        {
            ErrorHandler.NotImplemented();
        }
    }
}
