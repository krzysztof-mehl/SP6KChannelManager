using SP6KChannelManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Windows;

namespace SP6KChannelManager.Services
{
    public partial class GroupCollectionService
    {
        [GeneratedRegex(@"^[\x21-\x7E](?:[\x20-\x7E]*[\x21-\x7E])?$")]
        private static partial Regex GroupNameRegex();

        private static bool IsGroupNameCorrect(Project project, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                ErrorHandlerService.AddError(project, "Group name cannot be empty.");

                return false;
            }

            if (!GroupNameRegex().IsMatch(name))
            {
                ErrorHandlerService.AddError(project, "Group name must consist of printable ASCII characters and cannot start or end with a space.");

                return false;
            }

            if (project.UniqueGroupNames)
            {
                foreach (var group in project.GroupCollection.Groups)
                {
                    if (group.Name == name)
                    {
                        ErrorHandlerService.AddError(project, "Group name must be unique.");

                        return false;
                    }
                }
            }

            return true;
        }

        public static bool AddGroupService(Project project, string name)
        {
            if (name != "")
            {
                if (!IsGroupNameCorrect(project, name))
                {
                    return false;
                }

                project.GroupCollection.Groups.Add(new() { Name = name });

                return true;
            }

            return false;
        }

        public static bool EditGroupService(Project project, string name)
        {
            if (name != "")
            {
                if (name != "" && !IsGroupNameCorrect(project, name))
                {
                    return false;
                }

                project.GroupCollection.SelectedGroup!.Name = name;

                return true;
            }

            return false;
        }

        public static void RemoveGroupService(Project project)
        {
            project.GroupCollection.Groups.Remove(project.GroupCollection.SelectedGroup!);
        }

        public static bool CloneGroupService(Project project, string name)
        {

            if (name != "")
            {
                if (!IsGroupNameCorrect(project, name))
                {
                    return false;
                }

                project.GroupCollection.Groups.Add(new() { Name = name, Channels = new(project.GroupCollection.SelectedGroup!.Channels) });

                return true;
            }

            return false;
        }

        public static void MoveUpGroupService(Project project)
        {
            int index = project.GroupCollection.Groups.IndexOf(project.GroupCollection.SelectedGroup!);

            project.GroupCollection.Groups.Move(index, index - 1);
        }

        public static void MoveDownGroupService(Project project)
        {
            int index = project.GroupCollection.Groups.IndexOf(project.GroupCollection.SelectedGroup!);

            project.GroupCollection.Groups.Move(index, index + 1);
        }


        [GeneratedRegex(@"^[\x21-\x7E](?:[\x20-\x7E]*[\x21-\x7E])?$")]
        private static partial Regex ChannelNameRegex();
        private static bool IsChannelNameCorrect(Project project, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                ErrorHandlerService.AddError(project, "Channel name cannot be empty.");

                return false;
            }

            if (!ChannelNameRegex().IsMatch(name))
            {
                ErrorHandlerService.AddError(project, "Channel name must consist of printable ASCII characters and cannot start or end with a space.");

                return false;
            }

            if (project.UniqueChannelNames)
            {
                foreach (var group in project.GroupCollection.Groups)
                {
                    foreach (var channel in group.Channels)
                    {
                        if (channel.Name == name)
                        {
                            ErrorHandlerService.AddError(project, $"Channel name must be unique, this name already exists in group '{group.Name}'");

                            return false;
                        }
                    }
                }
            }
            else
            {
                if (project.UniqueChannelNamesInGroup)
                {
                    foreach (var channel in project.GroupCollection.SelectedGroup!.Channels)
                    {
                        if (channel.Name == name)
                        {
                            ErrorHandlerService.AddError(project, "Channel name must be unique within the group.");

                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public static bool AddChannelService(Project project, string name)
        {
            if (name != "")
            {
                if (!IsChannelNameCorrect(project, name))
                {
                    return false;
                }

                project.GroupCollection.SelectedGroup!.Channels.Add(new() { Name = name });

                return true;
            }

            return false;
        }

        public static bool EditChannelService(Project project, string name)
        {
            if (name != "")
            {
                if (!IsChannelNameCorrect(project, name))
                {
                    return false;
                }

                project.GroupCollection.SelectedGroup!.SelectedChannel!.Name = name;

                return true;
            }

            return false;
        }

        public static void RemoveChannelService(Project project)
        {

            project.GroupCollection.SelectedGroup!.Channels.Remove(project.GroupCollection.SelectedGroup.SelectedChannel!);
        }

        public static bool CloneChannelService(Project project, string name)
        {
            if (name != "")
            {
                if (!IsChannelNameCorrect(project, name))
                {
                    return false;
                }
                project.GroupCollection.SelectedGroup!.Channels.Add(new() { Name = name });

                return true;
            }

            return false;
        }

        public static void MoveUpChannelService(Project project)
        {
            int index = project.GroupCollection.SelectedGroup!.Channels.IndexOf(project.GroupCollection.SelectedGroup.SelectedChannel!);

            project.GroupCollection.SelectedGroup.Channels.Move(index, index - 1);
        }

        public static void MoveDownChannelService(Project project)
        {
            int index = project.GroupCollection.SelectedGroup!.Channels.IndexOf(project.GroupCollection.SelectedGroup.SelectedChannel!);

            project.GroupCollection.SelectedGroup.Channels.Move(index, index + 1);
        }
    }
}
