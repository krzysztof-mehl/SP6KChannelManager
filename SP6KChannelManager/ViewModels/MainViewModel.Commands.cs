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
        public RelayCommand? ShowAboutCommand { get; private set; }
        public RelayCommand? SaveChannelCommand { get; private set; }
        public RelayCommand? CloseChannelCommand { get; private set; }

        private void InitializeCommands()
        {
            NewProjectCommand = new(NewProject);
            OpenProjectCommand = new(OpenProject);
            SaveProjectCommand = new(SaveProject);
            SaveAsProjectCommand = new(SaveAsProject);
            ExitCommand = new(new Action(() => Application.Current.Windows.OfType<Window>().FirstOrDefault(window => window.IsActive)?.Close()));

            AddGroupCommand = new(AddGroup);
            EditGroupCommand = new(EditGroup);
            RemoveGroupCommand = new(RemoveGroup);
            CloneGroupCommand = new(CloneGroup);
            SortGroupByNameCommand = new(SortGroupByName);
            MoveUpGroupCommand = new(MoveUpGroup);
            MoveDownGroupCommand = new(MoveDownGroup);

            AddChannelCommand = new(AddChannel);
            EditChannelCommand = new(EditChannel);
            RemoveChannelCommand = new(RemoveChannel);
            CloneChannelCommand = new(CloneChannel);
            SortChannelByNameCommand = new(SortChannelByName);
            SortChannelByCallsignCommand = new(SortChannelByCallsign);
            SortChannelByFrequencyCommand = new(SortChannelByFrequency);
            MoveUpChannelCommand = new(MoveUpChannel);
            MoveDownChannelCommand = new(MoveDownChannel);
            ShowAboutCommand = new(ShowAbout);
            SaveChannelCommand = new(SaveChannel);
            CloseChannelCommand = new(CloseChannel);
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
        }

        private void EditGroup()
        {
            ErrorHandler.NotImplemented();
        }

        private void RemoveGroup()
        {
            ErrorHandler.NotImplemented();
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
        }

        private void MoveDownGroup()
        {
            ErrorHandler.NotImplemented();
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
