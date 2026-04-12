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
        public string FilePath { get; set; } = "";
        public string GroupNamePattern { get; set; } = @"^[\x21-\x7E](?:[\x20-\x7E]{0,14}[\x21-\x7E])?$";
        public string GroupNamePatternDescription { get; set; } = $"Group name must be 1 to 16 characters long, cannot start or end with a space, and can only contain printable ASCII characters.";
        public string ChannelNamePattern { get; set; } = @"^[\x21-\x7E](?:[\x20-\x7E]{0,14}[\x21-\x7E])?$";
        public string ChannelNamePatternDescription { get; set; } = $"Channel name must be 1 to 16 characters long, cannot start or end with a space, and can only contain printable ASCII characters.";
        public string CallsignPattern { get; set; } = @"^([A-Z0-9]{1,3}[0-9][A-Z0-9]{0,3}[A-Z])$";
        public string CallsignPatternDescription { get; set; } = "Callsign must follow the ITU amateur callsign format.";

        public bool RequireProjectSaveConfirmation { get; set; } = true;

        public string? DefaultBandwidth { get; set; } = null;
        public string? DefaultTone { get; set; } = null;
        public decimal? DefaultCtcssTone { get; set; } = null;
        public string? DefaultTimeslot { get; set; } = null;
        public int? DefaultColorCode { get; set; } = null;

        public ObservableCollection<Group> Groups { get; set => SetProperty(ref field, value); } = [];
    }
}
