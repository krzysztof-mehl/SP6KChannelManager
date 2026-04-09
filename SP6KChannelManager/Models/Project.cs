using SP6KChannelManager.Helpers;
using SP6KChannelManager.ViewModels;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace SP6KChannelManager.Models
{
    public class Project : BaseViewModel
    {
        public String Product { get; set; } = AssemblyHelper.Product;
        public Version Version { get; set; } = AssemblyHelper.Version;
        [JsonIgnore] public string FilePath { get; set => SetProperty(ref field, value); } = "";
        public string GroupNamePattern { get; set => SetProperty(ref field, value); } = @"^[\x21-\x7E](?:[\x20-\x7E]{0,14}[\x21-\x7E])?$";
        public string GroupNamePatternDescription { get; set => SetProperty(ref field, value); } = $"Group name must be 1 to 16 characters long, cannot start or end with a space, and can only contain printable ASCII characters.";
        public string ChannelNamePattern { get; set => SetProperty(ref field, value); } = @"^[\x21-\x7E](?:[\x20-\x7E]{0,14}[\x21-\x7E])?$";
        public string ChannelNamePatternDescription { get; set => SetProperty(ref field, value); } = $"Channel name must be 1 to 16 characters long, cannot start or end with a space, and can only contain printable ASCII characters.";

        public ObservableCollection<Group> Groups { get; set => SetProperty(ref field, value); } = [];
    }
}
