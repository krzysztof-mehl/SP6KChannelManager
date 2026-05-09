using SP6KChannelManager.ViewModels;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace SP6KChannelManager.Models
{
    public class Group : BaseViewModel
    {
        public string Name { get; set => SetProperty(ref field, value); } = "";
        public ObservableCollection<Channel> Channels { get; set => SetProperty(ref field, value); } = [];
        [JsonIgnore] public Channel? SelectedChannel { get; set { IsChannelSelected = value != null; ChannelDetails = new(value); SetProperty(ref field, value); } } = null;
        [JsonIgnore] public Channel? ChannelDetails { get; set { IsChannelDetailsVisible = value != null; SetProperty(ref field, value); } } = null;
        [JsonIgnore] public bool IsChannelSelected { get; private set => SetProperty(ref field, value); } = false;
        [JsonIgnore] public bool IsChannelDetailsVisible { get; private set => SetProperty(ref field, value); } = false;

        public Group()
        {
        }

        public Group(Group? source)
        {
            if (source != null)
            {
                Name = source.Name;
#pragma warning disable IDE0028 // Simplify collection initialization
                Channels = new(source.Channels.Select(c => new Channel(c)));
#pragma warning restore IDE0028 // Simplify collection initialization
            }
        }

        public static bool ValidateName(ErrorHandler errorHandler, Project project, string name)
        {
            if (!Regex.IsMatch(name, project.GroupNamePattern))
            {
                ErrorHandler.AddError(errorHandler, $"{project.GroupNamePatternDescription}\n\n{project.GroupNamePattern}");
                return false;
            }
            if (project.Groups.Any(g => g.Name == name))
            {
                ErrorHandler.AddError(errorHandler, "Group name must be unique within the project.");
                return false;
            }
            return true;
        }
    }
}
